using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    public GameObject HealthBar;
    public GameObject EXPBar;
    public Text Level;

    // Update is called once per frame
    void Update()
    {
        float CalcHealth = Mathf.Clamp(PlayerData.instance.PlayerEntity.stats.CurrentHP / PlayerData.instance.PlayerEntity.stats.MaxHP, 0, 1);
        float CalcEXP = Mathf.Clamp((float) PlayerData.instance.EXP / PlayerData.instance.NextLevelUp,0,1);
        HealthBar.transform.localScale = new Vector3(CalcHealth, 1, 1);
        EXPBar.transform.localScale = new Vector3(CalcEXP, 1, 1);
        Level.text = PlayerData.instance.PlayerEntity.stats.Level.ToString();
    }
}
