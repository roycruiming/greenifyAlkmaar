using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToNextScreen : MonoBehaviour
{
    // Start is called before the first frame update

    

     public void start()
    {
     
    }

    public void next() {

       

       // DontDestroyOnLoad(GameObject.Find("OnSceneLoaded"));
        SceneManager.LoadScene(5);

    }

    public void GoToPhotoScene() {
        string score = GameObject.Find("GameTimer").GetComponent<Text>().text;
        GameObject OnSceneLoaded = GameObject.Find("OnSceneLoaded");

        if (OnSceneLoaded == null) {

            GameObject pref = Resources.Load("OnSceneLoaded", typeof(GameObject)) as GameObject;
            pref.gameObject.name = "OnSceneLoaded";
            DontDestroyOnLoad(pref); 


           
            


        }

        SceneLoaded sl = OnSceneLoaded.GetComponent<SceneLoaded>();
        if (sl != null) sl.TimeScore = score;


        GameObject PLAYER = GameObject.Find("3RD Person"); 




        DontDestroyOnLoad(PLAYER);
        DontDestroyOnLoad(OnSceneLoaded);

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
