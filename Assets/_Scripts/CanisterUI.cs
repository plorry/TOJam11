using UnityEngine;
using System.Collections;

public class CanisterUI : MonoBehaviour {
	public ItMe myPlayer;
	public int canisterNumber;
	private CanvasRenderer im;
	// Use this for initialization
	void Start () {
		im = GetComponent<CanvasRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (myPlayer.GetCurrentFuel () < canisterNumber) {
			// This fuel unit is used up
			im.SetAlpha (0);
		} else {
			im.SetAlpha(1);
		}
	}
}
