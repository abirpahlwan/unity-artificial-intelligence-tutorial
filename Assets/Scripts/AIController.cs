using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {
	public GameObject goal;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start() {
		agent = this.GetComponent<NavMeshAgent>();
		agent.SetDestination(goal.transform.position);
	}
}