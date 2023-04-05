using System;
using Game.Code.Types;
using UnityEngine;

namespace Game.Code.Frameworks
{
    public class NoiseFramework
    {
        public static DataMap2D<int> GenerateNoiseMap255(string encodedTree, int width, int height, int seed, float scale=1)
        {
            var map = new DataMap2D<int>(width,height);

            var tree = FastNoise.FromEncodedNodeTree(encodedTree);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var value = tree.GenSingle2D(x * scale, y * scale, seed);
                    var clampValue = (int)Math.Round((value +1)*0.5f * 255);
                    map[y][x] = clampValue;
                }
            }

            return map;
        }
    }
}