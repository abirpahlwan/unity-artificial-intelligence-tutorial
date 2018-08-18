using UnityEngine;

public class AIRobotGuardController : MonoBehaviour {
	public Transform player;
	private Animator animator;

	private float rotationSpeed = 2.0f;
	private float speed = 2.0f;
	private float visionDist = 20.0f;
	private float visionAngle = 30.0f;
	private float shootDistance = 5.0f;

	string state = "IDLE";

	// Use this for initialization
	void Start() {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		Vector3 direction = player.position - gameObject.transform.position;
		float angle = Vector3.Angle(direction, gameObject.transform.forward);

		if (direction.magnitude < visionDist && angle < visionAngle) {
			direction.y = 0;

			gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,
				Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

			if (direction.magnitude > shootDistance) {
				if (state != "RUNNING") {
					state = "RUNNING";
					animator.SetTrigger("isRunning");
				}
			}
			else {
				if (state != "SHOOTING") {
					state = "SHOOTING";
					animator.SetTrigger("isShooting");
				}
			}

		}
		else {
			if (state != "IDLE") {
				state = "IDLE";
				animator.SetTrigger("isIdle");
			}
		}

		if (state == "RUNNING")
			gameObject.transform.Translate(0, 0, Time.deltaTime * speed);
	}
}