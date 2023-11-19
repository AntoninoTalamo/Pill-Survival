using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Entity))]
public abstract class Enemy : MonoBehaviour
{
    public Entity entity;
    public GameObject target;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entity.EntityObject = gameObject;
        target = GameObject.Find("ThePlayer");//placeholder
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (entity.Dead)
            Destroy(gameObject);
        onUpdate();
    }
    private void FixedUpdate()
    {
        
    }
    protected abstract void onUpdate();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox_Player" && other.GetComponent<EntityHitbox>() != null)
        {
            other.GetComponent<EntityHitbox>().ApplyDamage(entity);
        }
    }
}
