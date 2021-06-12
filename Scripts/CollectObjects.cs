using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class CollectObjects : MonoBehaviour
{

    public AudioClip coinSource;

    int score;
    float timer;
    bool startDeleteMessage;
    void Start()
    {
        print("Start method");
        score = 0;
        timer = 0.0f;
        startDeleteMessage = false;
        GameObject.Find("userMessageUI").GetComponent<TextMeshProUGUI>().text = "";
        displayMessage("");
    }

    // Update is called once per frame
    void Update()
    {
        if (startDeleteMessage)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = "";
                timer = 0.0f;
                startDeleteMessage = false;
            }
        }
     
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.tag == "pick_up")
        {
            string nameOfObject = hit.collider.gameObject.name;

            print("Collided with " + nameOfObject);
            score++;
            gameObject.GetComponent<AudioSource>().clip = coinSource;
            gameObject.GetComponent<AudioSource>().Play();
            displayMessage("Score: " + score);
            Destroy(hit.collider.gameObject);

            print("Score: " + score);
            if (score > 9) SceneManager.LoadScene("Level2");

            else if (score > 14) SceneManager.LoadScene("Level3");

            else if (score >18) SceneManager.LoadScene("EndScreen");

        }
    }

    void displayMessage(string theMessage)
    {
	GameObject.Find("userMessageUI").GetComponent<TextMeshProUGUI>().text = theMessage;
    }	
}
