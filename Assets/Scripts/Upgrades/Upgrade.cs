using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public string UpgradeName = "An Upgrade";
    public string UpgradeDescription = "Upgrade Description";
    public int ID = 1;
    public float Cooldown = 0;
    public enum ProcType {None, OnAttack, OnTakeDamage, OnKill, OnTimer, OnSpawn}
    public ProcType ProcCondition = ProcType.None;

    public abstract void Apply(Entity entity);// obtain
    public abstract void Proc(Entity entity, int stack);// use
    public abstract void Unapply(Entity entity);// lose 
    public UpgradeContainer CreateUpgradeContainer()
    {
        UpgradeContainer container = new UpgradeContainer();
        switch (ProcCondition)
        {
            case (ProcType.OnAttack):
                container.ProcCondition = UpgradeContainer.ProcType.OnAttack;
                break;
            case (ProcType.OnTakeDamage):
                container.ProcCondition = UpgradeContainer.ProcType.OnTakeDamage;
                break;
            case (ProcType.OnKill):
                container.ProcCondition = UpgradeContainer.ProcType.OnKill;
                break;
            case (ProcType.OnTimer):
                container.ProcCondition = UpgradeContainer.ProcType.OnTimer;
                break;
        }
        container.Stack = 1;
        if (Cooldown >= 0f)
        {
            container.Cooldown.Set(Cooldown);
            container.HasCooldown = true;
        }
        else
            container.HasCooldown = false;
        container.Upgrade = this;
        return container;
    }
    protected void AddToUpgradeList(Entity entity)
    {
        UpgradeContainer container = entity.ContainerWithUpgrade(this);
        if (container != null)
            container.Stack++;
        else
            entity.Upgrades.Add(CreateUpgradeContainer());
    }
    protected void RemoveFromUpgradeList(Entity entity)
    {
        UpgradeContainer container = entity.ContainerWithUpgrade(this);
        if (container != null)
        {
            container.Stack--;
            if (container.Stack <= 0)
                entity.Upgrades.Remove(container);
        }
    }
}
