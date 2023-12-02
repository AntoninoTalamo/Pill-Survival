using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Text Title;
    public Text Description;
    public Image Icon;
    public Upgrade AssignedUpgrade;

    public void Start()
    {
        Assign(AssignedUpgrade);
    }
    public void Assign(Upgrade U)
    {
        AssignedUpgrade = U;
        Title.text = U.UpgradeName;
        Description.text = U.UpgradeDescription;
        Icon.sprite = U.UpgradeIcon;
    }
    public void OnUpgradeSelected()
    {
        AssignedUpgrade.Apply(PlayerData.instance.PlayerEntity);
        UIManager.instance.menuState = UIManager.MenuState.IDLE;
    }
}
