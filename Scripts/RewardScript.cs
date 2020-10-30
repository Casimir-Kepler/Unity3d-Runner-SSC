using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScript : MonoBehaviour
{
    private Vector3 StartPos;
    private Vector3 EndPos;
    private bool Direction;
    private float step;
    private float Progress;

    void Start()
    {
    	Direction = false;
    	step = 0.01f;
        StartPos = transform.position;
        EndPos = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	if(Direction == false){
    		transform.position = Vector3.Lerp(StartPos, EndPos, Progress);
    	}
        else{
        	transform.position = Vector3.Lerp(EndPos, StartPos, Progress);
        }
        if (Progress >= 1f){
        	Progress = 0f;
        	Direction = !Direction;
        } 
        Progress += step;
    }
}
