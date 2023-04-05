namespace Automata.IO
{


    public interface IFile : IO
    {
        IDirectory Directory { get; }
        string Name { get; }
    }

    public sealed class File : IFile
    {
        public string Path { get; }
        public IDirectory Directory { get; }
        public string Name { get; }

        public File(IDirectory directory, string name)
        {
            Path = IO.CorrectSlash(directory.Path + "/" + name);
            Directory = directory;
            Name = name;
        }
    }
}