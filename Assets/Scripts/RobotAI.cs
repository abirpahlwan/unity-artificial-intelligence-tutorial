using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Panda;
using Random = UnityEngine.Random;

public class RobotAI : MonoBehaviour {
	public Transform player;
	public Transform bulletSpawn;
	public Slider healthBar;
	public GameObject bulletPrefab;

	private NavMeshAgent agent;
	public Vector3 destination; // The movement destination.
	public Vector3 target; // The position to aim to.
	private float health = 100.0f;
	private float rotationSpeed = 5.0f;

	private float visibleRange = 80.0f;
	private float shotRange = 40.0f;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
//		agent.stoppingDistance = shotRange - 5; //for a little buffer
		InvokeRepeating("UpdateHealth", 5, 0.5f);
	}

	void Update() {
		Vector3 healthBarPos = Camera.main.WorldToScreenPoint(transform.position);
		healthBar.value = (int) health;
		healthBar.transform.position = healthBarPos + new Vector3(0, 60, 0);
	}

	private void UpdateHealth() {
		if (health < 100)
			health++;
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("bullet")) {
			health -= 10;
		}
	}

	// Behaviours
	[Task]
	public void PickDestination(float x, float z) {
		destination = new Vector3(x, 0, z);
		agent.SetDestination(destination);
		Task.current.Succeed();
	}
	
	[Task]
	public void PickRandomDestination() {
		destination = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
		agent.SetDestination(destination);
		Task.current.Succeed();
	}

	[Task]
	public void SetTargetDestination() {
		destination = target;
		agent.SetDestination(destination);
		Task.current.Succeed();
	}

	[Task]
	public void MoveToDestination() {
		if (Task.isInspected) {
			Task.current.debugInfo = String.Format("t={0:0.00}", Time.time);
		}

		if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending) {
			Task.current.Succeed();
		}
	}

	[Task]
	public void TargetPlayer() {
		target = player.transform.position;
		Task.current.Succeed();
	}

	[Task]
	public bool Turn(float angle) {
		var p = this.transform.position + Quaternion.AngleAxis(angle, Vector3.up) * this.transform.forward;
		this.target = p;
		this.target.y = this.transform.position.y;
		return true;
	}

	[Task]
	public void LookAtTarget() {
		Vector3 direction = target - transform.position;
		
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

		if (Task.isInspected) {
			Task.current.debugInfo = string.Format("angle={0}", Vector3.Angle(transform.forward, direction));
		}

		if (Vector3.Angle(transform.forward, direction) < 5.0f) {
			Task.current.Succeed();
		}
	}

	[Task]
	public bool Fire() {
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
		bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 2000);
		
		return true;
	}

	[Task]
	public bool SeePlayer() {
		Vector3 playerDistance = player.transform.position - this.transform.position;

		RaycastHit hit;
		bool isWall = false;
		
		if (Physics.Raycast(this.transform.position, playerDistance, out hit)) {
			if (hit.collider.gameObject.CompareTag("wall")) {
				isWall = true;
			}
		}
		
		if (Task.isInspected) {
			Task.current.debugInfo = string.Format("Wall Ahead: {0}", isWall);
		}

		return playerDistance.magnitude < visibleRange && !isWall;
	}

	[Task]
	public void TakeCover() {
		Vector3 awayFromPlayer = this.transform.position - player.transform.position;
		destination = this.transform.position + awayFromPlayer * 2;
		agent.SetDestination(destination);
		Debug.Log("Destination: " + destination);
		Task.current.Succeed();
	}

	[Task]
	public bool Explode() {
		Destroy(healthBar.gameObject);
		Destroy(this.gameObject);
		return true;
	}

	[Task]
	public bool ShotLineUp() {
		Vector3 distance = target - this.transform.position;
		return distance.magnitude < shotRange && Vector3.Angle(this.transform.forward, distance) < 5.0f;
	} 

	[Task]
	public bool IsHealthLessThan(float health) {
		return this.health < health;
	}

	[Task]
	public bool InDanger(float minimumDistance) {
		Vector3 playerDistance = player.transform.position - this.transform.position;
		return (playerDistance.magnitude < minimumDistance);
	}
}
