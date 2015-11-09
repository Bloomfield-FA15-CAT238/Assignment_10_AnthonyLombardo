using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class End : MonoBehaviour {
	Vector3 autoRotate = new Vector3(0.0f,5.0f,0.0f);
	public GameControl gc;

	void Start(){

	}

	void Update () {
		transform.Rotate (autoRotate);
	}

	void OnTriggerEnter(Collider other){
		gc.endUI.enabled = true;
	}
	void OnTriggerExit(){
		gc.endUI.enabled = false;
	}
}
