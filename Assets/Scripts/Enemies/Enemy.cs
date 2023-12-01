using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Entity))]
public abstract class Enemy : MonoBehaviour
{
    public Entity entity;
    public GameObject target;

    private void Start()
    {
        entity = GetComponent<Entity>();
        entity.EntityObject = gameObject;
        target = PlayerData.instance.PlayerEntity.EntityObject;
        onStart();
    }
    private void Update()
    {
        if (entity.Dead)
            Destroy(gameObject);
        onUpdate();
    }
    protected abstract void onUpdate();
    protected abstract void onStart();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox_Player" && other.GetComponent<EntityHitbox>() != null)
        {
            other.GetComponent<EntityHitbox>().ApplyDamage(entity);
        }
    }
}
