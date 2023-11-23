using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Inscribed")]
    [SerializeField] Transform Carrot;
    [SerializeField] float MoveSpeed;
    [SerializeField] EntityHitbox Hitbox;
    float mult = 1;

    private void FixedUpdate()
    {
        float mult = 1;
        if (Hitbox.entity != null)
            mult = 1 + Hitbox.entity.stats.AttackSpeed;
        else
            Destroy(gameObject);
        transform.position = Vector3.MoveTowards(transform.position, Carrot.position, MoveSpeed * mult);
    }
}
