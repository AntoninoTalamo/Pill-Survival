using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum EntityType {Undefined, Player, Enemy};
    public EntityType Type = EntityType.Undefined;
    //game object this entity is attached too
    public GameObject EntityObject;

    public EntityStats stats;
    public List<UpgradeContainer> Upgrades = new List<UpgradeContainer>();
    public bool Dead;
    Cooldown IFrames = new Cooldown();

    [HideInInspector] public Vector3 FacingPos;
    [HideInInspector] public Quaternion FacingRot;
    [HideInInspector] public Vector3 LastHit;

    public void Update()
    {
        IFrames.Tick();
        TickUpgradeCooldowns();
        ProcOnTimer(); //Passive Upgrade Abilities
    }
    public void TakeDamage(Entity E, float Modifier)
    {
        if(IFrames.Up())
        {
            ProcOnTakeDamage();
            IFrames.Set(0.025f * (1 + stats.InvincibleIncrease));  //apply invincibility
            float Damage = (E.stats.BaseDamage * (1 + E.stats.DamageIncrease) / (1 + (stats.Armor * 1f)) * Modifier);

            //Crit Chance
            float crit = Random.Range(0f, 1f);
            if (E.stats.CritChance > crit)
            {
                Debug.Log("CRIT!");
                Damage *= 2f;
            }
          
            //Dodge chance
            float dodge = Random.Range(0f, 1f);
            if(Mathf.Clamp(stats.DodgeChance,0,0.8f) <= Random.Range(0f, 1f))
                stats.CurrentHP = Mathf.Clamp(stats.CurrentHP - Damage, 0, stats.MaxHP); //Deal Damage here
            else
                Debug.Log("Dodge!");

            //heal attacker
            E.HealDamage(Damage * E.stats.LifeSteal);

            //Enemy Specific Stuff
            if(Type == EntityType.Enemy)
            {
                AudioManager.instance.PlaySound(2);//TakeDamage sound
                GameObject DamageNum = ObjectPool.instance.PullObject("DamageNumber");
                if(DamageNum != null)
                {
                    DamageNum.transform.position = this.transform.position + new Vector3(0, 3, 0);
                    DamageNum.GetComponent<DamageNumber>().SetNumber(Mathf.RoundToInt(Mathf.RoundToInt(Damage)));
                }
            }
            else
                AudioManager.instance.PlaySound(3);//Player damage sound
            if (stats.CurrentHP <= 0)
            {
                E.ProcOnKill();
                Dead = true;
                //Prompt the player to restart if the Player is dead
                if (Type == EntityType.Player)
                    UIManager.instance.menuState = UIManager.MenuState.DEAD;
            }
            Debug.Log("Damage Taken: " + Damage);
        }
    }
    public void HealDamage(float Health)
    {
        stats.CurrentHP = Mathf.Clamp(stats.CurrentHP + Health, 0, stats.MaxHP);
    }
    public void TickUpgradeCooldowns()
    {
        foreach(UpgradeContainer Up in Upgrades)
        {
            if (Up.HasCooldown)
                Up.Cooldown.Tick();
        }
    }
    /*
     * Functions for activating upgrade abilities
     */
    public void ProcOnTakeDamage()
    {
        foreach (UpgradeContainer P in Upgrades)
            if (P.ProcCondition == UpgradeContainer.ProcType.OnTakeDamage) ProcAnUpgrade(P);
    }
    public void ProcOnKill()
    {
        foreach (UpgradeContainer P in Upgrades)
            if (P.ProcCondition == UpgradeContainer.ProcType.OnKill) ProcAnUpgrade(P);
    }
    public void ProcOnAttack()
    {
        foreach (UpgradeContainer P in Upgrades)
            if (P.ProcCondition == UpgradeContainer.ProcType.OnAttack) ProcAnUpgrade(P);
    }
    public void ProcOnTimer()
    {
        foreach (UpgradeContainer P in Upgrades)
            if (P.ProcCondition == UpgradeContainer.ProcType.OnTimer) ProcAnUpgrade(P);
    }
    public void ProcAnUpgrade(UpgradeContainer P)
    {
        if(P.HasCooldown) P.Cooldown.Set(P.Upgrade.Cooldown);
        P.Upgrade.Proc(this, P.Stack);
    }
    /*
     * Functions for checking upgrades
     */
    public UpgradeContainer ContainerWithUpgrade(Upgrade Up)
    {
        foreach (UpgradeContainer container in Upgrades)
            if (container.Upgrade == Up) return container;
        return null;
    }
    public void AssignHitboxes(GameObject root)
    {
        //Assign Hitboxes
        EntityHitbox[] hitboxes = root.GetComponentsInChildren<EntityHitbox>();
        foreach (EntityHitbox box in hitboxes)
        {
            box.entity = this;
            box.gameObject.tag = "Hitbox_" + Type.ToString();
            box.Initalize();
        }
    }

}
