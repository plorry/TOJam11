using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreSummary : MonoBehaviour {

	public GameObject summaryObj;
	Text summary;
	// Use this for initialization
	void Awake () {
		summary = summaryObj.GetComponent<Text>();
	}

	void Start () {
		summary.text = "People Rescued: " + GameManager.i.peopleSaved + "\nCanisters Used: " + GameManager.i.canistersUsed + "\nTime Spent: " + GameManager.i.timeSpent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
