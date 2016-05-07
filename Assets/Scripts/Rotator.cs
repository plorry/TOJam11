using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float rotxSpeed = 1.0f;
	public float rotySpeed = 1.0f;
	public float rotzSpeed = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotxSpeed, rotySpeed, rotzSpeed);
	}
}
