using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEndManagement : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
        Console.WriteLine("Was pressed!");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
