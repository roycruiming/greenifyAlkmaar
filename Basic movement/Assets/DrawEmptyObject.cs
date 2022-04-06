using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEmptyObject : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(this.transform.position, 0.5f);

    }

   

}
