using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cooldown
{
    float timer = 0f;

    public void Tick()//Decrement the timer
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0f, Mathf.Infinity);
    }
    public bool Up()//Is the time over?
    {
        return timer <= 0f;
    }
    public void Set(float time)//Set the timer
    {
        timer = time;
    }
}
