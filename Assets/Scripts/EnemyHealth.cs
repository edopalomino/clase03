using System.Collections;
using UnityEngine;

/// <summary>
/// Maneja la salud y el comportamiento del enemigo
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth { get; private set; } = 100; // Salud máxima del enemigo
    public int currentHealth { get; private set; }  // Salud actual del enemigo
    [SerializeField]
    private Material damage; // MeshRenderer para visualizar el daño
    private Material[] original = new Material[3]; // Material original del enemigo
    [SerializeField]
    private MeshRenderer[] meshRenderer; // MeshRenderer del enemigo
    private Animator animator; // Animator del enemigo
    [SerializeField] private GameObject dead;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject proyectile;
    [SerializeField] private float[] spawnInterval = { 8, 10};// Intervalo de tiempo entre cada spawn

    // Este evento se activará cuando el enemigo muera.
    public delegate void EnemyDied();
    public event EnemyDied OnEnemyDied;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // Obtiene el Animator del enemigo
        for(int i = 0; i < meshRenderer.Length; i++)
        {
            original[i] = meshRenderer[i].material; // Guarda el material original del enemigo
        }
    }

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual con la salud máxima
        float random = Random.Range(spawnInterval[0], spawnInterval[1]); // Genera un número aleatorio entre el intervalo de tiempo
        InvokeRepeating("SpawnObject", 0f, random);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;  // Reduce la salud del enemigo
        // Inicia la corrutina para visualizar el daño
        StartCoroutine(Damage());
        animator.Play("damage_ghost"); // Activa el trigger "Damage" del Animator

        // Verifica si la salud actual es menor o igual a 0
        if (currentHealth <= 0)
        {
            Die(); // Llama a la función para que el enemigo muera
        }
    }

    private IEnumerator Damage()
    {
        foreach (MeshRenderer mesh in meshRenderer)
        {
            mesh.material = damage; // Cambia el material del enemigo por el material de daño
        }
        yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos
        for(int i = 0; i < meshRenderer.Length; i++)
        {
            meshRenderer[i].material = original[i]; // Restaura el material original del enemigo
        }
    }

    void SpawnObject()
    {
        // Instanciar el objeto en la posición actual del script
        animator.Play("attack_ghost");
        Instantiate(proyectile, spawnPoint.transform.position, spawnPoint.rotation);
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        // Crea las particulas de muerte del enemigo y agrega un offset de 1 en el eje Y
        Instantiate(dead, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        // Si hay suscriptores al evento, los notifica
        OnEnemyDied?.Invoke();

        // Destruye el objeto del enemigo
        Destroy(gameObject);
    }
}
