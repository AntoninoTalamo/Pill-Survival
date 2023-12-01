using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance { get; private set; }
    public int EXP = 0;
    public int NextLevelUp = 10;
    public Entity PlayerEntity;
    public EntityStats DefaultStats;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        PlayerEntity = GetComponent<Entity>();

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void ResetPlayer()
    {
        PlayerEntity.stats.CopyStats(DefaultStats);
    }
}
