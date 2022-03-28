using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingIconCameraController : MonoBehaviour
{
    public Camera puzzleCam;

    public GameObject player;

    protected Camera activeCamera;

    public IEnumerator moverLoadCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiatalizeCam()
    {
        //This causes the screen to leave the player's back and give them their cursor back
        player.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //This makes sure there is a camera the game can now use at the right distance from the canvas
        activeCamera = Instantiate(puzzleCam, new Vector3(245f, 300f, -545f), Quaternion.identity);
        //make sure it's position is relative to the parent!
        activeCamera.transform.SetParent(transform, false);
        transform.GetComponent<Canvas>().worldCamera = activeCamera;
    }

    public IEnumerator LeaveCam(bool cleared)
    {
        //This makes sure the moving icons stop and are removed upon answering
        StopCoroutine(moverLoadCoroutine);
        yield return new WaitForSeconds(1f);
        //This gives control back to the player character, with no cursor control
        player.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //This causes the boxes to become green!
        Color clear = new Color(0.05f, 0.9f, 0f);

        gameObject.transform.parent.GetComponent<Renderer>().material.color = clear;

        //When the puzzle is done, it must be destroyed
        Destroy(this.gameObject);
    }

}
