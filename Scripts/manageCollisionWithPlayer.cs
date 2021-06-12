using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageCollisionWithPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//print("hello there");
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//print("pain");
		transform.GetChild(0).GetComponent<manageWeapon>().manageCollision(hit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
