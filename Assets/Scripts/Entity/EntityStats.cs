using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityStats
{
    public int Level = 1;

    [Header("Basic Stats")]
    public float MaxHP = 100f;
    public float CurrentHP = 100f;
    public float BaseDamage = 10f;
    public float InvincibleTime = 0.1f;

    [Header("Unique Stats")]
    public float HPRegeneration = 0f;
    public float LifeSteal = 0f;
    public float Armor = 0f;
    public float DamageIncrease = 0f;
    public float CritChance = 0f;
    public float AttackSpeed = 0f;
    public float AttackVelocity = 0f;
    public float MoveIncrease = 0f;
    public float Luck = 0f;
    public float InvincibleIncrease = 0f;
    public float DodgeChance = 0f;

    public void AddStats(EntityStats s)
    {
        MaxHP += s.MaxHP;
        CurrentHP += s.MaxHP;//not a typo
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHP);
        BaseDamage += s.BaseDamage;

        HPRegeneration += s.HPRegeneration;
        LifeSteal += s.LifeSteal;
        Armor += s.Armor;
        DamageIncrease += s.DamageIncrease;
        CritChance += s.CritChance;
        AttackSpeed += s.AttackSpeed;
        AttackVelocity += s.AttackVelocity;
        MoveIncrease += s.MoveIncrease;
        Luck += s.Luck;
        InvincibleIncrease += s.InvincibleIncrease;
        DodgeChance += s.DodgeChance;
    }
    public void RemoveStats(EntityStats s)
    {
        MaxHP -= s.MaxHP;
        CurrentHP -= s.MaxHP;//not a typo
        CurrentHP = Mathf.Clamp(CurrentHP, 1, MaxHP);
        BaseDamage -= s.BaseDamage;

        HPRegeneration -= s.HPRegeneration;
        LifeSteal -= s.LifeSteal;
        Armor -= s.Armor;
        DamageIncrease -= s.DamageIncrease;
        CritChance -= s.CritChance;
        AttackSpeed -= s.AttackSpeed;
        AttackVelocity -= s.AttackVelocity;
        MoveIncrease -= s.MoveIncrease;
        Luck -= s.Luck;
        InvincibleIncrease -= s.InvincibleIncrease;
        DodgeChance -= s.DodgeChance;
    }
    public float GetAttackCooldown(float initial)
    {
        return initial / (1 + AttackSpeed);
    }
}
