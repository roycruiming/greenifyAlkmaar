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
        //
        SceneManager.sceneLoaded += onSceneLoaded;
        if (isFirst) {
            isFirst = false;
          GlobalGameHandler.SetPreviousScneneName(SceneManager.GetActiveScene().name);         
        }
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode) {
        //set timescale back to 1, 
        Time.timeScale = 1;
        //if scene name = socl or socl 1, the social layer function is loaded
        if (scene.name == "socl" || scene.name == "socl 1") OnSocialLayerLoaded();
        //otherwise set the name of the previous scene. 
        else  GlobalGameHandler.SetPreviousScneneName(scene.name);
    }


    void OnSocialLayerLoaded()
    {
        if (GlobalGameHandler.GetPreviousScneneName() != "Multiplayer") {
            if(GameObject.Find("LevelTitle") != null)
            {
                //look at the name of the scene, and apply this to the titl hud element. 
                string title = GlobalGameHandler.GetPreviousScneneName();
                if (title == "Tutorial-Level") title = "Tutorial";
                if (title == "DeMeentv2") title = "De Meent";
                if (title == "DeKaasmarkt") title = "Kaasmarkt";
                GameObject.Find("LevelTitle").GetComponent<Text>().text = title;

                // 
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

        //search for the player
        GameObject player = GameObject.Find("3RD Person");
        //if script cant find the player (when scene directly opened, get placeholderplayer
        if (player == null) GameObject.Find("player");
        GameObject placeholder = GameObject.Find("player placeholder");

        //destroy placeholder "players" 
        if(placeholder != null)
        {
            foreach (Transform c in placeholder.transform) Destroy(c.gameObject);
        }

        //
        Animator animator = player.GetComponent<Animator>();
        animator.applyRootMotion = false;
        //load animationcontroler and use with animator
        animator.runtimeAnimatorController = Resources.Load("ScoreBoardAnim") as RuntimeAnimatorController;
        player.transform.SetParent(placeholder.transform);
        //set player position
        player.transform.localPosition = new Vector3(0, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //destroy unnecessary components.
        Destroy(player.GetComponent<ThirtPersonPLayerScript>());
        Destroy(player.GetComponent<ArrowVisible>());
        Destroy(player.GetComponent<raycaster>());
        //destroy the playercamera 
        Transform cam = player.transform.Find("Main Camera");
        if (!cam) cam = player.transform.Find("Camera");
        if (cam != null) Destroy(cam.gameObject); 
    }

}
