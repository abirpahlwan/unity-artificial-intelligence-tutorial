using UnityEngine;
using UnityStandardAssets.Utility;

public class FollowPath : MonoBehaviour {
	[SerializeField] private GameObject waypointManager;

	[SerializeField] private Transform[] waypoints;

	// [SerializeField] private Transform goal;
	private int currentntWaypoint = -1;

	UnityEngine.AI.NavMeshAgent agent;

	void Start() {
		waypoints = waypointManager.GetComponent<WaypointCircuit>().Waypoints;
		Debug.Log("Total waypoints: " + waypoints.Length);

		agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	// Update is called once per frame
	void Update() {
		/* if(Input.GetMouseButtonUp(0)){
			Vector3 mouse = Input.mousePosition;
			Ray castPoint = Camera.main.ScreenPointToRay(mouse);
			RaycastHit hit;
			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity)) {
				goal.position = hit.point;
			}
		} */
	}

	public void GoToPrevPoint() {
		currentntWaypoint = currentntWaypoint <= 0 ? waypoints.Length - 1 : --currentntWaypoint;
		agent.SetDestination(waypoints[currentntWaypoint].transform.position);
		UpdateColors();
		Debug.Log("Current target: Waypoint->" + (currentntWaypoint + 1));
	}

	public void GoToNextPoint() {
		currentntWaypoint = currentntWaypoint >= waypoints.Length - 1 ? 0 : ++currentntWaypoint;
		agent.SetDestination(waypoints[currentntWaypoint].transform.position);
		UpdateColors();
		Debug.Log("Current target: Waypoint->" + (currentntWaypoint + 1));
	}

	private void UpdateColors() {
		for (int i = 0; i < waypoints.Length; i++) {
			if (i == currentntWaypoint) {
				waypoints[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
			}
			else {
				waypoints[i].GetComponent<MeshRenderer>().material.color = Color.yellow;
			}
		}
	}
}