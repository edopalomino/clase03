using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blaster;

    private void OnEnable()
    {
        Countdown.onCountdownEnd += SpawnBlaster;
    }

    private void OnDisable()
    {
        Countdown.onCountdownEnd -= SpawnBlaster;
    }


    private void SpawnBlaster()
    {
        blaster.SetActive(true);
    }
}
