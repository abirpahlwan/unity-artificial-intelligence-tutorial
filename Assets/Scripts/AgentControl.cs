using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentControl : MonoBehaviour {
	public NavMeshAgent agent;

	// Use this for initialization
	void Start() {
		agent = this.GetComponent<NavMeshAgent>();
	}
}