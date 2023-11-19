using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Weapon", menuName = "ScriptableObjects/Upgrades/PlayerWeapon", order = 2)]
public class PlayerWeapon : Upgrade
{
    [SerializeField] EntityBehaviour NewWeapon;
    public override void Apply(Entity entity)
    {
        if (entity.EntityObject.GetComponent<Player>() != null)
            entity.EntityObject.GetComponent<Player>().AttackAbility = NewWeapon;
    }

    public override void Proc(Entity entity, int stack)
    {
        //Don't do anything on proc, in fact this upgrade doesn't even get added to the entity's upgrade list
    }

    public override void Unapply(Entity entity)
    {
        //This upgrade cannot be removed as it is not saved in the entity's upgrade list
    }
}
