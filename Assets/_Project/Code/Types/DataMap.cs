using System;
using UnityEngine;

namespace Game.Code.Types
{
    /// <summary>
    /// Height to Width
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataMap2D<T>
    {
        public int Width { get; }
        public int Height { get; }

        public T[][] Map;

        public T[] this[int y] => Map[y];

        public Vector2Int Center { get; }
        
        public DataMap2D(int width, int height)
        {

            Width = width > 0 ? width : 1;
            Height = height > 0 ? height : 1;

            Map = new T[Height][];

            try
            {
                for (var y = 0; y < Height; y++)
                {
                    Map[y] = new T[Width];
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            
            var cW = Mathf.RoundToInt(Width * 0.5f);
            var cH = Mathf.RoundToInt(Height * 0.5f);
            Center = new Vector2Int(cW,cH);
        }

        public void Set(Vector2Int pos,T value, bool centerOffset = true)
        {
            pos += Center;
            Map[pos.y][pos.x] = value;
        }

        public T Get(Vector2Int pos, bool centerOffset = true)
        {
            pos += Center;
            return Map[pos.y][pos.x];
        }
    }
}