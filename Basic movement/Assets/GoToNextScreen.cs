using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToNextScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject myPrefab;

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

    public void GoToPhotoScene() {
        string score = GameObject.Find("GameTimer").GetComponent<Text>().text;
        GameObject OnSceneLoaded = GameObject.Find("OnSceneLoaded");

        if (OnSceneLoaded == null) {
           GameObject pref =  Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            pref.gameObject.name = "OnSceneLoaded"; 
            OnSceneLoaded =   GameObject.Find("OnSceneLoaded");
            OnSceneLoaded.gameObject.name = "OnSceneLoaded"; 


        }

        SceneLoaded sl = OnSceneLoaded.GetComponent<SceneLoaded>();
        if(sl != null) sl.TimeScore = score;


        Object.DontDestroyOnLoad(GameObject.Find("3RD Person"));
        Object.DontDestroyOnLoad(OnSceneLoaded);

        SceneManager.LoadScene(18);

    }

    public void GoToPhotoSceneMultiPlayer()
    {
        //string score = GameObject.Find("GameTimer").GetComponent<Text>().text;
        GameObject OnSceneLoaded = GameObject.Find("OnSceneLoaded");

        //OnSceneLoaded.GetComponent<SceneLoaded>().TimeScore = score;

        //Object.DontDestroyOnLoad(GameObject.Find("3RD Person"));
        //Object.DontDestroyOnLoad(GameObject.Find("OnSceneLoaded"));

        SceneManager.LoadScene(19);

    }

}
