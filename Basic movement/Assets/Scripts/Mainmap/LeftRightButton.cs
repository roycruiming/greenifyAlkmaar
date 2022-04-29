using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightButton : MonoBehaviour
{

    float initialY;
    bool forcedHidden = false;
    private void Awake() {
        initialY = this.transform.position.y;
    }

    public void ForcedHidden() {
        this.forcedHidden = true;
    }

    public void UnableForcedHidden() {
        this.forcedHidden = false;
    }

    public void SimulateClick() {
        StartCoroutine(clickAnimation());
    }

    public void HideButton() {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 200f, this.transform.position.z);
    }

    public void ShowButton() {
        this.transform.position = new Vector3(this.transform.position.x, this.initialY, this.transform.position.z);
    }

    IEnumerator clickAnimation() {
        this.HideButton();

        yield return new WaitForSeconds(0.2f);

        if(!forcedHidden) this.ShowButton();
    }
}
