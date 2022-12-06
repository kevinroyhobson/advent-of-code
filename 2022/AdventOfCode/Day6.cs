namespace AdventOfCode;

public class Day6
{
    private const string InputPath = "input/2022-12-06.txt";

    private string _input;
    private int _bufferSize = 0;
    private string _buffer = "";
    private int _inputProcessIndex = 0;
    
    public int FindStartMarkerIndex(int markerLength)
    {
        _bufferSize = markerLength;
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

        if (_buffer.Length > _bufferSize)
        {
            _buffer = _buffer[1..];
        }
    }

    private bool IsBufferAMarker()
    {
        return _buffer.Distinct().Count() == _bufferSize;
    }
}
