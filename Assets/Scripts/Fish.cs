using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start() {
		speed = Random.Range(FlockManager.Instance.minimumSpeed, FlockManager.Instance.maximumSpeed);
	}

	// Update is called once per frame
	void Update() {

		Bounds bounds = new Bounds(FlockManager.Instance.transform.position, FlockManager.Instance.swimLimit);

		RaycastHit hit;

		// If gone out of bounds
		if (!bounds.Contains(this.transform.position)) {
			Vector3 direction = FlockManager.Instance.transform.position - gameObject.transform.position;
			gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,
				Quaternion.LookRotation(direction), FlockManager.Instance.rotationSpeed * Time.deltaTime);
		}
		else if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit)) {
			Vector3 direction = Vector3.Reflect(gameObject.transform.forward, hit.normal);
			gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,
				Quaternion.LookRotation(direction), FlockManager.Instance.rotationSpeed * Time.deltaTime);
		}
		else {
			if (Random.Range(0, 100) < 20) {
				ApplyRules();
			}

			if (Random.Range(0, 100) < 10) {
				speed = Random.Range(FlockManager.Instance.minimumSpeed, FlockManager.Instance.maximumSpeed);
			}
		}

		transform.Translate(0, 0, Time.deltaTime * speed);
	}

	private void ApplyRules() {
		Vector3 averageCenter = Vector3.zero;
		Vector3 averageAvoidance = Vector3.zero;
		float averageSpeed = 0.01f;
		float neighbourDistance;
		int groupSize = 0;

		// For all fishes in the flock
		foreach (var fish in FlockManager.Instance.fishes) {
			// If it ain't me
			if (fish != gameObject) {
				// How far is it from me
				neighbourDistance = Vector3.Distance(fish.transform.position, gameObject.transform.position);

				// If it's my neighbour
				if (neighbourDistance <= FlockManager.Instance.neighbourDistance) {
					averageCenter += fish.transform.position;
					groupSize++;

					// If it's in my private space
					if (neighbourDistance < 1.0f) {
						averageAvoidance += (gameObject.transform.position - fish.transform.position);
					}

					Fish anotherFish = fish.GetComponent<Fish>();
					averageSpeed += anotherFish.speed;
				}
			}
		}

		// Netflix and flock
		if (groupSize > 0) {
			averageCenter = averageCenter / groupSize +
			                (FlockManager.Instance.goal.transform.position - this.transform.position);
			speed = averageSpeed / groupSize;

			Vector3 heading = (averageCenter + averageAvoidance) - gameObject.transform.position;
			if (heading != Vector3.zero) {
				gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,
					Quaternion.LookRotation(heading), FlockManager.Instance.rotationSpeed * Time.deltaTime);
			}
		}

	}
}