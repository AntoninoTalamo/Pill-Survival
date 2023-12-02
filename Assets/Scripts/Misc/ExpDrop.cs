using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDrop : MonoBehaviour
{
    public int Amount = 1;

    public void Update()
    {
        if (Vector3.Distance(PlayerData.instance.PlayerEntity.EntityObject.transform.position, transform.position) < 3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerData.instance.PlayerEntity.EntityObject.transform.position, Time.deltaTime * 5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerData.instance.ApplyEXP(Amount);
            Destroy(this.gameObject);
        }
    }
}
