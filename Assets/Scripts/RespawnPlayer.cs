using UnityEngine;
using System.Collections;

public class RespawnPlayer : MonoBehaviour {
	public Transform respawn;

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.transform.position = respawn.transform.position;
		}
	}
}
