using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Code.World
{
    public class WorldFramework
    {
        public static Vector3 GetMouseWorldPoint()
        {
            var camera = CameraSetup.Camera;
            var screenPosition = Mouse.current.position.ReadValue();
            
            var worldPosition = camera.ScreenToWorldPoint(screenPosition);

            var clampedPosition = new Vector3(worldPosition.x, 0, worldPosition.z);

            return clampedPosition;
        }

        public static Vector3 GetExpectedChunkPosition(Vector3 point, ChunkProperties properties)
        {
            var clampedPoint = new Vector3(Mathf.RoundToInt(point.x), 0, Mathf.RoundToInt(point.y));
            // Перемножаем количество ячеек в строке/столбце на размеры одной ячейки.
            var chunkSizes = new Vector2(properties.Width * properties.CellWidth,
                properties.Height * properties.CellHeight);

            var chunkRoot = new Vector3(clampedPoint.x * chunkSizes.x, 0, clampedPoint.z * chunkSizes.y);
            return chunkRoot;
        }

        [CanBeNull]
        public static Chunk FindNearestChunkToPoint(IList<Chunk> chunks, Vector3 point, ChunkProperties properties)
        {
            var chunkPositionFromPoint = GetExpectedChunkPosition(point, properties);
            
            Chunk nearest = null;
            float sqrDistance = -1;
            for (var i = 0; i < chunks.Count; i++)
            {
                var chunk = chunks[i];
                var distance = Vector3.SqrMagnitude(chunk.Position - chunkPositionFromPoint);

                if (nearest is null)
                {
                    nearest = chunk;
                    sqrDistance = distance;
                    continue;
                }

                if (distance < sqrDistance)
                {
                    nearest = chunk;
                    sqrDistance = distance;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Получение координаты относительно размера радиуса.
        /// </summary>
        public static Vector3 TransformPointToRadiusSpace(Vector3 point, int radius)
        {
            var snapPoint = new Vector3(Mathf.RoundToInt(point.x / radius), 0, Mathf.RoundToInt(point.z / radius));
            return snapPoint;
        }
    }
}