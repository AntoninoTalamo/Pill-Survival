using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeContainer
{
    public enum ProcType { None, OnAttack, OnTakeDamage, OnKill, OnTimer }
    public ProcType ProcCondition = ProcType.None;
    public bool HasCooldown = false;
    public Cooldown Cooldown = new Cooldown();
    public int Stack = 1;
    public Upgrade Upgrade;
}
