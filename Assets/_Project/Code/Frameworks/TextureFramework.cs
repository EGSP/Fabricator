using System;
using UnityEngine;

namespace Game.Code.Frameworks
{
    public class TextureFramework
    {
        public static int FlatArrayIndex(int x, int y, int width) => x + (y * width);

        public static Texture2D Create2D(int w,int h)
        {
            var tex = new Texture2D(w, h, TextureFormat.RGBA32, 0, false);
            tex.filterMode = FilterMode.Point;
            return tex;
        }

        public static Texture2D GetOrCreateMainTexture(MeshRenderer renderer, int width=100,int height=100)
        {
            var mat = renderer.material;
            var tex = mat.mainTexture as Texture2D;
            if (tex != null)
                return tex;

            tex = Create2D(width, height);
            return tex;
        }
        
        public static Texture2D CreateAndSetMainTexture(MeshRenderer renderer, int width=100,int height=100)
        {
            var mat = renderer.material;
            var tex = Create2D(width, height);

            mat.mainTexture = tex;
            return tex;
        }

        public static void SetTexture(MeshRenderer renderer, Texture2D texture, bool shared = false)
        {
            if (shared is false)
            {
                var mat = renderer.material;
                mat.mainTexture = texture;
            }
            else
            {
                var mat = renderer.sharedMaterial;
                mat.mainTexture = texture;
            }
        }
    }

    public static class TextureExtensions
    {
        public static void SetPixels(this Texture2D tex, Func<int, int, Color> pixelFunction)
        {
            var w = tex.width;
            var h = tex.height;

            var colors = new Color[w * h];

            for (var x = 0; x < w; x++)
            {
                for (var y = 0; y < h; y++)
                {
                    var color = pixelFunction(x, y);
                    colors[TextureFramework.FlatArrayIndex(x, y, w)] = color;
                }
            }
            
            tex.SetPixels(colors);
            tex.Apply();
        }
    }
}