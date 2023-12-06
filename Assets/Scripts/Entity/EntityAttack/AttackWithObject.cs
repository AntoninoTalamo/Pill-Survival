using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Projectile", menuName = "ScriptableObjects/EntityBehaviour/AttackWithObject", order = 1)]
public class AttackWithObject : EntityAttack
{
    [SerializeField] GameObject AttackObject;
    public override void Execute(Entity entity)
    {
        GameObject proj = ObjectPool.instance.PullObject(AttackObject.name);
        if(proj != null)
        {
            proj.transform.position = entity.FacingPos;
            proj.transform.rotation = entity.FacingRot;
            entity.AssignHitboxes(proj);
        }
    }
}
