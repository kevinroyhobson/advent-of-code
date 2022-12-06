namespace AdventOfCode;

public class Day6
{
    private const string InputPath = "input/2022-12-06.txt";

    private string _input;
    private string _buffer = "";
    private int _inputProcessIndex = 0;
    
    public int Puzzle1()
    {
        _input = File.ReadAllText(InputPath);
        while (!IsBufferAMarker())
        {
            ProcessNextCharacter();
        }

        return _inputProcessIndex;
    }

    private void ProcessNextCharacter()
    {
        _buffer += _input[_inputProcessIndex];
        _inputProcessIndex++;

        if (_buffer.Length > 4)
        {
            _buffer = _buffer[1..];
        }
    }

    private bool IsBufferAMarker()
    {
        return _buffer.Distinct().Count() == 4;
    }
}
