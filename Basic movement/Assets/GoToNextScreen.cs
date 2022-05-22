using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToNextScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public void next() {
        if (GlobalGameHandler.GetInstance() != null)
        {
            string scene = GlobalGameHandler.GetNextSceneName();
            SceneManager.LoadScene(scene);
        }
        else {
            Debug.Assert(false, "Start from scene to use this button");
        
        }
    }

    public void GoToPhotoScreen() {
        string score = GameObject.Find("GameTimer").GetComponent<Text>().text;
        GameObject OnSceneLoaded = GameObject.Find("OnSceneLoaded");

        OnSceneLoaded.GetComponent<SceneLoaded>().Time = score;

        Object.DontDestroyOnLoad(GameObject.Find("3RD Person"));
        Object.DontDestroyOnLoad(GameObject.Find("OnSceneLoaded"));
        SceneManager.LoadScene(18);

    }
}
