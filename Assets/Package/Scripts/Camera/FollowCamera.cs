using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple script which forces an object to follow the camera.
/// </summary>
public class FollowCamera : MonoBehaviour
{
    Transform mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        // When loading a new scene there may be a new camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }

        transform.position = mainCamera.position;
    }
}
