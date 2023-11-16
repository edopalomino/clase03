using UnityEngine;

/// <summary>
/// Este script controla el comportamiento de persecución del enemigo hacia el jugador.
/// </summary>
public class ChasePlayer : MonoBehaviour
{
    public string playerTag = "Head"; // Tag to identify the player GameObject
    public float chaseSpeed = 5.0f; // Adjustable chase speed
    public GameObject dead; // Prefab de la explosión

    private Rigidbody rb; // Reference to the Rigidbody component
    private Transform playerTransform; // Reference to the player's transform

    public delegate void OnCaught(int damage);
    public static event OnCaught onCaught;
    private int damageAttack = 20;

    void Start()
    {
        // Find the player GameObject using the specified tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObject != null)
        {
            // Get a reference to the player's transform
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player with tag '" + playerTag + "' not found.");
            enabled = false; // Disable this script if the player is not found
            return;
        }

        // Get a reference to the Rigidbody component
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation to prevent unwanted physics interactions
    }

    void FixedUpdate()
    {
        // Calculate the direction towards the player
        Vector3 chaseDirection = playerTransform.position - transform.position;
        chaseDirection.Normalize();

        // Look at the player
        transform.LookAt(playerTransform);

        // Move the enemy towards the player using physics
        rb.velocity = chaseDirection * chaseSpeed;
    }

    public void OnHit()
    {
        Debug.Log("Enemy has been hit!");
        Instantiate(dead, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered: " + other.tag);
        if (other.CompareTag("Player"))
        {
            onCaught?.Invoke(damageAttack);
            Destroy(gameObject);
            Debug.Log("Player has been caught!");
        }
    }
}




