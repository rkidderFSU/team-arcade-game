using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{

    public float moveSpeed;
    public float timeToWait;
    public bool hasTimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTimeElapsed)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(timeToWait);
        hasTimeElapsed = true;
    }
}
