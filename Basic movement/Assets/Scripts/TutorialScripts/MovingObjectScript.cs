using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectScript : MonoBehaviour
{
    public float targetX;
    public float targetY;
    public float targetZ;

    public float speed;

    private bool towardsTarget = true;

    private Vector3 startingPos;
    private Vector3 targetPos;

    public void Awake() {
        this.startingPos = transform.position;
        this.targetPos = new Vector3(targetX, targetY, targetZ);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(towardsTarget) {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        }
        else {
            transform.position = Vector3.Lerp(transform.position, startingPos, Time.deltaTime * speed);
        }

        targetIsReached();
    }

    private void targetIsReached() {
        float distance;
        
        if(towardsTarget) distance = Vector3.Distance(transform.position, targetPos);
        else distance = Vector3.Distance(transform.position, startingPos);

        if(Mathf.Abs(distance) < 0.2) towardsTarget = !towardsTarget;
    }
}
