using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaded : MonoBehaviour
{
    public string TimeScore;
    private string previousName = "";
    private bool isFirst = true; 

    private void OnEnable()
    {

        print("enabled"); 
        SceneManager.sceneLoaded += onSceneLoaded;
        if (isFirst) {
            isFirst = false;
          GlobalGameHandler.SetPreviousScneneName(SceneManager.GetActiveScene().name);         
        }
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode) {
        print(scene.name);
        Time.timeScale = 1;
        if (scene.name == "socl" || scene.name == "socl 1") OnSocialLayerLoaded();
        else  GlobalGameHandler.SetPreviousScneneName(scene.name);
    }


    void OnSocialLayerLoaded()
    {
        if (GlobalGameHandler.GetPreviousScneneName() != "Multiplayer") {
            if(GameObject.Find("LevelTitle") != null)
            {
                string title = GlobalGameHandler.GetPreviousScneneName();
                if (title == "Tutorial-Level") title = "Tutorial";
                if (title == "DeMeentv2") title = "De Meent";
                if (title == "DeKaasmarkt") title = "Kaasmarkt";

                GameObject.Find("LevelTitle").GetComponent<Text>().text = title ;
                GameObject.Find("GameTimer").GetComponent<Text>().text = TimeScore;

            }
        }
        else
        {
            GameObject obj = GameObject.Find("GameTimer");  //.GetComponent<Text>().text = TimeScore;
            Text x = obj.GetComponent<Text>();
            x.text = TimeScore; 
        }


        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameObject player = GameObject.Find("3RD Person");
        if (player == null) GameObject.Find("player"); 
        GameObject placeholder = GameObject.Find("player placeholder");

        if(placeholder != null)
        {
            foreach (Transform c in placeholder.transform) Destroy(c.gameObject);


        }

        Animator animator = player.GetComponent<Animator>();
        animator.applyRootMotion = false;
        animator.runtimeAnimatorController = Resources.Load("ScoreBoardAnim") as RuntimeAnimatorController;
        player.transform.SetParent(placeholder.transform);
        player.transform.localPosition = new Vector3(0, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        Destroy(player.GetComponent<ThirtPersonPLayerScript>());
        Destroy(player.GetComponent<ArrowVisible>());
        Destroy(player.GetComponent<raycaster>());
        Transform cam = player.transform.Find("Main Camera");
        if (!cam) cam = player.transform.Find("Camera");
        if (cam != null) Destroy(cam.gameObject); 
        Debug.Log("socLayer loaded");
    }

}
