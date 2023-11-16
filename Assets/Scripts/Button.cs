using UnityEngine;

public class Button : MonoBehaviour
{
    private Animator animator;
    private bool hasBeenPressed = false;
    public delegate void OnGoPressed();
    public static event OnGoPressed onGoPressed;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPress()
    {
        if(hasBeenPressed) return;
        animator.Play("ButtonPress");
        onGoPressed?.Invoke();
        GetComponent<AudioSource>().Play();
        hasBeenPressed = true;
    }
}
