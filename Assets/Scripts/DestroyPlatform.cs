using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        transform.parent = GameObject.Find("Platform Container").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isGameActive)
        {
            Destroy(gameObject, 7);
        }
    }
}
