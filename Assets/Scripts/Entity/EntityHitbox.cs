using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHitbox : MonoBehaviour
{
    public float DamageModifier = 1f;
    public bool Deflectable = false;
    public Entity entity;
    [SerializeField] Renderer[] Renderers;

    public void Initalize()
    {
        foreach(Renderer Rend in Renderers)
        {
            if (gameObject.tag == "Hitbox_Player") Rend.material.color = Color.cyan;
            if (gameObject.tag == "Hitbox_Enemy") Rend.material.color = Color.red;
        }
    }
    public void ApplyDamage(Entity hit)
    {
        hit.TakeDamage(entity, DamageModifier);
    }
}
