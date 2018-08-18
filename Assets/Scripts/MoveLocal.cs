using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocal : MonoBehaviour {
	public Transform goal;
	public float speed = 0.5f;
	public float accuracy = 0.5f;
	public float rotSpeed = 0.5f;

	void Start() {

	}

	void LateUpdate() {
		Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
		Vector3 direction = lookAtGoal - this.transform.position;
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction),
			Time.deltaTime * rotSpeed);

		if (Vector3.Distance(lookAtGoal, this.transform.position) > accuracy)
			this.transform.Translate(0, 0, speed * Time.deltaTime);
	}
}