using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
