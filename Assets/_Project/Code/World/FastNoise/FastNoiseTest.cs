using System;
using System.IO;
using Automata.IO;
using Game.Code.Assets;
using Game.Code.Frameworks;
using Game.Code.Types;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class FastNoiseTest : SerializedMonoBehaviour
    {
        [FormerlySerializedAs("EncodedTree")] public string encodedTree;
        [FormerlySerializedAs("Seed")] public int seed;
        [FormerlySerializedAs("Scale")] public float scale;
        [FormerlySerializedAs("Size")] public int size;
        

        [FormerlySerializedAs("GridRootOffset")] public Vector3 gridRootOffset;
        [FormerlySerializedAs("CellSize")] public Vector2 cellSize;
        
        public void Awake()
        {
            // var nodeTree = FastNoise.FromEncodedNodeTree(encodedTree);
            // Debug.Log("Noise test");
            
            // Debug.Log(nodeTree.GenSingle2D(0, 2444, 35));
            // GenerateBitmap(nodeTree, "");
        }


        [Button]
        public void GenerateTestBmp()
        {
            var nodeTree = FastNoise.FromEncodedNodeTree(encodedTree);
            // GenerateBitmap(nodeTree, seed, scale,size);

            var meshFilter = GetComponent<MeshFilter>();
            var meshRenderer = GetComponent<MeshRenderer>();

            var grid = MeshFramework.CreateQuadGrid(size, size, gridRootOffset, cellSize);
            meshFilter.mesh = grid;
            
            var texture = TextureFramework.Create2D(size, size);
            var noise = NoiseFramework.GenerateNoiseMap255(encodedTree, size, size, seed, scale);
            texture.SetPixels((x, y) =>
            {
                var value = noise[y][x];
                var color = new Color((float) value / (float) 255, (float) value / (float) 255,
                    (float) value / (float) 255);
                
                // Debug.Log($"{x}:{y} = {color}");
                return color;
            });
            TextureFramework.SetTexture(meshRenderer, texture, true);

        }
        
        // public static void GenerateBitmap(FastNoise fastNoise, int seed,float scale, ushort size = 512)
        // {
        //     var file = AssetFramework.GetTempDirectory().File("noise-test.bmp");
        //     using (BinaryWriter writer = new BinaryWriter(file.Stream()))
        //     {
        //         const uint imageDataOffset = 14u + 12u + (256u * 3u);
        //
        //         // File header (14)
        //         writer.Write('B');
        //         writer.Write('M');
        //         writer.Write(imageDataOffset + (uint)(size * size)); // file size
        //         writer.Write(0); // reserved
        //         writer.Write(imageDataOffset); // image data offset
        //         // Bmp Info Header (12)
        //         writer.Write(12u); // size of header
        //         writer.Write(size); // width
        //         writer.Write(size); // height
        //         writer.Write((ushort)1); // color planes
        //         writer.Write((ushort)8); // bit depth
        //         // Colour map
        //         for (int i = 0; i < 256; i++)
        //         {
        //             writer.Write((byte)i);
        //             writer.Write((byte)i);
        //             writer.Write((byte)i);
        //         }
        //
        //         var map = new DataMap2D<float>(size,size);
        //         
        //         for (var y = 0; y < map.Map.Length; y++)
        //         {
        //             var row = map.Map[y];
        //             for (var x = 0; x < row.Length; x++)
        //             {
        //                 var value = fastNoise.GenSingle2D(x*scale, y*scale,  seed);
        //                 int noiseValue = (int)Math.Round((value - 0) * 255);
        //                 writer.Write((byte)Math.Clamp(noiseValue, 0, 255));  
        //             }
        //         }
        //         //
        //         // // Image data
        //         // float[] noiseData = new float[size * size];
        //         // FastNoise.OutputMinMax minMax = fastNoise.GenUniformGrid2D(noiseData, 0, 0, size, size, 0.02f, seed);
        //         //
        //         // float scale = 255.0f / (minMax.max - minMax.min);
        //         //
        //         // foreach (float noise in noiseData)
        //         // {
        //         //     //Scale noise to 0 - 255
        //         //     int noiseI = (int)Math.Round((noise - minMax.min) * scale);
        //         //
        //         //     writer.Write((byte)Math.Clamp(noiseI, 0, 255));                    
        //         // }                
        //     }
        // }
    }
}