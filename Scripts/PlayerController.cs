using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 10;
	Transform tr;
	Rigidbody rb;
    GameObject Model;

	Vector3 moveDirection;
	int Direction;
	float RotateSpeed;
	float Rotation;
    float Side;

    public GameController GC;

    void Start()
    {
        TouchController.TouchEvent += CheckInput;

        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        Side = 0;
        moveDirection = new Vector3(0, 0, 1);
        Direction = 0;
        RotateSpeed = 0.11f * speed;
        Rotation = tr.rotation.y;

        Model = GameObject.FindGameObjectWithTag("Player");
        Model.transform.localPosition = new Vector3(Side, -0.5f, 0);
    }

    void FixedUpdate(){
        if (GC.ReturnPlay()){
            Model.transform.localPosition = new Vector3(Side, Model.transform.localPosition.y, 0);
            moveDirection = tr.forward;
            rb.velocity = moveDirection * speed;

            if (Direction == -1) TurnLeft();
            if (Direction == 1) TurnRight();
        }
        else{
            rb.velocity = Vector3.zero;
        }
    }

    void TurnLeft(){
    	rb.velocity = moveDirection * speed;
     	tr.rotation = Quaternion.Euler(tr.rotation.eulerAngles.x, tr.rotation.eulerAngles.y - RotateSpeed, tr.rotation.eulerAngles.z);
    }

    void TurnRight(){
    	rb.velocity = moveDirection * speed;
     	tr.rotation = Quaternion.Euler(tr.rotation.eulerAngles.x, tr.rotation.eulerAngles.y + RotateSpeed, tr.rotation.eulerAngles.z);
    }

    public void SetZeroPos(){
        tr.position = new Vector3(0, 2, 0);
        Side = 0;
        Direction = 0;
        speed = 10;
    }

    void CheckInput(TouchController.TouchType type)
    {
        if (GC.ReturnPlay()){
            if (type == TouchController.TouchType.LEFT){
                if (Model.transform.localPosition.x > -4){
                    Side -= 0.03f;
                    if(Side <= -4) Side = -4;
                }
            }
            if (type == TouchController.TouchType.RIGHT){
                if (Model.transform.localPosition.x < 4){
                    Side += 0.03f;
                    if(Side >= 4) Side = 4;
                }
            }
        }  
    }

    void OnTriggerEnter(Collider other){
        if (GC.ReturnPlay()){
            switch(other.gameObject.tag){
                case "Turn_Left":
                    Direction = -1;
                    Rotation -= 90;
                    break;

                case "Turn_Right":
                    Direction = 1;
                    Rotation += 90;
                    break;
            
                case "Turn_Stop":
                    Direction = 0;
                    tr.rotation = Quaternion.Euler(tr.rotation.eulerAngles.x, Rotation, tr.rotation.eulerAngles.z);
                    break;
                default: return;
            }
        }
    }

}
