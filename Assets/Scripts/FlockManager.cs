using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {
	private static FlockManager _instance;

	public static FlockManager Instance
	{
		get { return _instance; }
	}

	[SerializeField] private GameObject fishPrefab;

	[Header("Flock Settings")] public GameObject goal;
	public int fishCount = 40;
	public GameObject[] fishes;

	public Vector3 swimLimit = new Vector3(5.0f, 2.5f, 5.0f);

//	Color water = new Color(0x90, 0xB6, 0xF2, 0x00); //90B6F200

	[Header("Fish Settings")] [Range(0.0f, 5.0f)]
	public float minimumSpeed = 0.0f;

	[Range(0.0f, 5.0f)] public float maximumSpeed = 5.0f;

	[Range(1.0f, 10.0f)] public float neighbourDistance = 1.0f;

	[Range(1.0f, 10.0f)] public float rotationSpeed = 2.0f;

	void Awake() {
		DontDestroyOnLoad(this);
		if (_instance == null) {
			_instance = this;
		}
		else {
			DestroyObject(gameObject);
		}
	}

	void Start() {
		fishes = new GameObject[fishCount];

		for (int i = 0; i < fishCount; i++) {
			Vector3 fishPosition = this.transform.position + new Vector3(Random.Range(-swimLimit.x, swimLimit.x),
				                       Random.Range(-swimLimit.y, swimLimit.y),
				                       Random.Range(-swimLimit.z, swimLimit.z));

			fishes[i] = (GameObject) Instantiate(fishPrefab, fishPosition, Quaternion.identity);
		}

	}

	// Update is called once per frame
	void Update() {
		if (Random.Range(0, 100) < 10) {
			goal.transform.position = this.transform.position + new Vector3(Random.Range(-swimLimit.x, swimLimit.x),
				                          Random.Range(-swimLimit.y, swimLimit.y),
				                          Random.Range(-swimLimit.z, swimLimit.z));

		}
	}
}