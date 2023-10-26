using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    private float tiempo = 0f;
    private int contador = 0;

    [SerializeField]
    private TextMeshProUGUI textMesh;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;

    public delegate void OnCountdownEnd();
    public static event OnCountdownEnd onCountdownEnd;

    // Start se llama antes del primer frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Button.onGoPressed += Go;
    }

    private void OnDisable()
    {
        Button.onGoPressed -= Go;
    }

    private void Go()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (contador < 3)
        {
            tiempo += Time.deltaTime;

            if (tiempo >= 1f)
            {
                tiempo = 0f;
                contador++;
                textMesh.text = contador.ToString();
                audioSource.Play();
            }

            yield return null;
        }
        yield return new WaitForSeconds(1f);
        audioSource.clip = audioClip;
        audioSource.Play();
        textMesh.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        textMesh.text = "";
        onCountdownEnd?.Invoke();
    }
}
