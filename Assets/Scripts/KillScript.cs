using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour {
	[SerializeField] Transform spawnPoint;
	void OnCollisionEnter (Collision c)
	{
		if (c.gameObject.tag == "Player") 
		{
			c.transform.position = spawnPoint.position;
			c.transform.rotation = spawnPoint.rotation;
		}

	}
}