using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverBread : GoapAction {
	
	private bool isCompleted = false;
	private float startTime = 0;
	public float workDuration = 2.0f;

	public DeliverBread() {
		addPrecondition("hasDelivery", true);
		addEffect("doJob", true);
		name = "DeliverBread";
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
			this.GetComponent<Inventory>().breadLevel -= 5;
			
			isCompleted = true;
			
			Debug.Log("Finished: " + name);
		}

		return true;
	}
}
