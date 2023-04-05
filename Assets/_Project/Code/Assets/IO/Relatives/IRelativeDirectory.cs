namespace Automata.IO
{


    public interface IRelativeDirectory : IO
    {

    }

    public class RelativeDirectory : IRelativeDirectory
    {
        public string Path { get; }

        public RelativeDirectory(string path)
        {
            Path = IO.CorrectSlash(path);
        }

        public RelativeDirectory(IRelativeDirectory root, string name)
        {
            Path = IO.CorrectSlash(root.Path + "/" + name);
        }
    }
}