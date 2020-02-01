using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float verticalInput = 0;
    float horizontalInput = 0;
    float jumpForce = 5;
    [SerializeField]
    float speedHorizontal = 15;

    float boundLeft = -20;
    float boundRight = 20;

    bool isOnGround = true;

    Rigidbody rigidbody;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * speedHorizontal * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }

            if (transform.position.y < -20)
            {
                gameManager.GameOver();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
