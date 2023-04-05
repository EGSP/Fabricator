using UnityEngine;

namespace Game.Code.Frameworks
{
    public class MeshFramework
    {
        public static Mesh CreateQuadGrid(int width, int height, Vector3 rootOffset, Vector2 cellSize)
        {
            var mesh = new Mesh
            {
                name = "Procedural Grid"
            };
            var size = width * height;
            var dataSize = (width + 1) * (height + 1);

            rootOffset -= new Vector3((float)width*cellSize.x/2f, 0,(float)height*cellSize.y/2f);
            
            var vertices = new Vector3[dataSize];
            var uv = new Vector2[dataSize];
            
            for (int i = 0, y = 0; y <= height; y++) 
            {
                for (int x = 0; x <= width; x++, i++)
                {
                    vertices[i] = new Vector3(x*cellSize.x, 0, y*cellSize.y) + rootOffset;
                    uv[i] = new Vector2((float) x / (float) width, (float) y / (float) height);
                }
            }
            mesh.vertices = vertices;
            mesh.uv = uv;


            int[] triangles = new int[width * height * 6];
            for (int ti = 0, vi = 0, y = 0; y < height; y++, vi++)
            {
                for (int x = 0; x < width; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + width + 1;
                    triangles[ti + 5] = vi + width + 2;
                }
            }

            mesh.triangles = triangles;
            return mesh;
        }
    }
}