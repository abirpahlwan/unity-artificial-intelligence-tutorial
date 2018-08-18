using UnityEngine;

public class Patrol : NPCBaseFSM {
	private GameObject[] waypoints;
	private int currentWaypoint;

	void Awake() {
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator, stateInfo, layerIndex);
		currentWaypoint = 0;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (waypoints.Length == 0) return;
		if (Vector3.Distance(waypoints[currentWaypoint].transform.position, NPC.transform.position) < accuracy) {
			currentWaypoint++;
			if (currentWaypoint >= waypoints.Length) {
				currentWaypoint = 0;
			}
		}

		/*var direction = waypoints[currentWaypoint].transform.position - NPC.transform.position;
		NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
		NPC.transform.Translate(0, 0, Time.deltaTime * speed);*/

		agent.SetDestination(waypoints[currentWaypoint].transform.position);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}
}