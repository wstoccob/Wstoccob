using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FallingPlatformMechanism : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private bool platformIsTriggered = false;
    [SerializeField] private float delayTime = 1.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rb.bodyType = RigidbodyType2D.Static;

    }
    private void Update()
    {
        if ( transform.position.y < -10f )
        {
            gameObject.SetActive( false );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && platformIsTriggered == false)
        {
            platformIsTriggered = true;
            Invoke("PlatformFalling", delayTime);
        }
    }
    private void PlatformFalling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        boxCollider.enabled = false;
        animator.SetBool("IsTriggered", true);
    }
}
