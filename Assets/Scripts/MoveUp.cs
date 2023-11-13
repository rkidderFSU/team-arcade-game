using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    private GameManager manager;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isGameActive)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }
}
