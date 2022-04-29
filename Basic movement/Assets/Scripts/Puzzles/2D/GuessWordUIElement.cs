using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessWordUIElement : MonoBehaviour
{

    float initialY;
    // Start is called before the first frame update
    public void HideElement() {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 800f, this.transform.position.z);
    }

    public void ShowElement() {
        this.transform.position = new Vector3(this.transform.position.x, this.initialY, this.transform.position.z);
    }
}
