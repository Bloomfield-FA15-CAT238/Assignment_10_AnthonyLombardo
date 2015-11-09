using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OutsideGuardAI : MonoBehaviour {
	private GuardStates state = GuardStates.Guarding;
	static private List<GameObject> guardPoints = null;
	public static GameControl gc;
	Slider hp;

	#region Guard Options
	public float walkingSpeed = 3.0f;
	public float chasingSpeed = 5.0f;
	public float attackingSpeed = 1.5f;
	public float attackingDistance = 1.0f;
	#endregion

	private GameObject guardingInterestPoint;
	private GameObject playerOfInterest;

	void Start () {
		if (guardPoints == null) {
			guardPoints = new List<GameObject>();
			foreach(GameObject go in GameObject.FindGameObjectsWithTag("guardPoints")){
				guardPoints.Add(go);
			}
		if (hp == null){
				hp = GameObject.Find("HP").GetComponent<Slider>();
			}
		}
		SwitchToGuarding ();
	}

	void Update () {
		switch(state) {
		case GuardStates.Attacking:
			OnAttackingUpdate();
			break;
		case GuardStates.Chasing:
			OnChasingUpdate();
			break;
		case GuardStates.Guarding:
			OnGuardingUpdate();
			break;
			}
		}

		void OnAttackingUpdate(){
		float step = attackingSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerOfInterest.transform.position, step);
		hp.value -= .05f;
		float distance = Vector3.Distance (transform.position, playerOfInterest.transform.position);
		if (distance <= attackingDistance) {
			SwitchToChasing(playerOfInterest);
		}
	}

	void OnChasingUpdate(){
		float step = chasingSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerOfInterest.transform.position, step);
		float distance = Vector3.Distance (transform.position, playerOfInterest.transform.position);
		if (distance <= 3) {
			SwitchToAttacking (playerOfInterest);
		}
	}

	void OnGuardingUpdate(){
			float step = walkingSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, guardingInterestPoint.transform.position, step);
			float distance = Vector3.Distance(transform.position, guardingInterestPoint.transform.position);
			if(distance==0) {
				SelectRandomPatrolPoint();
			}
		}

	void OnTriggerStay(Collider collider){
		if (collider.gameObject.tag == "Player"){
		SwitchToChasing (collider.gameObject);
		}
	}

	void OnTriggerExit(Collider collider){
		SwitchToGuarding ();
	}

	void SwitchToGuarding() {
		state = GuardStates.Guarding;
		GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f);
		SelectRandomPatrolPoint();
		playerOfInterest = null;
	}
	
	void SwitchToAttacking(GameObject target) {
		state = GuardStates.Attacking;
		GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
	}
	
	void SwitchToChasing(GameObject target) {
		state = GuardStates.Chasing;
		GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 0.0f);
		playerOfInterest = target;
	}
	
	void SelectRandomPatrolPoint() {
		int choice = Random.Range(0,guardPoints.Count);
		guardingInterestPoint = guardPoints[choice];
	}
	void RespawnPlayer(){

	}
}
