using UnityEngine;
using System.Collections;

public class ItMe : MonoBehaviour {

	public Rigidbody rb;
	public Camera eyes;
	private Vector3 startForce;
	private Vector3 targetVelocity;
	private Vector3 velocityIncrement;
	private bool jetting = false;
	public GameObject[] targets;
	public int fuelUnits;
	public int maxFuel = 6;
	public Canvas leftEye;
	public Canvas rightEye;
	// prefabs
	public CursorMark cursorPrefab;
	public CardboardAudioSource jetSound;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		GenerateTargets ();
		FillTank ();
	}

	// Update is called once per frame
	void Update () {
		if (Cardboard.SDK.Triggered) {
			if (fuelUnits > 0) {
				targetVelocity = eyes.transform.forward;
				velocityIncrement = (targetVelocity - rb.velocity) / 50;
				jetting = true;
				fuelUnits--;
				jetSound.Play();
			}
		}

		if (jetting) {
			if (rb.velocity != targetVelocity) {
				rb.velocity += velocityIncrement;
			} else {
				jetting = false;
			}	
		}

	}

	void OnCollisionEnter(Collision collisionInfo){
		if (collisionInfo.collider.tag == "Friend") {
			Destroy(collisionInfo.collider.gameObject);
		}
	}

	// Set our current fuel to the max
	void FillTank() {
		fuelUnits = maxFuel;
	}
	
	void GenerateTargets() {
		for (int i = 0; i < targets.Length; i++) {
			CursorMark cursorMark = (CursorMark)Instantiate(cursorPrefab, eyes.transform.forward, Quaternion.identity);
			cursorMark.target = targets[i];
			cursorMark.transform.SetParent(transform);
			cursorMark.myCamera = gameObject;
		}
	}

	public int GetMaxFuel() {
		return maxFuel;
	}

	public int GetCurrentFuel() {
		return fuelUnits;
	}
}
