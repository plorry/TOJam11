using UnityEngine;
using System.Collections;

public class EyeCanvas : MonoBehaviour {
	public ItMe player;
	public CanisterUI canisterPrefab;
	private RectTransform myRect;
	private float width;
	private float height;
	private float scale;
	private bool shownWinText = false;
	private bool showLoseText = false;
	public GameObject winText;
	public GameObject loseText;
	// Use this for initialization
	void Start () {
		myRect = GetComponent<RectTransform> ();
		width = myRect.rect.width;
		height = myRect.rect.height;
		scale = width / 1000;

		GenerateFuel ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.isWon && shownWinText == false) {
			Complete();
		}
		if (player.isLose) {
			Lose ();
		}
	}

	void GenerateFuel() {
		for (var i = 0; i < player.GetMaxFuel(); i++) {
			CanisterUI canister = (CanisterUI)Instantiate(canisterPrefab);
			canister.transform.SetParent(transform, false);
			canister.transform.localPosition = new Vector3((i * 100 - 240) * scale,-(height / 2) + (200 * scale),0);
			canister.canisterNumber = i + 1;
			canister.myPlayer = player;
			canister.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 0);
		}
	}

	void Complete() {
		if (!shownWinText) {
			GameObject text = Instantiate (winText);
			text.transform.SetParent (transform, false);
			text.transform.localPosition = new Vector3 ((10) * scale, -(height / 2) + (700 * scale), 0);
			shownWinText = true;
		}
	}	

	void Lose() {
		if (!showLoseText) {
			GameObject text = Instantiate (loseText);
			text.transform.SetParent (transform, false);
			text.transform.localPosition = new Vector3 ((10) * scale, -(height / 2) + (700 * scale), 0);
			showLoseText = true;
		}
	}
}
