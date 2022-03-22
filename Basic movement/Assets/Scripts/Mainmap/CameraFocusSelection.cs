using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusSelection : MonoBehaviour
{
    public float cameraSpeed;
    public float offsetX;

    public void FixedUpdate() {
        Follow();
    }

    void Follow() {
        Transform target = GameObject.Find("level-selector-outline").transform;
        Vector3 targetPos = new Vector3(target.position.x + offsetX, transform.position.y, transform.position.z);
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
        transform.position = smoothPos;
    }
}
