namespace AdventOfCode;

public class Day7
{
    private const string InputPath = "input/2022-12-07.txt";
    private Directory _rootDirectory;
    private Directory _currentDirectory;
    private Queue<string> _inputQueue;
    
    public int Puzzle1()
    {
        InitializeDirectoryStructure();
        GetSize(_rootDirectory);
        
        return _allDirectorySizes.Where(size => size < 100000).Sum();
    }

    public int Puzzle2()
    {
        int totalSpace = 70000000;
        int spaceRequired = 30000000;
        
        InitializeDirectoryStructure();
        var usedSpace = GetSize(_rootDirectory);
        int availableSpace = totalSpace - usedSpace;
        int newSpaceNeeded = spaceRequired - availableSpace;
        
        return _allDirectorySizes.Where(size => size >= newSpaceNeeded).Min();
    }
    
    private void InitializeDirectoryStructure() 
    {
        _rootDirectory = new Directory("/", null);
        _currentDirectory = _rootDirectory;
        _inputQueue = new Queue<string>();

        foreach (var line in File.ReadAllLines(InputPath))
        {
            _inputQueue.Enqueue(line);
        }

        while (_inputQueue.Any())
        {
            var nextCommand = _inputQueue.Dequeue();
            if (IsChangeDirectoryCommand(nextCommand))
            {
                ProcessChangeDirectoryCommand(nextCommand);
            }
            else if (IsListCommand(nextCommand))
            {
                var listOutput = new List<string>();
                while (_inputQueue.Any() && !IsCommand(_inputQueue.Peek()))
                {
                    listOutput.Add(_inputQueue.Dequeue());
                }
                ProcessListOutput(listOutput);
            }
        }
    }

    private class Directory
    {
        public Directory(string name, Directory parent)
        {
            Name = name;
            Parent = parent;
            ChildDirectoryByName = new Dictionary<string, Directory>();
            FileSizeByName = new Dictionary<string, int>();
        }
        
        public string Name { get; }
        public Directory Parent { get; }
        public Dictionary<string, Directory> ChildDirectoryByName { get; }
        public Dictionary<string, int> FileSizeByName { get; }
    }

    private bool IsCommand(string line)
    {
        return line.StartsWith("$");
    }

    private bool IsListCommand(string line)
    {
        return line == "$ ls";
    }
    
    private bool IsChangeDirectoryCommand(string line)
    {
        return line.StartsWith("$ cd ");
    }

    private void ProcessListOutput(IEnumerable<string> listOutput)
    {
        foreach (var childEntry in listOutput)
        {
            ProcessChildEntry(childEntry);
        }
    }

    private void ProcessChildEntry(string childEntry)
    {
        var tokens = childEntry.Split();
        var childName = tokens[1];
        if (tokens[0] == "dir")
        {
            if (_currentDirectory.ChildDirectoryByName.ContainsKey(childName)) return;
            var childDir = new Directory(childName, _currentDirectory);
            _currentDirectory.ChildDirectoryByName[childName] = childDir;
        }
        else
        {
            var size = int.Parse(tokens[0]);
            _currentDirectory.FileSizeByName[tokens[1]] = size;
        }
    }

    private void ProcessChangeDirectoryCommand(string command)
    {
        var directoryName = command.Split()[2];
        _currentDirectory = directoryName switch
        {
            "/" => _rootDirectory,
            ".." => _currentDirectory.Parent,
            _ => _currentDirectory.ChildDirectoryByName[directoryName]
        };
    }

    private List<int> _allDirectorySizes = new List<int>();
    private int GetSize(Directory directory)
    {
        var size = directory.FileSizeByName.Values.Sum();
        foreach (var childDirectory in directory.ChildDirectoryByName.Values)
        {
            size += GetSize(childDirectory);
        }
        _allDirectorySizes.Add(size);
        return size;
    }
}
