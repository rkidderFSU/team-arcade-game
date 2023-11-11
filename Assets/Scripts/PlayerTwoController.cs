using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    private Rigidbody rb;
    public bool onGround;
    public float timeToWait;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        float input = Input.GetAxis("Player 2");
        rb.AddForce(Vector3.right * input * moveSpeed);
        if ((Input.GetKeyDown(KeyCode.Alpha8) || (Input.GetKeyDown(KeyCode.Keypad8))) && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    IEnumerator Countdown()
    {
        canMove = false;
        yield return new WaitForSeconds(timeToWait);
        canMove = true;
    }
}
