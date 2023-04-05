namespace Automata.IO
{


    public static class RelativeDirectoryExtensions
    {
        public static IRelativeDirectory Directory(this IRelativeDirectory root, string name)
            => new RelativeDirectory(root, name);

        public static IRelativeFile File(this IRelativeDirectory dir, string file)
            => new RelativeFile(dir, file);
    }
}