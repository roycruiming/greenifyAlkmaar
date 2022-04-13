using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallingDown : MonoBehaviour
{
    bool startFallingDown = false;
    float animationSpeed;

    public float rotationXvalue;

    Vector3 targetRotation;
    Quaternion qt;

    public void Awake() {
        this.targetRotation = new Vector3(this.rotationXvalue,0,0);
        this.animationSpeed = Random.Range(28.0f, 37.0f);


        qt = Quaternion.Euler(this.targetRotation);

    }
    public void StartFallingDown() {
        this.startFallingDown = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startFallingDown) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, this.qt, animationSpeed * Time.deltaTime);
            animationSpeed += 0.4f;
        }
    }
}
