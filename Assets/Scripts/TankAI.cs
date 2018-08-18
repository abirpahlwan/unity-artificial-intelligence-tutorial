using UnityEngine;

public class TankAI : MonoBehaviour {
	private Animator animator;
	[SerializeField] private GameObject turret;
	[SerializeField] private GameObject player;

	[SerializeField] private GameObject bulletPrefab;

	void Start() {
		animator = GetComponent<Animator>();
	}

	void Update() {
		animator.SetFloat("distance", Vector3.Distance(gameObject.transform.position, player.transform.position));
	}

	private void Fire() {
		GameObject bullet = Instantiate(bulletPrefab, turret.transform.position, turret.transform.rotation);
		bullet.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
	}

	public void StartFiring() {
		InvokeRepeating("Fire", 0.5f, 0.5f);
	}

	public void StopFiring() {
		CancelInvoke("Fire");
	}

	public GameObject GetPlayer() {
		return player;
	}
}