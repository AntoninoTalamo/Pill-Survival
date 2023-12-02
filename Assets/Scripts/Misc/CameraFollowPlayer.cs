using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    public Transform player;
    Vector3 Offset;
    private void Start()
    {
        Offset = transform.position - player.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, player.transform.position + Offset, Time.deltaTime * 1f);
    }
}
