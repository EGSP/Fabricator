using System;
using System.Collections;
using System.Collections.Generic;
using Game.Code.Frameworks;
using Game.Code.Types;
using Game.Code.World;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class World : SerializedMonoBehaviour
    {
        [FormerlySerializedAs("Target")] public Transform target;
        
        [FormerlySerializedAs("ChunkPrefab")] public Chunk chunkPrefab;

        public ChunkProperties ChunkProperties;

        private DataMap2D<Chunk> chunks;

        private void Awake()
        {
            chunks = new DataMap2D<Chunk>(100,100);
        }

        private void Update()
        {
            if (target is null)
                return;

            var tPos = target.position;
            var radius = ChunkProperties.Radius;

            var coords = WorldFramework.TransformPointToRadiusSpace(tPos, radius);
            // Debug.Log(coords);

            var intCoords = new Vector3Int(coords.x.ToInt(), 0, coords.z.ToInt());
            UpdateChunk(new Vector2Int(intCoords.x, intCoords.z));
        }

        private void UpdateChunk(Vector2Int coords)
        {
            Debug.Log($"Coords - {coords}");
            var chunk = chunks.Get(coords);

            if (chunk is null)
            {
                var inst = Instantiate(chunkPrefab, new Vector3(coords.x*ChunkProperties.Radius, 0, coords.y*ChunkProperties.Radius),Quaternion.identity);
                chunks.Set(coords,inst);
            }
        }
    }
}
