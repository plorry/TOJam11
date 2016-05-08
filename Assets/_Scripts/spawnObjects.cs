using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class spawnObjects : MonoBehaviour {

	public int spawnCount = 2;

	public GameObject playerSpawn;
	public GameObject[] astroSpawners;
	public GameObject[] junkSpawners;
	public GameObject[] junkTypes;
	public ItMe playerPrefab;
	public ItMe player;
	public GameObject[] otherNauts;
	private List<GameObject> positionList;

	public blastRegulator astroPrefab;
	// Use this for initialization
	void Awake () {
	}

	void Start () {
		SetupLevel (GameManager.i.GetLevel());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GeneratePeople(int count) {
		if (GameManager.i.GetLevel () == 1) {
			SpawnPerson (playerSpawn);
		} else {
			for (int i = 0; i < count; i++) {
				int astroPosition = Random.Range (0, positionList.Count);
				SpawnPerson (positionList [astroPosition]);
				positionList.RemoveAt (astroPosition);
			}
		}
	}
	// Setup the level
	public void SetupLevel(int challenge){
		ResetPositions ();
		//SpawnPlayer ();
		positionList = astroSpawners.ToList ();
		GeneratePeople (challenge);
	}

	private void SpawnPerson(GameObject position) {
		blastRegulator astronaut = (blastRegulator)Instantiate(astroPrefab);
		astronaut.transform.position = position.transform.position;
		astronaut.transform.rotation = position.transform.rotation;
		player.AddTarget(astronaut);
		GameManager.i.AddAstronaut(astronaut.GetAstronaut());
	}

	public void ResetPositions() {
		astroSpawners = GameObject.FindGameObjectsWithTag("Astronaut Spawner");
	}

	private void SpawnPlayer() {
		player = (ItMe)Instantiate (playerPrefab, playerSpawn.transform.position, Quaternion.identity);
		GameManager.i.SetPlayer (player);
	}
}
