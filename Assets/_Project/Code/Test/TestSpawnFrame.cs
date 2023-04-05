using Automata.IO;
using Game.Code.Assets;
using Game.Code.Entities;
using Game.Code.World;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Code.Test
{
    public class TestSpawnFrame : MonoBehaviour
    {
        public GameObject prefab;
        //public string path;
        
        
        public async void OnClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var frameGo = GameObject.Instantiate(prefab).GetComponent<ConstructionFrame>();
                frameGo.transform.position = WorldFramework.GetMouseWorldPoint();
                
                var frameFile = AssetFramework.GetAssetFile(new RelativeFile(
                    new RelativeDirectory("/Entities/Frames/"), "testframe.json"));

                var frame = await frameFile.FromJson<Frame>();
                if (frame is null)
                {
                    frame = new Frame(10, 0);
                    await frameFile.ToJson(frame);
                }
                frameGo.Frame = frame;
            }
        }
    }
}