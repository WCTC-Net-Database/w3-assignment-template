using w3.console.Interfaces;

namespace w3.tests;

public class MockInput : IInput
{
    private readonly string[] _inputs;
    private int _index;

    public MockInput(string[] inputs = null)
    {
        _inputs = inputs ?? new string[] { };
        _index = 0;
    }

    public string ReadLine()
    {
        if (_index < _inputs.Length)
        {
            return _inputs[_index++];
        }
        return "Mocked input";
    }
}