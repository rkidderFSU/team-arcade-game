using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTwo : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    private Rigidbody2D rb;
    public bool onGround;
    public GameManager manager;
    public float playerCollisionForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.Find("Player 2").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isGameActive)
        {
            Move();
        }
    }

    private void Move()
    {
        float input = Input.GetAxis("Player 2");
        rb.AddForce(Vector3.right * input * moveSpeed);
        if ((Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && transform.position.y > collision.transform.position.y)
        {
            onGround = true;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
