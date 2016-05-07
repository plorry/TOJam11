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
	public CursorMark cursorPrefab;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		for (int i = 0; i < targets.Length; i++) {
			print ("I create");
			CursorMark cursorMark = (CursorMark)Instantiate(cursorPrefab, eyes.transform.forward, Quaternion.identity);
			cursorMark.target = targets[i];
			cursorMark.transform.SetParent(transform);
			cursorMark.myCamera = gameObject;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Cardboard.SDK.Triggered) {
			targetVelocity = eyes.transform.forward * 3;
			velocityIncrement = (targetVelocity - rb.velocity) / 50;
			jetting = true;
		}

		if (jetting) {
			if (rb.velocity != targetVelocity) {
				rb.velocity += velocityIncrement;
			} else {
				print ("arrived!");
				jetting = false;
			}	
		}

	}

	void OnCollisionEnter(Collision collisionInfo){
		if (collisionInfo.collider.tag == "Friend") {
			print (collisionInfo.collider.gameObject);
			Destroy(collisionInfo.collider.gameObject);
		}
	}
}
