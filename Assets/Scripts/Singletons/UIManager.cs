using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    [Header("Upgrade Menu")]
    public GameObject UpgradeSelectionPanel;
    public UpgradeButton[] UpgradeButtons = new UpgradeButton[3];
    public enum MenuState {IDLE, GAMEPLAY, UPGRADESELECT, PAUSE}
    public MenuState menuState = MenuState.IDLE;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (menuState)
        {
            case (MenuState.IDLE):
                Time.timeScale = 1f;
                UpgradeSelectionPanel.SetActive(false);
                break;
            case (MenuState.UPGRADESELECT):
                Time.timeScale = 0f;
                UpgradeSelectionPanel.SetActive(true);
                break;
            case (MenuState.PAUSE):
                Time.timeScale = 0f;
                UpgradeSelectionPanel.SetActive(false);
                break;
        }
    }
    public void QueueUpgrades(Upgrade U1, Upgrade U2, Upgrade U3)
    {
        UpgradeButtons[0].Assign(U1);
        UpgradeButtons[1].Assign(U2);
        UpgradeButtons[2].Assign(U3);
    }
}
