using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    int health;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    public void gotAttacked()
    {
        health -= 20;
        if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
