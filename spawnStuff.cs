using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnStuff : MonoBehaviour {
	public GameObject player;
	public GameObject toSpawn;
	private float distance = 3.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(spawnTheStuff());
	}
	
	IEnumerator spawnTheStuff(){
		yield return new WaitForSeconds(5);
        Debug.Log("Spawning stuff...");
		Vector3 newObjectLocation = (player.transform.forward * distance) + player.transform.position;
        Instantiate(toSpawn, newObjectLocation, Quaternion.identity);		
	}
	// Update is called once per frame
	void Update () {
		
	}
}
