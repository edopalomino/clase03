using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void OnEnable()
    {

        Destroy(gameObject, 5f); // Destruir la bala después de 5 segundos
    }

}
