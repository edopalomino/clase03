using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShooting : MonoBehaviour
{
    public float range = 100f; // Alcance del disparo
    public int damage = 10; // Da�o que har� el disparo
    [SerializeField]
    private TextMeshPro meshPro; // Texto para mostrar la cantidad de municiones
    private int ammo = 999; // Cantidad de municiones
    [SerializeField]
    public GameObject weapon; // C�mara del jugador para determinar el punto de inicio del raycast
    [SerializeField]
    private InputActionProperty RightClick; // Bot�n derecho del mouse
    [SerializeField]
    private InputActionProperty gripActionR;
    [SerializeField]
    private InputActionProperty gripActionL;
    [SerializeField]
    LineRenderer LineRenderer; // LineRenderer para visualizar el raycast
    [SerializeField]
    private GameObject bullet; // Prefab de la bala


    void Update()
    {
        LineRenderer.SetPosition(0, LineRenderer.transform.position);
        LineRenderer.SetPosition(1, LineRenderer.transform.position + LineRenderer.transform.forward * -1 * range);
    }

  

    private void UpdateAmmo(int ammo)
    {
        this.ammo = ammo;
        meshPro.text = ammo.ToString();
    }

    private void OnEnable()
    {
        Ammo.AmmoChanged += UpdateAmmo;

        RightClick.action.performed += ctx => Shoot(); // Suscribirse al evento aqu�
        //grip action hasta que el valor sea mayoro igual a 1
        gripActionR.action.performed += ctx =>
        {
            if (ctx.ReadValue<float>() >= 1.0f && ammo > 0)
            {
                Shoot();
            }
        };
        gripActionL.action.performed += ctx =>
        {
            if (ctx.ReadValue<float>() >= 1.0f && ammo > 0)
            {
                Shoot();
            }
        };
    }

    private void OnDisable()
    {
        Ammo.AmmoChanged -= UpdateAmmo;
        RightClick.action.performed -= ctx => Shoot(); // Desuscribirse al evento aqu�
    }

    void Shoot()
    {
        ammo -= 1;  
        meshPro.text = ammo.ToString();
        RaycastHit hit; // Informaci�n sobre lo que golpea el raycast
        if (Physics.Raycast(weapon.transform.position, weapon.transform.forward * -1, out hit, range))
        {
            Debug.DrawRay(weapon.transform.position, weapon.transform.forward * -1 * range, Color.red, 1f);

            //Crear bala en el punto de impacto
            Instantiate(bullet, hit.point, Quaternion.identity);

            // Aqu� puedes a�adir l�gica adicional, por ejemplo, reducir la salud de un enemigo
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            Target target = hit.transform.GetComponent<Target>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if(target != null)
            {
                target.OnHit(hit.point);
            }
        }
        else
        {
            Instantiate(bullet, hit.point, Quaternion.identity);
        }
    }
}
