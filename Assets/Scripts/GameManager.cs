using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(SceneManager.GetActiveScene().name == "StartMenu")
            {
                Application.Quit();
		        Debug.Log("Quit!");
            } 
            else 
            {
                SceneManager.LoadScene("StartMenu");
            }

        }

    }
}
