using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class TakeScreenshot : MonoBehaviour
{

    private Camera camera;


    public int width;
    public int heigth;
    public TextureFormat textureFormat;

    private void Awake()
    {
        
        this.width = 1000;
        this.heigth = 1000;
        this.textureFormat = TextureFormat.RGBA32; 
        
    }



    [ContextMenu("Take Screenshot")]
    public void TakeScreenShot() {


        if (camera == null) camera = this.GetComponent<Camera>();



        RenderTexture rt = new RenderTexture(width, heigth, 24);
        camera.targetTexture = rt;
        Texture2D screenshot = new Texture2D(width, heigth, TextureFormat.RGBA32, false);
        camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, heigth), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor)
        {
            DestroyImmediate(rt);
        }
        else
        {
            Destroy(rt);
        }

        byte[] bytes = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes("C:/Users/tim/Documents/GitHub/greenifyAlkmaar/Basic movement/Assets/Sprites/Camera/test.png", bytes);





    }


}
