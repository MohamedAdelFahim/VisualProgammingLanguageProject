using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 0.3f;
    private void FixedUpdate() {
        float horizontal = 0f;
        float vertical = 0f;
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            horizontal += 1f;
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            horizontal -= 1f;
        }

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            vertical += 1f;
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            vertical -= 1f;
        }
        transform.Translate(new Vector3(horizontal * speed, vertical * speed, 0));
    }
}