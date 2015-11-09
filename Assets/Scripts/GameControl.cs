using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
	public static GameControl gc;
	Transform respawnPoint;
	GameObject player;
	Slider hP;
	public Canvas endUI;

	void Start () {
		if (respawnPoint == null || hP == null) {
			respawnPoint = GameObject.Find ("RespawnPoint").GetComponent<Transform> ();
			hP = GameObject.Find ("HP").GetComponent<Slider> ();
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		endUI.enabled = false;
	}
	

	void Update () {
		if (hP.value == 0)
			RespawnPlayer();
	}

	public void RespawnPlayer(){
		player.gameObject.transform.position = respawnPoint.transform.position;
		hP.value = 1;
	}
	public void EndGame(){
		endUI.enabled = true;
	}
	public void OnClick(){
		RespawnPlayer ();
	}
}