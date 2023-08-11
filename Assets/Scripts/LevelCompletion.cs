using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletion : MonoBehaviour
{
    private AudioSource finishSoundEffect;
    private float delayTime = 2f;
    private bool isFinished = false;
    void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.name == "Player" && isFinished == false)
        {
            finishSoundEffect.Play();
            Invoke("CompleteLevel", delayTime);
            isFinished = true;
        }
    }
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
