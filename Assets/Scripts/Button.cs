using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Animator animator;
    private bool isPressed = false;
    public delegate void OnGoPressed();
    public static event OnGoPressed onGoPressed;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPress()
    {
        if(isPressed) return;
        animator.Play("ButtonPress");
        onGoPressed?.Invoke();
        GetComponent<AudioSource>().Play();
        isPressed = true;
    }
}
