using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTester : MonoBehaviour
{
    public List<Upgrade> PossibleUpgrades = new List<Upgrade>();
    public Entity toApplyTo;
    public void Assign()
    {
        int index = Random.Range(0, PossibleUpgrades.Count);
        PossibleUpgrades[index].Apply(toApplyTo);
    }
}
