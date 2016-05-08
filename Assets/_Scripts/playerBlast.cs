using UnityEngine;
using System.Collections;

public class playerBlast : MonoBehaviour {
	
	public GameObject vacuumLocation;
	public float vacuumForce = 20f;
	public float explosiveForce;
	public float rDrag;
	public Vector3 randomDir;
	
	public bool isBlasting = false;
	public bool hasBeenRegulated = false;

	private ItMe itMe;
	private playerBlast thisScript;
	private Rigidbody rBody;

	// Use this for initialization
	void Start () {
		itMe = GetComponent<ItMe> ();
		thisScript = GetComponent<playerBlast> ();

		rDrag = Random.Range (1.25f, 1.5f);
		rBody = GetComponent<Rigidbody> ();
		explosiveForce = Random.Range (75, 80);
		randomDir = new Vector3 (Random.Range(-2, 2), Random.Range(0, 2), Random.Range (-1, 0));
		print ("It okay");
	}
	
	// Update is called once per frame
	void Update () {
		if (isBlasting == true && hasBeenRegulated == false) {
			Blast ();
		}

		if (hasBeenRegulated == true && rBody.velocity.magnitude < 0.2) {
			print ("Ready!");
			itMe.enabled = true;
			thisScript.enabled = false;
		}
	}
	
	void Blast () {
		print ("blast");
		var bearing = vacuumLocation.transform.position - transform.position;
		rBody.AddForce (bearing * vacuumForce);
		rBody.useGravity = false;
	}
	
	void OnTriggerStay (Collider other){
		if (other.tag == "Blast Regulator") {
			hasBeenRegulated = true;
			rBody.AddExplosionForce (explosiveForce, randomDir, 50);
		}
	}
	
	void OnTriggerExit (Collider other){
		if (other.tag == "Blast Regulator") {
			rBody.drag = rDrag;
		}
	}
}