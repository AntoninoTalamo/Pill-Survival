using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingEnemy : Enemy
{
    [SerializeField]
    float MoveSpeed = 1f;

    protected override void onStart()
    {
        MoveSpeed = MoveSpeed + (Random.Range(-0.2f, 0.2f) * MoveSpeed); //varience
    }
    protected override void onUpdate()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, MoveSpeed * Time.deltaTime);
    }
}
