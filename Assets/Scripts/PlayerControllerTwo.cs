using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTwo : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    private Rigidbody2D rb;
    public bool onGround;
    private GameManager manager;
    AudioSource playerAudio;
    public AudioClip jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        rb.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        onGround = true;
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
            playerAudio.PlayOneShot(jumpSound, 1f);
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
