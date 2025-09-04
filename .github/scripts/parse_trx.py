import sys, json, xml.etree.ElementTree as ET

if len(sys.argv) < 2:
    print("Usage: parse_trx.py <path-to-results.trx>", file=sys.stderr)
    sys.exit(2)

trx_path = sys.argv[1]
root = ET.parse(trx_path).getroot()

# Find <Counters> regardless of namespace
counters = None
for elem in root.iter():
    if elem.tag.rsplit('}', 1)[-1] == 'Counters':
        counters = elem
        break

if counters is None:
    print("Could not find <Counters> in TRX", file=sys.stderr)
    sys.exit(1)

def geti(name):
    v = counters.get(name)
    try:
        return int(v)
    except (TypeError, ValueError):
        return 0

total  = geti('total')
passed = geti('passed')

status = "ok" if (total > 0 and passed == total) else "failed"

out = {
  "version": 1,
  "status": status,
  "tests": [{
      "name": "Unit tests",
      "number": 1,
      "score": passed,
      "max_score": total,
      "output": f"Passed {passed} of {total} unit tests."
  }]
}
print(json.dumps(out))
