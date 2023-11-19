using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Boost", menuName = "ScriptableObjects/Upgrades/StatBoost", order = 1)]
public class StatBoost : Upgrade
{
    [SerializeField] EntityStats statBoosts;
    public override void Apply(Entity entity)
    {
        AddToUpgradeList(entity); //Add to Upgrade list
        entity.stats.AddStats(statBoosts);
    }

    public override void Proc(Entity entity, int stack)
    {
        //do nothing, this upgrade shouldn't do anything when procced
    }

    public override void Unapply(Entity entity)
    {
        RemoveFromUpgradeList(entity); //Remove From Upgrade List
        entity.stats.RemoveStats(statBoosts);
    }
}
