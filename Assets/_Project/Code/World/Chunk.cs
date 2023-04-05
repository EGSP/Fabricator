using UnityEngine;

namespace Game
{
    public class Chunk : MonoBehaviour
    {
        private MeshRenderer _renderer;
        private MeshFilter _filter;

        public Vector3 Position => transform.position;
    }
    
    public struct ChunkProperties
    {
        public readonly int Width;
        public readonly int Height;

        public readonly int CellWidth;
        public readonly int CellHeight;

        public ChunkProperties(int width, int height, int cellWidth, int cellHeight)
        {
            Width = width;
            Height = height;
            CellWidth = cellWidth;
            CellHeight = cellHeight;
        }

        /// <summary>
        /// Радиус вписанной окружности. Считается что чанк - это квадрат.
        /// </summary>
        public int Radius => Width * CellWidth;
    }
}