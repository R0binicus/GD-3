using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private GameObject leftGate;
    [SerializeField] private GameObject rightGate;
    [SerializeField] private GameObject upGate;
    [SerializeField] private GameObject downGate;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    [SerializeField] private float timeBetweenSpawn;
    private float spawnTime;
    [SerializeField]private float startTime;
    private float startTimePrivate;
    private float gameTime;
    private bool Inc01 = false;
    private bool Inc02 = false;
    private bool Inc03 = false;
    private bool Inc04 = false;
    private bool Inc05 = false;
    private bool Inc06 = false;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0.0f;
        //startTimePrivate = Time.time + startTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        //if(Time.time > spawnTime && Time.time > startTimePrivate)
        //{
        //    Spawn();
        //    spawnTime = Time.time + timeBetweenSpawn;
        //}
        if(gameTime > spawnTime && gameTime > startTime)
        {
            Spawn();
            spawnTime = gameTime + timeBetweenSpawn;
        }

        if(gameTime > (startTime + 10f) && Inc01 == false)
        {
            Inc01 = true;
            timeBetweenSpawn = timeBetweenSpawn - 0.07f;
        } else if(gameTime > (startTime + 20) && Inc02 == false)
        {
            Inc02 = true;
            timeBetweenSpawn = timeBetweenSpawn - 0.07f;
        } else if(gameTime > (startTime + 30) && Inc03 == false)
        {
            Inc03 = true;
            timeBetweenSpawn = timeBetweenSpawn - 0.07f;
        } else if(gameTime > (startTime + 40) && Inc04 == false)
        {
            Inc04 = true;
            timeBetweenSpawn = timeBetweenSpawn - 0.07f;
        }
         else if(gameTime > (startTime + 50) && Inc05 == false)
        {
            Inc05 = true;
            timeBetweenSpawn = timeBetweenSpawn - 0.07f;
        }
         else if(gameTime > (startTime + 60) && Inc06 == false)
        {
            Inc06 = true;
            timeBetweenSpawn = timeBetweenSpawn - 0.05f;
        }

    }

    void Spawn()
    {
        int randomInt = Random.Range(1, 5);
        //float randomY = Random.Range(minY, maxY);
        switch(randomInt) 
        {
            case 1:
            Instantiate(leftGate, transform.position + new Vector3(2, 3, 0), transform.rotation);
                break;
            case 2:
            Instantiate(rightGate, transform.position + new Vector3(-2, 3, 0), transform.rotation);
                break;
            case 3:
            Instantiate(upGate, transform.position + new Vector3(2, 3, 0), transform.rotation);
            Instantiate(upGate, transform.position + new Vector3(-2, 3, 0), transform.rotation);
                break;
            case 4:
            Instantiate(downGate, transform.position + new Vector3(2, 3, 0), transform.rotation);
            Instantiate(downGate, transform.position + new Vector3(-2, 3, 0), transform.rotation);
                break;
            default:
            Instantiate(leftGate, transform.position + new Vector3(2, 3, 0), transform.rotation);
            Instantiate(rightGate, transform.position + new Vector3(-2, 3, 0), transform.rotation);
            Instantiate(downGate, transform.position + new Vector3(2, 3, 0), transform.rotation);
            Instantiate(downGate, transform.position + new Vector3(-2, 3, 0), transform.rotation);
            Instantiate(downGate, transform.position + new Vector3(2, 3, 0), transform.rotation);
            Instantiate(downGate, transform.position + new Vector3(-2, 3, 0), transform.rotation);
                break;
        }
    }
}
