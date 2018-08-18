using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {
	private GameObject[] goalLocations;
	private NavMeshAgent agent;
	private float agentSpeedMultiplier;
	private Animator animator;

	// Use this for initialization
	void Start() {
		goalLocations = GameObject.FindGameObjectsWithTag("Waypoint");

		agent = this.GetComponent<NavMeshAgent>();
		agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);

		agentSpeedMultiplier = Random.Range(0.8f, 1.2f);

		agent.speed *= agentSpeedMultiplier;

		animator = this.GetComponent<Animator>();
		animator.SetFloat("walkingOffset", Random.Range(0, 1));
		animator.SetFloat("speedMultiplier", agentSpeedMultiplier);
		animator.SetTrigger("isWalking");
	}

	// Update is called once per frame
	void Update() {
		if (agent.remainingDistance < agent.stoppingDistance) {
			agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
		}
	}
}