using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SFB;
using System;

public class TakeScreenshot : MonoBehaviour
{

    public Camera camera;

    public int width = 256;
    public int height = 256;
    public int depth = 24;
    public TextureFormat textureFormat = TextureFormat.RGBA32 ;
    


    private void Awake()
    {
        
        //if (width == null) width = 256;
        //if (height == null)
    }




    [ContextMenu("Take Screenshot")]
    public void TakeScreenShot() {

        string path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "png");


        //check if there is a path 
        if (path == null || path.Length == 0 || System.IO.File.Exists(path)) {
            //path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "png"); 
            return;
            
        }










        RenderTexture rt = new RenderTexture(width, height, depth);
        camera.targetTexture = rt;
        Texture2D screenshot = new Texture2D(width, height, textureFormat, false);
        camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
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
        System.IO.File.WriteAllBytes(path, bytes);





    }


}
