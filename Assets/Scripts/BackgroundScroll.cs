using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatHeight;
    public float moveSpeed;
    public GameManager manager;
    private GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        repeatHeight = GetComponent<BoxCollider2D>().size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        startPos = cam.transform.position;
        if (manager.isGameActive)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            if (transform.position.y < startPos.y - repeatHeight)
            {
                transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
            }
        }
    }
}
