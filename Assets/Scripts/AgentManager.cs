using UnityEngine;

public class AgentManager : MonoBehaviour {
	GameObject[] agents;

	// Use this for initialization
	void Start() {
		agents = GameObject.FindGameObjectsWithTag("Agent");
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
				foreach (GameObject agent in agents) {
					agent.GetComponent<AgentControl>().agent.SetDestination(hit.point);
				}
			}
		}
	}
}