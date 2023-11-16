using UnityEngine;
using TMPro;

public class MessageScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro score;
    private int scoreValue = 0;
    
    public void SetScore(string value)
    {
        score.text = value;
        Destroy(gameObject, 0.8f);
    }
}
