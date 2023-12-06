using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public Text Numb;
    public void SetNumber(int Num)
    {
        Numb.text = Num.ToString();
    }
    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 1, 0), 1f * Time.deltaTime);
    }
}
