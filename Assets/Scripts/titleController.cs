using UnityEngine;
using System.Collections;

public class titleController : MonoBehaviour {

	public GameObject player;
	public GameObject astronaut;
	public GameObject glowFX;

	public Transform target;
	RaycastHit hit;
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = astronaut.GetComponent<Animator> ();
		animator.SetBool ("hasBeenBlasted", true);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lookDir = player.transform.TransformDirection (Vector3.forward);
		if(Physics.Raycast(player.transform.position, lookDir, out hit)){
			target = hit.transform;
			if(target.tag == "Start Button" && glowFX.transform.localScale.x <= 150 && glowFX.transform.localScale.y <= 25){
				glowFX.transform.localScale += new Vector3 (0.4f, 0.2f, 0f);
			} else if(glowFX.transform.localScale.x >= 140 && glowFX.transform.localScale.y >= 20){
				glowFX.transform.localScale -= new Vector3 (0.4f, 0.2f, 0f);
			}
		}

		if (Cardboard.SDK.Triggered) {
			if(target.tag == "Start Button"){
				if (!GameManager.i) {
					Application.LoadLevel("Airlock");
				}
				if (GameManager.i.GetLevel() <= 4) {
					Application.LoadLevel("Airlock");
				} else {
					Application.LoadLevel("intro");
				}
			}
			if(target.tag == "Goat"){
				GameObject goatSound = Instantiate(Resources.Load ("GoatSound", typeof(GameObject))) as GameObject;
			}
			if(target.tag == "Blastable"){
				var randomPosition = new Vector3(Random.value, Random.value, Random.value);
				var rBody = astronaut.GetComponent<Rigidbody>();
				rBody.AddForce(randomPosition * 1000);
			}
		}

		target = null;
	}
}
