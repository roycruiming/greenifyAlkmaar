using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPage : MonoBehaviour
{
    public GameObject ControlPageUI;
    public GameObject ObjectivesPageUI;

    public void close()
    {
        ControlPageUI.SetActive(false);
        ObjectivesPageUI.SetActive(false);
    }
}
