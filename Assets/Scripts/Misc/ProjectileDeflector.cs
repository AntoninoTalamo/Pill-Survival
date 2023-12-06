using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeflector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EntityHitbox hit = other.GetComponent<EntityHitbox>();
        if (hit != null)
        {
            if (hit.Deflectable)
                Destroy(hit.gameObject);
        }
    }
}
