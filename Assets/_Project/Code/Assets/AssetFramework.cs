using Automata.IO;
using UnityEngine;

namespace Game.Code.Assets
{
    public class AssetFramework
    {
        public readonly static IDirectory Root;
        
        static AssetFramework()
        {
            Root = new Directory(Application.streamingAssetsPath);
        }

        public static IFile GetAssetFile(IRelativeFile relativeFile)
        {
            var file = Root.Join(relativeFile);
            return file;
        }

        public static IDirectory GetTempDirectory()
        {
            
            var dir = Root.Directory("temp");
            dir.Create();
            return dir;
        }
    }
}