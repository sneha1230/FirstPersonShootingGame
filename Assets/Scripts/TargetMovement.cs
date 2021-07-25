using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float targetMoveSpeed,targetRotateSpeed;
    public bool shouldMove,shouldRotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            transform.position += new Vector3(targetMoveSpeed, 0, 0) * Time.deltaTime;
        }
        if (shouldRotate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, targetRotateSpeed*Time.deltaTime, 0f));
        }
    }
}
