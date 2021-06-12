using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Use this for initialization
    float timeLimit = 120;
    float time = 0.0f;
    int seconds, minutes;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        seconds = (int)(time % 60);
        minutes = (int)(time / 60);

        print(minutes + ":" + seconds);
        GameObject.Find("timerUI").GetComponent<TextMeshProUGUI>().text = minutes + ":" + seconds;
        if (time > (timeLimit - 2))
        {
            GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = "Time UP!";


        }
        if (time > timeLimit)
        {
            SceneManager.LoadScene("level3");

        }


    }
}