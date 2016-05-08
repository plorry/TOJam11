using UnityEngine;
using System.Collections;

public class blastAirlock : MonoBehaviour {

	public GameObject[] blastables;
	public GameObject[] lights;
	public GameObject airlockDoor;
	public GameObject vacuumLocaction;

	public bool blastablesFound = false;
	public bool blastComplete = false;

	public float blastTime = 5;
	private float elapsed = 0;

	Animator animator;
	// Use this for initialization
	void Start () {
		animator = airlockDoor.GetComponent <Animator>();
		lights = GameObject.FindGameObjectsWithTag ("Light");

		if (GameManager.i.GetLevel () == 1) {
			// first level - wait a bit before blast
		}
	}
	
	// Update is called once per frame
	void Update () {
		elapsed += Time.deltaTime;
		if (blastables.Length < 1 && blastablesFound == false) {
			blastables = GameObject.FindGameObjectsWithTag ("Blastable");
			blastablesFound = true;
		}

		if (elapsed > blastTime && animator.GetBool("evacAirlock") == false) {
			animator.SetBool ("evacAirlock", true);
			for (int i = 0; i < lights.Length; i++) {
				var lightObj = lights [i].GetComponent<Light> ();
				lightObj.enabled = !lightObj.enabled;
			}
			GameObject doorDing = Instantiate(Resources.Load("Door Sound", typeof(GameObject))) as GameObject;
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen") && blastComplete == false){
			for(int i = 0; i < blastables.Length; i++){
				if(blastables[i].GetComponent<blastRegulator>()){
					var blastControl = blastables [i].GetComponent<blastRegulator> ();
					blastControl.vacuumLocation = vacuumLocaction;
					blastControl.isBlasting = true;
				}
				if(blastables[i].GetComponent<playerBlast>()){
					print ("player");
					var playerBlast = blastables[i].GetComponent<playerBlast>();
					playerBlast.vacuumLocation = vacuumLocaction;
					playerBlast.isBlasting = true;
				}

			}
			GameObject decompression = Instantiate(Resources.Load("Decompression Sound", typeof(GameObject))) as GameObject;
			blastComplete = true;
		}
	}
}
