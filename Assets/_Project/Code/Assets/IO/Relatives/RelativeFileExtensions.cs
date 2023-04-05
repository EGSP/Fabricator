namespace Automata.IO
{


    public static class RelativeFileExtensions
    {
        public static IRelativeFile RelativeFile(this IFile file, IDirectory excludeDirectoryPath)
        {
            return RelativeIO.RelativeFile(excludeDirectoryPath.Path, file.Path);
        }
    }
}