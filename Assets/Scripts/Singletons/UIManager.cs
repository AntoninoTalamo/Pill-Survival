using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [Header("Upgrade Menu")]
    public GameObject UpgradeSelectionPanel;
    public UpgradeButton[] UpgradeButtons = new UpgradeButton[3];
    public enum MenuState {IDLE, GAMEPLAY, UPGRADESELECT, DEAD}
    public MenuState menuState = MenuState.IDLE;

    [Header("Gameplay Timer")]
    private float currentTime; // Current time remaining
    private bool isTimerRunning = false; // Flag to check if the timer is running

    public Text timerText; // Reference to a UI Text component to display the timer
    public GameObject TimerPanel;

    [Header("\"You Died\" Menu")]
    public GameObject GameOverPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (menuState)
        {
            case (MenuState.GAMEPLAY):
                Time.timeScale = 1f;
                UpgradeSelectionPanel.SetActive(false);
                GameOverPanel.SetActive(false);
                isTimerRunning = true;
                break;
            case (MenuState.IDLE):
                Time.timeScale = 1f;
                UpgradeSelectionPanel.SetActive(false);
                GameOverPanel.SetActive(false);
                isTimerRunning = false;
                break;
            case (MenuState.UPGRADESELECT):
                Time.timeScale = 0f;
                UpgradeSelectionPanel.SetActive(true);
                GameOverPanel.SetActive(false);
                isTimerRunning = false;
                break;
            case (MenuState.DEAD):
                Time.timeScale = 0f;
                UpgradeSelectionPanel.SetActive(false);
                GameOverPanel.SetActive(true);
                isTimerRunning = false;
                break;
        }
        if (isTimerRunning)
        {
            TimerPanel.SetActive(true);
            currentTime += Time.deltaTime;
            UpdateTimerText();
        }
        else
            TimerPanel.SetActive(false);
    }
    public void QueueUpgrades(Upgrade U1, Upgrade U2, Upgrade U3)
    {
        UpgradeButtons[0].Assign(U1);
        UpgradeButtons[1].Assign(U2);
        UpgradeButtons[2].Assign(U3);
        menuState = MenuState.UPGRADESELECT;
    }
    public void ResetTimer()
    {
        currentTime = 0f;
        UpdateTimerText();
        isTimerRunning = false;
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
