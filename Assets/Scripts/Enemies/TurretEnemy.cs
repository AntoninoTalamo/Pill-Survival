using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    int State = 0;
    //0 = Idle
    //1 = Attack Player
    [SerializeField] GameObject Model;
    [SerializeField] Transform AttackOffset;
    [SerializeField] EntityBehaviour ModeOfAttack;
    [SerializeField] float Range = 20f;
    Cooldown AttackCooldown = new Cooldown();
    protected override void onUpdate()
    {
        entity.BehaviourPos = AttackOffset.position;
        entity.BehaviourRot = Model.transform.rotation;
        switch (State){
            case 0://Idle
                if (Vector3.Distance(transform.position,target.transform.position) <= Range) State = 1;
                break;
            case 1://Attack Player
                if (Vector3.Distance(transform.position, target.transform.position) >= Range) State = 0;
                else
                {
                    AttackCooldown.Tick();
                    Model.transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
                    if (AttackCooldown.Up())
                    {
                        AttackCooldown.Set(entity.stats.GetAttackCooldown(ModeOfAttack.Cooldown));
                        ModeOfAttack.Execute(entity);
                    }
                }
                break;
        }
    }
}
