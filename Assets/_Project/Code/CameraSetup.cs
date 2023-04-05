using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraSetup : MonoBehaviour
{
    [FormerlySerializedAs("DefaultPosition")] public Vector3 defaultPosition;
    [FormerlySerializedAs("DefaultRotation")] public Vector3 defaultRotation;

    public static Camera Camera;

    private void Awake()
    {
        Camera = transform.GetComponent<Camera>();
        if (Camera is null)
        {
            Debug.Log("Camera not found");
            return;
        }

        var transform1 = Camera.transform;
        transform1.position = defaultPosition;
        transform1.eulerAngles = defaultRotation;
    }
}
