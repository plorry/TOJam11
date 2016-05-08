using UnityEngine;
using System.Collections;

public class CursorMark : MonoBehaviour {

	public GameObject myCamera;
	public blastRegulator target;
	private Vector3 myVector;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!target) {
			Destroy(gameObject);
			return;
		}

		transform.LookAt (myCamera.transform.position);
		myVector = target.transform.position - myCamera.transform.position;
		transform.localPosition = (myVector / myVector.magnitude);
	}
}
