using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaded : MonoBehaviour
{
    public string Time;
    //private Scene PreviousScene;
    //private readonly string[] Levels = {"Tutorial-Level", "DeMeentV2", "DeKaasMarkt"};

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;

    }

    private void Awake()
    {
        
    }



    // Start is called before the first frame update
    // called second

    private void onSceneLoaded(Scene scene, LoadSceneMode mode) {
        print(scene.name); 
        if (scene.name == "socl") OnSocialLayerLoaded(); 
        else  GlobalGameHandler.SetPreviousScneneName(scene.name);  
        
    }


    void OnSocialLayerLoaded()
    {


        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameObject.Find("GameTimer").GetComponent<Text>().text = Time; 

        GameObject player = GameObject.Find("3RD Person");
        

        GameObject placeholder = GameObject.Find("player placeholder");


        foreach (Transform c in placeholder.transform) Destroy(c.gameObject);


        //set animator to our custom animator
        Animator animator = player.GetComponent<Animator>();
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

       



        //player.transform.parent = placeholder.transform;

        Debug.Log("socLayer loaded");

    }

}
