using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HandAnimController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private GameObject blaster;
    [SerializeField] private TextMeshPro debugTouch;
    [SerializeField] private TextMeshPro debugTouch2;

    private GameObject otherCollider;

    private bool isOnTrigger = false;

    private float gripValue = 0;
    private float triggerValue = 0;

    private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            otherCollider = other.gameObject;
            isOnTrigger = true;
        }
    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            otherCollider = null;
            isOnTrigger = false;
        }else if(other.gameObject.tag == "Button")
        {
            other.GetComponent<Button>().OnPress();
        }
    }


    private void Start()
    {
        anim = GetComponent<Animator> ();
    }

    private void Update ()
    {
        debugTouch.text = gripValue.ToString();
        debugTouch2.text = isOnTrigger.ToString();
        triggerValue = triggerAction.action.ReadValue<float>();   
        gripValue = gripAction.action.ReadValue<float>();

        if(gripValue >= 1 && isOnTrigger)
        {
            blaster.SetActive(true);
            if(otherCollider != null)
            {
                Destroy(otherCollider);
            }
        }
       
        anim.SetFloat("Trigger", triggerValue);
        anim.SetFloat("Grip", gripValue);
    }
    
}
