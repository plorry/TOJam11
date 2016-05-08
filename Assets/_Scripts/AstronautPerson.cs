using UnityEngine;
using System.Collections;

public class AstronautPerson : MonoBehaviour {
	public Personality personalityPrefab;
	public Personality myPersonality;
	public string[] names;
	private string[] phrases;
	private string myName;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		if (GameManager.i.GetLevel () == 1) {
			// first level - it's the boss
			myName = "boss";
			SetPersonality(myName);
			print (myPersonality.name);
			rb.transform.LookAt(GameManager.i.myPlayer.transform);
			myPersonality.LoadAudio();
			
		} else {
			PickName ();
			SetPersonality (myName);
			for (int i = 0; i < 6; i++) {
				phrases [i] = "talk0" + (i + 1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PickName() {
		myName = GameManager.i.PopName ();
		print (myName);
	}

	void SetPersonality(string name) {
		myPersonality = (Personality)Instantiate (personalityPrefab);
		myPersonality.transform.SetParent (transform);
		myPersonality.name = name;
	}

	public void Say(string phrase) {
		myPersonality.Say (phrase);
	}

	public void SayRandom() {
		int i = Random.Range (0, phrases.Length);
		Say (phrases [i]);
	}
}
