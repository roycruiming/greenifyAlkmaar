using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class TakeScreenshot : MonoBehaviour
{

    private Camera camera; 





    [ContextMenu("Take Screenshot")]
    public void TakeScreenShot() {


        if (camera == null) camera = this.GetComponent<Camera>();



        RenderTexture rt = new RenderTexture(256, 256, 24);
        camera.targetTexture = rt;
        Texture2D screenshot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
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
