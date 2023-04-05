using System.Text;

namespace Automata.IO
{


    public interface IO
    {
        string Path { get; }

        static string CorrectSlash(string path)
        {
            var builder = new StringBuilder(path.Length);
            for (var i = 0; i < path.Length; i++)
            {
                var c = path[i];
                switch (c)
                {
                    case '/':
                        Slash(ref i, path, builder);
                        break;
                    case '\\':
                        Slash(ref i, path, builder);
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
            }

            void Slash(ref int index, string source, StringBuilder builder)
            {
                builder.Append('/');
                index++;
                for (; index < source.Length; index++)
                {
                    var c = source[index];
                    if (c == '/' || c == '\\')
                        continue;
                    index--;
                    return;
                }
            }

            return builder.ToString();
        }
    }

// ReSharper disable once InconsistentNaming
    public interface RelativeIO : IO
    {
        public static string SubtractLeft(string leftPath, string fullPath)
            => IO.CorrectSlash(fullPath).Substring(IO.CorrectSlash(leftPath).Length);

        public static IRelativeDirectory RelativeDirectory(
            string leftPath, string fullPath)
        {
            return new RelativeDirectory(SubtractLeft(leftPath, fullPath));
        }

        public static IRelativeFile RelativeFile(string leftPath, string fullPath)
        {
            var relativePath = SubtractLeft(leftPath, fullPath);
            var directoryPath = System.IO.Path.GetDirectoryName(relativePath);
            var filename = System.IO.Path.GetFileName(relativePath);

            return new RelativeFile(new RelativeDirectory(directoryPath), filename);
        }
    }
}
