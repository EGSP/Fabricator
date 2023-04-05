using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Code.World
{
    public class QuadGrid : MonoBehaviour
    {
        [FormerlySerializedAs("Size")] public int size;
        [FormerlySerializedAs("Offset")] public Vector3 offset;
        
        public void Awake()
        {
            if (GetComponent<MeshFilter>() is null)
                return;
            
            GenerateGrid();

            // transform.position -= new Vector3(size / 2f, 0, size / 2f);
        }

        private void GenerateGrid()
        {
            var mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            mesh.name = "Procedural Grid";

            var _offset = GetOffsetVector();

            var dataSize = (size + 1) * (size + 1);

            var vertices = new Vector3[dataSize];
            var uv = new Vector2[dataSize];
            
            for (int i = 0, y = 0; y <= size; y++) 
            {
                for (int x = 0; x <= size; x++, i++)
                {
                    vertices[i] = new Vector3(x, 0, y) + _offset;
                    uv[i] = new Vector2((float) x / (float) size, (float) y / (float) size);
                }
            }
            mesh.vertices = vertices;
            mesh.uv = uv;


            int[] triangles = new int[size * size * 6];
            for (int ti = 0, vi = 0, y = 0; y < size; y++, vi++)
            {
                for (int x = 0; x < size; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + size + 1;
                    triangles[ti + 5] = vi + size + 2;
                }
            }

            mesh.triangles = triangles;
        }

        private Vector3 GetOffsetVector()
        {
            var dir = offset;
            dir *= size*0.5f;
            return dir;
        }
    }
}