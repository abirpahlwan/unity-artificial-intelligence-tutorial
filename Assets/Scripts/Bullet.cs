using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject explosion;

	void OnCollisionEnter(Collision col) {
		GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
		Destroy(e, 1.5f);
		Destroy(this.gameObject);
	}
}