using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsCarrot : MonoBehaviour
{
    [SerializeField] Transform Carrot;
    [SerializeField] float MoveSpeed;

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Carrot.position, MoveSpeed);
    }
}
