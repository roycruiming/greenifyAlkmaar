using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToNextScreen : MonoBehaviour
{



    public void next() {
        SceneManager.LoadScene(5);
    }

    public void nextMp()
    {
        SceneManager.LoadScene(0);
    }


    public void GoToPhotoScene() {

        GameObject p = GameObject.Find("3RD Person"); 
        DontDestroyOnLoad(p);
        SceneManager.LoadScene(18);

    }

    public void GoToPhotoSceneMultiPlayer()
    {
        SceneManager.LoadScene(19);
    }

}
