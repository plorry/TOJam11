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
	
	Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		rDrag = Random.Range (1.25f, 1.5f);
		explosiveForce = Random.Range (75, 80);
		randomDir = new Vector3 (Random.Range(-2, 2), Random.Range(0, 2), Random.Range (-1, 0));
	}
	
	// Update is called once per frame
	void Update () {
		if (isBlasting == true && hasBeenRegulated == false) {
			Blast ();
		}
	}
	
	void Blast () {
		var rBody = gameObject.GetComponent<Rigidbody> ();
		var bearing = vacuumLocation.transform.position - transform.position;
		rBody.AddForce (bearing * vacuumForce);
		rBody.useGravity = false;
	}
	
	void OnTriggerStay (Collider other){
		if (other.tag == "Blast Regulator") {
			hasBeenRegulated = true;
			var rBody = gameObject.GetComponent<Rigidbody> ();
			rBody.AddExplosionForce (explosiveForce, randomDir, 50);
			animator.SetBool ("hasBeenBlasted", true);
		}
	}
	
	void OnTriggerExit (Collider other){
		if (other.tag == "Blast Regulator") {
			var rBody = gameObject.GetComponent<Rigidbody> ();
			rBody.drag = rDrag;
		}
	}
}