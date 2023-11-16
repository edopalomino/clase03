using UnityEngine;

public class Ammo : MonoBehaviour
{
    public delegate void OnAmmoChanged(int ammo);
    public static event OnAmmoChanged AmmoChanged;
    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AmmoChanged?.Invoke(6);
            audioSource.Play();
            Destroy(GetComponent<SphereCollider>());
            Destroy(GetComponent<MeshRenderer>());
            Destroy(gameObject, 0.8f);
        }
    
    }
}
