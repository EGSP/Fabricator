using System;
using Game.Code.World;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Code.Control
{
    public class UnitController : MonoBehaviour
    {
        [FormerlySerializedAs("Unit")] public Unit unit;
        
        
        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var screenPosition = Mouse.current.position.ReadValue();
                if (Camera.main != null)
                {
                    unit.Navigation.GoToPoint(WorldFramework.GetMouseWorldPoint());
                }
            }
        }
    }
}