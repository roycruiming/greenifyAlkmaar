using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float targetX;
    public float targetY;
    public float targetZ;

    public float speed;

    private bool towardsTarget = true;

    private Quaternion startingRotation;
    private Quaternion targetRotation;

    public void Awake() {
        this.targetRotation = Quaternion.Euler(targetX, targetY, targetZ);
        this.startingRotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x,this.transform.rotation.eulerAngles.y,this.transform.rotation.eulerAngles.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(towardsTarget) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, this.targetRotation, speed * Time.deltaTime);
        }
        else transform.rotation = Quaternion.RotateTowards(transform.rotation, this.startingRotation, speed * Time.deltaTime);

        targetIsReached();
    }

    // private void targetIsReached() {
    //     float distance = 10;
        
    //     if(towardsTarget) distance = Vector3.Distance(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), new Vector3(targetRotation.x, targetRotation.y, targetRotation.z));
    //     //else distance = Vector3.Distance(transform.position, startingPos);
    //     print(distance);
    //     if(Mathf.Abs(distance) < 0.2) towardsTarget = !towardsTarget;
    // }
    
     private void targetIsReached() {

         if(towardsTarget) {
             if(transform.rotation.x == targetRotation.x && transform.rotation.y == targetRotation.y && transform.rotation.z == targetRotation.z) towardsTarget = !towardsTarget;
         } 
         else if(transform.rotation.x == startingRotation.x && transform.rotation.y == startingRotation.y && transform.rotation.z == startingRotation.z) towardsTarget = !towardsTarget;
     }
}
