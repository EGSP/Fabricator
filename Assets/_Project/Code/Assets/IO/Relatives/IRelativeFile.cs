namespace Automata.IO
{


    public interface IRelativeFile : IO
    {
        IRelativeDirectory RelativeRoot { get; }
        string Name { get; }
    }

    public class RelativeFile : IRelativeFile
    {
        public string Path { get; }

        public IRelativeDirectory RelativeRoot { get; }
        public string Name { get; }

        public RelativeFile(IRelativeDirectory root, string name)
        {
            RelativeRoot = root;
            Name = name;
            Path = IO.CorrectSlash(RelativeRoot.Path + "/" + name);
        }
    }
}