using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainCharacter : MonoBehaviour
{
    public static int coins = 0;
    public static int lives = 100;
    [SerializeField]
    private float speed = 5.0F;
    [SerializeField]
    private float jumpForce = 15.0F;
    private bool isGrounded = false;
    private int jump = 1;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI livesText;


    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;


    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        coinText.text = 'x' + coins.ToString();
        livesText.text = lives.ToString();
    }

    void Update()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        checkLive();
        State = CharState.Idle;

        if (isGrounded == true)
            jump = 1;

        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Jump") && jump > 0)
        {
            Jump();
            jump--;
        }
        else if (isGrounded && Input.GetButtonDown("Jump") && jump == 0)
        {
            Jump();
        }
        
    }

    private void Run()
    {
        State = CharState.Run;

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;

        if (isGrounded) State = CharState.Run;
    }

    private void Jump()
    {
        State = CharState.Jump;

        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        if (!isGrounded) State = CharState.Jump;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Coin")
        {
            coins++;
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("Portal") && coins >= 6)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyMove enemy = collision.collider.GetComponent<EnemyMove>();
        if(enemy != null)
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                if(point.normal.y >= 0.9f)
                {
                    enemy.destroy();
                }
                else
                {
                    getDamage();
                }
            }
        }
    }

    void checkLive()
    {
        if (lives <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void getDamage()
    {
        lives -= 10;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 12.0F, ForceMode2D.Impulse);
    }


    public enum CharState
    {
        Idle,
        Run,
        Jump
    }
}
