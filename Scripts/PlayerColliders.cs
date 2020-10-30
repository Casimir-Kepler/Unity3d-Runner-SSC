using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColliders : MonoBehaviour
{
	Transform tr;
	Rigidbody rb;
    public GameController GC;
    
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other){
    	if (other.gameObject.tag == "JampPad"){
            rb.velocity = rb.velocity * 0;
            rb.AddForce(tr.up * 35, ForceMode.Impulse);
            Debug.Log("Jump");
        }
        if (other.gameObject.CompareTag("Reward")){
            GC.AddReward();
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Trap")){
            StartCoroutine(Death());
        }
    }

    IEnumerator Death(){
        GC.CancelPlay();
        yield return new WaitForSeconds(1);
        GC.ShowResult();
    }
}
