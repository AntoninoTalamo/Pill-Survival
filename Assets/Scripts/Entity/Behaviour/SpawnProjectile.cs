using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Projectile", menuName = "ScriptableObjects/EntityBehaviour/SpawnProjectile", order = 1)]
public class SpawnProjectile : EntityBehaviour
{
    [SerializeField] GameObject ProjectilePrefab;

    public override void Execute(Entity entity)
    {
        GameObject proj = GameObject.Instantiate(ProjectilePrefab);
        proj.transform.position = entity.BehaviourPos;
        proj.transform.rotation = entity.BehaviourRot;

        EntityHitbox[] hitboxes = proj.GetComponentsInChildren<EntityHitbox>();
        foreach (EntityHitbox box in hitboxes)
        {
            box.entity = entity;
            box.gameObject.tag = "Hitbox_" + entity.Type.ToString();
        }
    }
}
