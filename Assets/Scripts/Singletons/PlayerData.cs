using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public int EXP = 0;
    public int NextLevelUp = 5;
    public Entity PlayerEntity;
    public EntityStats DefaultStats;
     // List to hold all possible upgrades
    public List<Upgrade> availableUpgrades = new List<Upgrade>();

    private void Start()
    {
        PlayerEntity = GetComponent<Entity>();
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        //Remove all EXP, Upgrades, and Stat Changes
        EXP = 0;
        NextLevelUp = 5;
        PlayerEntity.stats.CopyStats(DefaultStats);
        PlayerEntity.stats.Level = 1;
        PlayerEntity.Upgrades.Clear();
        PlayerEntity.Dead = false;
    }

    public void ApplyEXP(int amount)
    {
        EXP += amount;
        if(EXP >= NextLevelUp)
        {
            EXP = 0;
            NextLevelUp += 8;
            PlayerEntity.stats.Level++;
            //Prompt the player pick from one of three randomly selected upgrades
            OfferUpgrades();
            AudioManager.instance.PlaySound(5);//Level up sound
        }
    }
    private void OfferUpgrades()
    {
        Upgrade U1 = availableUpgrades[Random.Range(0, availableUpgrades.Count)];
        Upgrade U2 = availableUpgrades[Random.Range(0, availableUpgrades.Count)];
        Upgrade U3 = availableUpgrades[Random.Range(0, availableUpgrades.Count)];
        UIManager.instance.QueueUpgrades(U1, U2, U3);
    }
}
