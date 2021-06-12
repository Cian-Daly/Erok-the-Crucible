using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageNPC : MonoBehaviour {

	int health;
	public GameObject spark;

	// Use this for initialization
	void Start () {
		health = 100;
	}

	public void gotHit()
	{
		health -= 50;
		if (health <=0) this.Destroy(); 
	}

	public void Destroy()
	{
		GameObject lastSpark = (GameObject) (Instantiate(spark, transform.position, Quaternion.identity));
		Destroy(lastSpark, 0.5f);
		Destroy(gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
