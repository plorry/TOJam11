using UnityEngine;
using System.Collections;

public class ItMe : MonoBehaviour {

	public Rigidbody rb;
	public Camera eyes;
	private Vector3 startForce;
	private Vector3 targetVelocity;
	private Vector3 velocityIncrement;
	private bool jetting = false;
	public blastRegulator[] targets;
	public int fuelUnits;
	public int maxFuel = 6;
	public Canvas leftEye;
	public Canvas rightEye;
	public GameObject airlockExit;
	public bool isWon = false;
	public GameObject otherExit;
	// prefabs
	public CursorMark cursorPrefab;
	public CardboardAudioSource jetSound;
	public ExitMark exitPrefab;

	void Awake () {
		enabled = false;
	}
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		//GenerateTargets ();
		SetRigidBody ();
		FillTank ();
		Destroy (airlockExit);

		ExitMark exitMark = (ExitMark)Instantiate(exitPrefab, eyes.transform.forward, Quaternion.identity);
		exitMark.target = otherExit;
		exitMark.transform.SetParent(transform);
		exitMark.myCamera = gameObject;
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
				GameManager.i.canistersUsed++;
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
		if (isActiveAndEnabled) {
			foreach (Transform child in collisionInfo.transform) {
				if (child.transform.tag == "Friend") {
					AstronautPerson a = collisionInfo.gameObject.GetComponent<AstronautPerson>();
					a.transform.SetParent(transform);
					a.myPersonality.transform.SetParent (transform);
					a.Say("saved");
					GameManager.i.astronauts.Remove(a);
					Destroy (collisionInfo.gameObject);
					rb.velocity = Vector3.zero;
					return;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (isActiveAndEnabled) {
			if (other.transform.tag == "Finish") {
				Win ();
				Application.LoadLevel("Airlock");
			}
		}
	}

	void Win() {
		if (!isWon) {
			isWon = true;
			GameManager.i.NextLevel();
		}
	}

	// Set our current fuel to the max
	void FillTank() {
		fuelUnits = maxFuel;
	}
	
	void GenerateTargets() {
		for (int i = 0; i < targets.Length; i++) {
			AddTarget(targets[i]);
		}

	}

	// Add a collectible target
	public void AddTarget(blastRegulator target) {
		CursorMark cursorMark = (CursorMark)Instantiate(cursorPrefab, eyes.transform.forward, Quaternion.identity);
		cursorMark.target = target;
		cursorMark.transform.SetParent(transform);
		cursorMark.myCamera = gameObject;
	}

	public int GetMaxFuel() {
		return maxFuel;
	}

	public int GetCurrentFuel() {
		return fuelUnits;
	}

	// Prep the rigidbody for float mode
	public void SetRigidBody() {
		rb.drag = 0;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
}
