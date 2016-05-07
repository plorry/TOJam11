using UnityEngine;
using System.Collections;

public class EyeCanvas : MonoBehaviour {
	public ItMe player;
	public CanisterUI canisterPrefab;
	private RectTransform myRect;
	private float width;
	private float height;
	private float scale;
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
	
	}

	void GenerateFuel() {
		for (var i = 0; i < player.GetMaxFuel(); i++) {
			CanisterUI canister = (CanisterUI)Instantiate(canisterPrefab);
			canister.transform.SetParent(transform, false);
			canister.transform.localPosition = new Vector3((i * 100 - 240) * scale,-(height / 2) + (100 * scale),0);
			canister.canisterNumber = i + 1;
			canister.myPlayer = player;
			canister.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 0);
		}
	}
}
