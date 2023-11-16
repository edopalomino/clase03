using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GrabBlaster : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty grabAction;
    [SerializeField]
    private TextMeshPro score;
    private float value = 0;
    public delegate void OnGrabbed(string hand);
    public static event OnGrabbed Grabbed;

    private void Update()
    {
        value = grabAction.action.ReadValue<float>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Hand" && value >= 1)
        {
            score.text = other.name;
            Grabbed?.Invoke(gameObject.tag);
            Destroy(gameObject);
        }
    }
}
