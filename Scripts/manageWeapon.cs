using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class manageWeapon : MonoBehaviour {

	private const int WEAPON_GUN = 0;
	//private const int WEAPON_AUTO_GUN = 1;

	int activeWeapon = WEAPON_GUN;
	private float timer;
	private bool timerStarted;
	bool canShoot = true;
	int currentWeapon;

	bool [] hasWeapon;
	int [] ammos;
	int [] maxAmmos;
	float [] reloadTimes;
	string [] weaponName;


	Camera playersCamera;
	Ray rayFromPlayer;
	RaycastHit hit;
	public GameObject sparksAtImpact;

	// Use this for initialization
	void Start () {
		
		ammos = new int [1];
		hasWeapon = new bool [1];
		maxAmmos = new int[1];
		reloadTimes = new float[1];
		weaponName = new string[1];

		hasWeapon [WEAPON_GUN] = true;

		weaponName [WEAPON_GUN] = "Gun";

		ammos [WEAPON_GUN] = 20;

		maxAmmos [WEAPON_GUN] = 20;

		playersCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

		if (timerStarted)
		{
			timer += Time.deltaTime;
			if (timer >= reloadTimes[currentWeapon])
			{
				timerStarted = false;
				canShoot = true;
			}
		}

		rayFromPlayer = playersCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
		Debug.DrawRay(rayFromPlayer.origin, rayFromPlayer.direction *100, Color.red);

		if (Input.GetButtonDown("Fire1"))
		{
			if ((currentWeapon == WEAPON_GUN && ammos[currentWeapon] >=1 && canShoot))
			{
				GetComponent<AudioSource>().Play();
				ammos[currentWeapon]--;
				
			if (Physics.Raycast(rayFromPlayer, out hit, 100))
			{
				print("The object "+hit.collider.gameObject.name+" is infront of the player");
				Vector3 positionOfImpact = hit.point;
				Instantiate(sparksAtImpact, positionOfImpact, Quaternion.identity);
				GameObject objectTargeted;
				if (hit.collider.gameObject.tag == "target")
				{
					objectTargeted = hit.collider.gameObject;
					objectTargeted.GetComponent<manageNPC>().gotHit();
				}
			}

			canShoot = false;
			timer = 0.0f;
			timerStarted = true;
			}

		    }

			GameObject.Find("userInfo").GetComponent<TextMeshProUGUI>().text = weaponName[currentWeapon]+"("+ammos[currentWeapon]+")";
	}
		

	public void manageCollision(ControllerColliderHit hit)
	{
		if (hit.collider.gameObject.tag == "ammo_gun")
		{
			print("Collected more ammo");

			while (ammos[currentWeapon] < maxAmmos[currentWeapon]) 
			{
			ammos[currentWeapon]++;
			}
			Destroy (hit.collider.gameObject);
		}
	}
}
