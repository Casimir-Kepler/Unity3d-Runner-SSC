using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrapScript : MonoBehaviour
{
	public bool Direction = false;
	private Vector3 StartPos;
    private Vector3 EndPos;
    private float step;
    private float Progress;
    
    void Start()
    {
    	step = 0.02f;
        StartPos = transform.localPosition;
        EndPos = new Vector3(transform.localPosition.x + 6f, transform.localPosition.y, transform.localPosition.z);
    }

    
    void FixedUpdate()
    {
    	transform.Rotate(new Vector3(-3,0,0));
    	if(Direction == false){
    		transform.localPosition = Vector3.Lerp(StartPos, EndPos, Progress);
    	}
        else{
        	transform.localPosition = Vector3.Lerp(EndPos, StartPos, Progress);
        }
        if (Progress >= 1f){
        	Progress = 0f;
        	Direction = !Direction;
        } 
        Progress += step;
    }
}
