using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlour : GoapAction {

	private bool isCompleted = false;
	private float startTime = 0;
	[SerializeField] private float workDuration = 2.0f;

	public PickupFlour() {
		addPrecondition("hasStock", true);
		addPrecondition("hasFlour", false);
		addEffect("hasFlour", true);
		name = "PickupFlour";
	}

	public override void reset() {
		isCompleted = false;
		startTime = 0;
	}

	public override bool isDone() {
		return isCompleted;
	}

	public override bool checkProceduralPrecondition(GameObject agent) {
		return true;
	}

	public override bool requiresInRange() {
		return true;
	}

	public override bool perform(GameObject agent) {
		if (startTime == 0) {
			startTime = Time.time;
			
			Debug.Log("Starting: " + name);
		}

		if (Time.time - startTime > workDuration) {
			this.GetComponent<Inventory>().flourLevel += 5;
			this.GetComponent<Worker>().windmillInventory.flourLevel -= 5;

			isCompleted = true;

			Debug.Log("Finished: " + name);
		}

		return true;
	}
}
