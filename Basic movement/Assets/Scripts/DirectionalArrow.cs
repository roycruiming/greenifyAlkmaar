using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> target;
    public int objectivesCounter = 0;



    private void Update()
    {
        //Arrow points to next objective
        Vector3 targetPosition = target[0].transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

        //Destroy is all objects are done
        if(objectivesCounter > target.Count)
        {
            Destroy(this.gameObject);
        }
    }

    public void DeleteItemInList(int valueTest)
    {
        //target.RemoveAt(valueTest);
        //target.Remove.name("Cube" + valueTest);
        target.RemoveAll(x => x.name =="Cube "+valueTest);
       
    }


}
