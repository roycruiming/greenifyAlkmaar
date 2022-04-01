using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{

    public static PLayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Activate()
    {

    }
}
