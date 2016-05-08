using UnityEngine;
using System.Collections;

public class blastRegulator : MonoBehaviour {

	public GameObject vacuumLocation;
	public float vacuumForce = 20f;
	public float explosiveForce;
	public float rDrag;
	public Vector3 randomDir;

	public bool isBlasting = false;
	public bool hasBeenRegulated = false;

	Animator animator;

	// Use this for initialization
	void Start () {
		if (gameObject.GetComponent<Animator> ()) {
			animator = gameObject.GetComponent<Animator> ();
		}

		int level = GameManager.i.GetLevel ();
		rDrag = Random.Range (1.5f - (level * 0.2f), 2.5f - (level * 0.2f));
		explosiveForce = Random.Range (40 + (20 * level), 60 + (20 * level));
		randomDir = new Vector3 (Random.Range(-20f - (level * 3f), 20f + (level * 3f)), Random.Range(-19 - (level * 3f), 21 + (level * 3f)), Random.Range (-20, 0));
	}
	
	// Update is called once per frame
	void Update () {
		if (isBlasting == true && hasBeenRegulated == false) {
			Blast ();
		}
	}

	void Blast () {
		var rBody = gameObject.GetComponent<Rigidbody> ();
		if (vacuumLocation != null) {
			var bearing = vacuumLocation.transform.position - transform.position;
			rBody.AddForce (bearing * vacuumForce);
			rBody.useGravity = false;
		}
		if (GetAstronaut ()) {
			AstronautPerson astronaut = GetAstronaut();
			astronaut.Say("whoa");
		}
	}

	void OnTriggerStay (Collider other){
		if (other.tag == "Blast Regulator") {
			hasBeenRegulated = true;
			var rBody = gameObject.GetComponent<Rigidbody> ();
			rBody.AddExplosionForce (explosiveForce, randomDir, 200);
			if (gameObject.GetComponent<Animator>()) {
				animator.SetBool ("hasBeenBlasted", true);
			}
		}
	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Blast Regulator") {
			var rBody = gameObject.GetComponent<Rigidbody> ();
			rBody.drag = rDrag;
		}
	}

	public Personality GetPersonality() {
		if (gameObject.GetComponent<AstronautPerson> () != null) {
			return gameObject.GetComponent<AstronautPerson> ().myPersonality;
		} else {
			return null;
		}
	}

	public AstronautPerson GetAstronaut() {
		if (gameObject.GetComponent<AstronautPerson> () != null) {
			return gameObject.GetComponent<AstronautPerson> ();
		} else {
			return null;
		}
	}
}
