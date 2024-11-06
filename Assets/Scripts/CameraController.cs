using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float z = -10;
    private Vector2 scrollDelta;
    private void Update()
    {
        scrollDelta = Input.mouseScrollDelta;
        z += scrollDelta.y;
        Debug.Log(scrollDelta);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(PlayerController.playerPosition.x,
            PlayerController.playerPosition.y,
            z);
    }
}
