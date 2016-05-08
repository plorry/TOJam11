using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager i;
	private List<string> names;
	public List<AstronautPerson> astronauts;
	public ItMe myPlayer;
	public GameObject airlockPrefab;
	public GameObject airlock;
	private int level = 1;
	public int canistersUsed = 0;
	public int peopleSaved = 0;
	public float timeSpent = 0;
	private bool hasIntroed = false;

	private float elapsed;

	
	void Awake () {
		if(!i) {
			i = this;
			DontDestroyOnLoad(gameObject);
		}else 
			Destroy(gameObject);
	}
	// Use this for initialization
	void Start () {
		Reset ();
		SetNames ();
	}
	
	// Update is called once per frame
	void Update () {
		elapsed += Time.deltaTime;
		if (elapsed > 10 && myPlayer.isActiveAndEnabled) {
			elapsed = 0;
			RandomSpeak();
		}

		if (elapsed > 1 && PickAstronaut ().myPersonality.name == "boss" && hasIntroed == false) {
			print ("boss speaks");
			AstronautPerson a = PickAstronaut();
			print (a);
			a.Say ("intro");
			hasIntroed = true;
		}
	}

	void Reset() {
		astronauts.Clear ();
	}

	public void AddAstronaut(AstronautPerson astronaut) {
		astronauts.Add (astronaut);
	}

	public AstronautPerson PickAstronaut() {
		int i = Random.Range(0, astronauts.Count);
		return astronauts[i];
	}

	private void RandomSpeak() {
		AstronautPerson astronaut = PickAstronaut ();
		int i = Random.Range (1, 6);
		astronaut.Say("talk0" + i);
	}

	public void SetPlayer(ItMe player) {
		myPlayer = player;
	}

	public void StartLevel() {

	}

	public void MakeAirlockExit() {

	}

	public void SetNames() {
		names = new List<string>();

		names.Add ("chip");
		names.Add ("suit");
		names.Add ("nasal");
		names.Add ("nature");
		names.Add ("timid");
		names.Add ("owe");
		names.Add ("typea");
		names.Add ("scat");
		names.Add ("positive");
		names.Add ("worry");
	}

	public string PopName () {
		int i = Random.Range (0, names.Count);
		string name = names [i];
		names.Remove (name);

		return name;
	}

	public void NextLevel(){
		level++;
		Reset ();
	}

	public int GetLevel() {
		return level;
	}


}
