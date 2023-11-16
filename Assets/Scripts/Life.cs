using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gestiona la cida del jugador y actyaliza el slider
/// </summary>
public class Life : MonoBehaviour
{
    private int currentLife = 100;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if(slider != null)
        {
            slider.value = currentLife;
        }
        else
        {
            Debug.LogWarning("No slider found");
        }
        
    }

    private void OnEnable()
    {
        ChasePlayer.onCaught += Damage;
    }

    private void OnDisable()
    {
        ChasePlayer.onCaught -= Damage;
    }
    private void Damage(int damage)
    {
        if (slider != null)
        {
            currentLife -= damage;
            currentLife = Mathf.Clamp(currentLife, 0, 100); // Evita que la vida sea menor a 0 o mayor a 100
            slider.value = currentLife;
        }
    }
}
