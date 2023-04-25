using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timerTime;
    public Text timerText;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        timerTime = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            timerTime += Time.deltaTime;
            DisplayTime(timerTime);
        }
    }

    void DisplayTime(float displayTime)
    {
        //float minutes = Mathf.FloorToInt(displayTime / 60);
        //float minutes = Mathf.FloorToInt(displayTime % 60);
        //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = "Time: " + (timerTime >= 60 ? Mathf.FloorToInt(timerTime / 60).ToString() + "m " : "") + $"{timerTime % 60:0.000}s";
    }
}
