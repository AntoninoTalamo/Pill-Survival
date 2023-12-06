using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    public float seconds = 3;
	// Use this for initialization
    private void OnEnable()
    {
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(seconds);
        ObjectPool.instance.PoolObject(this.gameObject);
    }
}
