using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

public class EasterEggFinding : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isFound = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (  collision.CompareTag("Player") && isFound == false  )
        {
            spriteRenderer.enabled = true;
            isFound = true;
            animator.SetBool("isFound", true);
        }
    }
}
