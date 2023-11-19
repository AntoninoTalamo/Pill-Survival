using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHitbox : MonoBehaviour
{
    public float DamageModifier = 1f;
    public bool DestroyOnUse = true;
    public Entity entity;

    public void ApplyDamage(Entity hit)
    {
        hit.TakeDamage(entity, DamageModifier);
        if(DestroyOnUse) Destroy(gameObject);
    }
}
