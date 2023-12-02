using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Entity))]
public abstract class Enemy : MonoBehaviour
{
    public Entity entity;
    public GameObject target;
    public GameObject EXPDrop;

    private void Start()
    {
        entity = GetComponent<Entity>();
        entity.EntityObject = gameObject;
        target = PlayerData.instance.PlayerEntity.EntityObject;
        onStart();
    }
    private void Update()
    {
        if (entity.Dead && EXPDrop != null)
        {
            GameObject xp = Instantiate(EXPDrop);
            xp.transform.position = this.transform.position;
        }
        if (entity.Dead || transform.position.y < -20f)//destroy if dead or out of bounds
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
