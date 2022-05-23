using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPicture : MonoBehaviour
{
    // Start is called before the first frame update

    // public TextAsset imageAsset;
    public void Start()
    {
        // Create a texture. Texture size does not matter, since
        // LoadImage will replace with with incoming image size.
        Texture2D tex = new Texture2D(2, 2);
        //tex.LoadImage(imageAsset.bytes);
        //GetComponent<Renderer>().material.mainTexture = tex;
    }
}




