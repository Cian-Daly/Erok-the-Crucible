using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNPCs : MonoBehaviour
{
     public GameObject NPC;
     [SerializeField]
     [Range(0.5f,5.0f)]
     float spawningFrequency;  
     public float timer;

    // Start is called before the first frame update
    void Start()
    {
        //spawningFrequency = 2;
        int level = PlayerPrefs.GetInt("level");
        if (level == 1) spawningFrequency = 1.0f;
        else if (level == 2) spawningFrequency = 3.0f;
        else spawningFrequency = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawningFrequency)
        {
            timer = 0.0f;
            GameObject npc = (GameObject)(Instantiate(NPC, gameObject.transform.position, Quaternion.identity));
            npc.GetComponent<ControlNPC>().myType = ControlNPC.NPC_TYPE.SLOW;
        }
    }
}
