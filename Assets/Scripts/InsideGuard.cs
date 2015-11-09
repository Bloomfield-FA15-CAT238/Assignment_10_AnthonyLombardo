using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InsideGuard : MonoBehaviour {
	protected GuardStates state = GuardStates.Guarding;
	static protected List<GameObject> navPatrolPoints = null;
	
	public float walkingSpeed = 3.0f;
	public float chasingSpeed = 5.0f;
	public float attackingSpeed = 1.5f;
	
	public float attackingDistance = 2.0f;
	
	protected GameObject patrollingInterestPoint;
	protected GameObject playerOfInterest;
	
	protected float navPatrolDistance = 10.0f;
	public NavMeshAgent navMeshAgent;
	Slider hp;
	
	void Start () {
		if(navPatrolPoints==null) {
			navPatrolPoints = new List<GameObject>();
			foreach(GameObject go in GameObject.FindGameObjectsWithTag("NavPatrolPoints")) {
				navPatrolPoints.Add(go);
			}
		}
		if (hp == null) {
			hp = GameObject.Find ("HP").GetComponent<Slider> ();
		}
		SwitchToGuarding();
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
	
	void OnAttackingUpdate() {
		navMeshAgent.SetDestination(playerOfInterest.transform.position);
		hp.value -= .05f;
		float distance = Vector3.Distance(transform.position, playerOfInterest.transform.position);
		if(distance>3) {
			SwitchToChasing(playerOfInterest);
		}
	}
	
	void OnChasingUpdate() {
		navMeshAgent.SetDestination(playerOfInterest.transform.position);
		float distance = Vector3.Distance(transform.position, playerOfInterest.transform.position);
		if(distance<=3) {
			SwitchToAttacking(playerOfInterest);
		}
	}
	
	void OnGuardingUpdate() {
		navMeshAgent.SetDestination(patrollingInterestPoint.transform.position);
		
		float distance = Vector3.Distance(transform.position, patrollingInterestPoint.transform.position);
		if(distance<=navPatrolDistance) {
			SelectRandomPatrolPoint();
		}
	}
	
	void OnTriggerEnter(Collider collider) {
		SwitchToChasing(collider.gameObject);
	}
	
	void OnTriggerExit(Collider collider) {
		SwitchToGuarding();
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
		int choice = Random.Range(0,navPatrolPoints.Count);
		patrollingInterestPoint = navPatrolPoints[choice];
		navMeshAgent.SetDestination(patrollingInterestPoint.transform.position);
	}
}
