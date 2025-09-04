import sys, os, json, xml.etree.ElementTree as ET
from collections import defaultdict

def ln(tag):  # local-name helper (namespace-agnostic)
    return tag.rsplit('}', 1)[-1]

def load_points_config(path):
    # Example:
    # { "scale_total": 100,
    #   "default": 1,          # used only if no scale_total given
    #   "tests": { "Class.Method": 25, ... } }
    cfg = {"default": 1, "tests": {}, "scale_total": None}
    if os.path.exists(path):
        with open(path, "r", encoding="utf-8") as f:
            raw = json.load(f)
            if "default" in raw:
                cfg["default"] = int(raw["default"])
            if "tests" in raw and isinstance(raw["tests"], dict):
                cfg["tests"] = {k: int(v) for k, v in raw["tests"].items()}
            if "scale_total" in raw and raw["scale_total"] is not None:
                cfg["scale_total"] = int(raw["scale_total"])
    return cfg

def build_definitions(root):
    """Map UnitTest 'id' -> fully qualified 'class.method' (fallback to testName)."""
    id_to_name = {}
    for ut in root.iter():
        if ln(ut.tag) != "UnitTest":
            continue
        uid = ut.get("id")
        tm = None
        for ch in ut:
            if ln(ch.tag) == "TestMethod":
                tm = ch
                break
        if uid and tm is not None:
            cls = tm.get("className", "") or ""
            name = tm.get("name", "") or ""
            fq = f"{cls}.{name}" if cls else name
            id_to_name[uid] = fq
    return id_to_name

def read_results(root, id_to_name):
    """Yield (full_name, outcome) for each executed test."""
    for r in root.iter():
        if ln(r.tag) != "UnitTestResult":
            continue
        tid = r.get("testId")
        outcome = (r.get("outcome") or "").strip()
        name = id_to_name.get(tid) or (r.get("testName") or "UNKNOWN")
        yield name, outcome

def distribute_evenly_int(total, n):
    """Return n integers summing to total, as even as possible."""
    if n <= 0:
        return []
    base = total // n
    rem = total - base * n
    return [base + (1 if i < rem else 0) for i in range(n)]

def main():
    if len(sys.argv) < 2:
        print("Usage: parse_trx.py <path-to-results.trx> [--list]", file=sys.stderr)
        sys.exit(2)

    trx_path = sys.argv[1]
    list_only = any(arg == "--list" for arg in sys.argv[2:])

    root = ET.parse(trx_path).getroot()
    defs = build_definitions(root)
    results = list(read_results(root, defs))  # [(name, outcome), ...]

    if list_only:
        for name, _ in results:
            print(name)
        return

    cfg = load_points_config(".github/autograding-points.json")
    scale_total = cfg.get("scale_total")
    explicit_map = cfg.get("tests", {}) or {}
    default_pts = int(cfg.get("default", 1))

    # Build the points table for this run
    all_names = [name for name, _ in results]
    explicit_present = {n: explicit_map[n] for n in all_names if n in explicit_map}
    unspecified = [n for n in all_names if n not in explicit_map]

    points_for = {}

    if scale_total:  # Explicit tests fixed, remainder evenly split among unspecified
        explicit_sum = sum(explicit_present.values())
        remaining = scale_total - explicit_sum

        if remaining < 0:
            # Too many points pinned; we keep explicit only and warn in stderr/log
            print(f"[autograder] Warning: explicit tests ({explicit_sum}) exceed scale_total "
                  f"({scale_total}). Using explicit_sum as total.", file=sys.stderr)
            # assign only explicit; unspecified get 0
            points_for.update({n: p for n, p in explicit_present.items()})
            for n in unspecified:
                points_for[n] = 0
            total_points_assigned = explicit_sum
        else:
            if unspecified:
                shares = distribute_evenly_int(remaining, len(unspecified))
                for n, share in zip(unspecified, shares):
                    points_for[n] = share
            else:
                if remaining > 0:
                    print(f"[autograder] Note: no unspecified tests to receive remaining "
                          f"{remaining} points; total will be {explicit_sum}.", file=sys.stderr)
            # add explicit on top
            for n, p in explicit_present.items():
                points_for[n] = points_for.get(n, 0) + p
            total_points_assigned = sum(points_for.values())
    else:
        # No scale_total: use per-test explicit values, default for others, then sum
        for n in unspecified:
            points_for[n] = default_pts
        for n, p in explicit_present.items():
            points_for[n] = p
        total_points_assigned = sum(points_for.values())

    # Compute earned
    earned_points = 0
    breakdown = []
    for name, outcome in results:
        pts = int(points_for.get(name, 0))
        mark = "✔" if outcome.lower() == "passed" else ("⏭" if outcome.lower() == "notexecuted" else "✘")
        if outcome.lower() == "passed":
            earned_points += pts
        breakdown.append(f"{mark} {name} (+{pts})")

    status = "ok" if (total_points_assigned > 0 and earned_points == total_points_assigned) else "failed"
    text = "Autograder breakdown:\n" + "\n".join(breakdown)

    out = {
      "version": 1,
      "status": status,
      "tests": [{
          "name": "Unit tests",
          "number": 1,
          "score": earned_points,
          "max_score": total_points_assigned,
          "output": f"Points {earned_points}/{total_points_assigned}.\n\n{text}"
      }]
    }
    print(json.dumps(out))

if __name__ == "__main__":
    main()
