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
        if(GameObject.Find("OverworldLevelSelectorHandler").GetComponent<OverworldLevelSelectorHandler>().currentlySelectedLevel != null) {
            Transform target = GameObject.Find("OverworldLevelSelectorHandler").GetComponent<OverworldLevelSelectorHandler>().currentlySelectedLevel.transform;
            Vector3 targetPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
            Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
            transform.position = smoothPos;
        }

    }
}
