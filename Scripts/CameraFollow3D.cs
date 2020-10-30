using UnityEngine;
using System.Collections;

public class CameraFollow3D : MonoBehaviour {

	public float damping = 1.5f;
	public Vector3 offset = new Vector3(2f, 1f, 2f);
	public bool faceLeft = false;
	private Transform player;
	private int lastX;
	private int TryToFindPlayer = 0;

	void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player").transform;
		offset = new Vector3(offset.x, offset.y, offset.z);
        transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, player.position.z);
		FindPlayer(faceLeft);
		TryToFindPlayer++;
	}

	public void FindPlayer(bool playerFaceLeft)
	{
		
		lastX = Mathf.RoundToInt(player.position.x);
		if(playerFaceLeft) transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, player.position.z + offset.z);
		else transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);
	}

    void LateUpdate() 
	{
		if(player)
		{
			transform.rotation = player.rotation;
			Vector3 target;
			//Debug.Log(player.forward);
			target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z) - player.forward * 25;
			Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
			transform.position = target;
		}
		else if(TryToFindPlayer < 5) Start();
			else Debug.LogWarning("Tag \"Player\" not found");
	}
}