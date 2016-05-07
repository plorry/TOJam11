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
	public GameObject player;
	public GameObject[] otherNauts;
	// Use this for initialization
	void Awake () {
		astroSpawners = GameObject.FindGameObjectsWithTag("Astronaut Spawner");
	}

	void Start () {
		List<GameObject> positionList = astroSpawners.ToList ();
		for(int i = 0; i < spawnCount; i++){
			GameObject astronaut = Instantiate(Resources.Load("astronaut", typeof(GameObject))) as GameObject;
			int astroPosition = Random.Range (0, positionList.Count);
			astronaut.transform.position = positionList[astroPosition].transform.position;
			positionList.RemoveAt(astroPosition);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
