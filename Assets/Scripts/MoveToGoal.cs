using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToGoal : MonoBehaviour {
	public float speed = 2f;
	public float accuracy = 0.5f;
	public Transform goal;

	void Start() {

	}

	void LateUpdate() {
		Vector3 direction = goal.position - this.transform.position;
		this.transform.LookAt(goal.position);
		Debug.DrawRay(this.transform.position, direction, Color.red);

		if (direction.magnitude > accuracy)
			this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
	}
}