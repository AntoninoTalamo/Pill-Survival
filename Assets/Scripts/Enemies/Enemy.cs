using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Entity))]
public abstract class Enemy : MonoBehaviour
{
    public Entity entity;
    public GameObject target;
    public GameObject EXPDrop;
    private EntityStats resetStats;

    private void Start()
    {
        entity = GetComponent<Entity>();
        entity.EntityObject = gameObject;
        resetStats = entity.stats;
        target = PlayerData.instance.PlayerEntity.EntityObject;
        onStart();
    }
    private void Update()
    {
        if (entity.Dead && EXPDrop != null)
        {
            GameObject xp = ObjectPool.instance.PullObject(EXPDrop.name);
            if(xp != null)
                xp.transform.position = this.transform.position;
        }
        if (entity.Dead || transform.position.y < -20f)
        {
            //destroy if dead or out of bounds
            ObjectPool.instance.PoolObject(this.gameObject);
            entity.stats = resetStats;
            entity.Dead = false;
        }
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
