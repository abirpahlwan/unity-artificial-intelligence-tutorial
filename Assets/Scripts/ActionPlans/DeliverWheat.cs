﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverWheat : GoapAction {

	private bool isCompleted = false;
	private float startTime = 0;
	[SerializeField] private float workDuration = 2.0f;
	public Inventory windmillInventory;

	public DeliverWheat() {
		addPrecondition("hasWheat", true);
		addEffect("doJob", true);
		name = "DeliverWheat";
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
			windmillInventory.flourLevel += 1;

			isCompleted = true;

			Debug.Log("Finished: " + name);
		}

		return true;
	}
}
