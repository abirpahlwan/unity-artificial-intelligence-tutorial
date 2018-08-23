using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeBread : GoapAction {
	
	private bool isCompleted = false;
	private float startTime = 0;
	public float workDuration = 2.0f;

	public BakeBread() {
		addPrecondition("hasFlour", true);
		addEffect("doJob", true);
		name = "BakeBread";
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
			this.GetComponent<Inventory>().flourLevel -= 2;
			this.GetComponent<Inventory>().breadLevel += 1;
			
			isCompleted = true;
			
			Debug.Log("Finished: " + name);
		}

		return true;
	}
}
