using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour {
	// public GameObject[] waypoints;
	public UnityStandardAssets.Utility.WaypointCircuit circuit;
	private int currentWayPoint = 0;

	[SerializeField] private float speed = 1.0f;
	[SerializeField] private float accuracy = 0.5f;
	[SerializeField] private float rotationSpeed = 0.5f;

	private void Start() {
		// waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
	}

	// Update is called once per frame
	private void Update() {
		if (circuit.Waypoints.Length == 0)
			return;

		Vector3 lookAtGoal = new Vector3(circuit.Waypoints[currentWayPoint].transform.position.x,
			this.transform.position.y, circuit.Waypoints[currentWayPoint].transform.position.z);
		Vector3 direction = lookAtGoal - this.transform.position;
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction),
			Time.deltaTime * rotationSpeed);

		if (direction.magnitude < accuracy) {
			currentWayPoint++;
			if (currentWayPoint >= circuit.Waypoints.Length) {
				currentWayPoint = 0;
			}
		}

		this.transform.Translate(0, 0, Time.deltaTime * speed);
	}
}