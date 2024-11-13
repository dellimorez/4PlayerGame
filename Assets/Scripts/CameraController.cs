using System.Collections;
using System.Collections.Generic;
using PlayerScript;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float z = -30;
    private Vector2 scrollDelta;

    /*
    private void Update()
    {
        scrollDelta = Input.mouseScrollDelta;
        z += scrollDelta.y;
    }
    */

    private void FixedUpdate()
    {
        transform.position = new Vector3(PlayerController.playerPosition.x,
            PlayerController.playerPosition.y,
            z);
    }
}
