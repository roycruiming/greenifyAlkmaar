using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadTemp : MonoBehaviour
{

    private void Start()
    {
        LoadPNG(Application.persistentDataPath + "/FotoTemp.jpg"); 
    }

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
    


            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            RawImage image = GameObject.Find("RawImage").GetComponent<RawImage>();
            image.color = new Color32(255, 255, 225, 255);
            image.texture = tex; 
            image.enabled = true; 

        }
        return tex;
    }

}
