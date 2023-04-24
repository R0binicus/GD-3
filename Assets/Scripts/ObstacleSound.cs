using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSound : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private AudioSource leftSound;
    [SerializeField] private AudioSource rightSound;
    [SerializeField] private AudioSource upSound;
    [SerializeField] private AudioSource downSound;
     
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ObstacleLeft")
        {
            leftSound.Play();
        }
        else if(collision.tag == "ObstacleRight")
        {
            rightSound.Play();
        }  
        else if(collision.tag == "ObstacleUp")
        {
            upSound.Play();
        } 
        else if(collision.tag == "ObstacleDown")
        {
            downSound.Play();
        }   
    }
}
