using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BallMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float baseSpeed = 10f;
    [SerializeField] float maxPaddleInfluence = 2f;
    [SerializeField] float maxReflectionAngle = 60f;

    [SerializeField]GameManager gameManager;
    [SerializeField]AudioSource audioSource;

    private Rigidbody2D rb;
    private Vector2 lastFrameVelocity;
    private float currentSpeed;
    public float rbVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        lastFrameVelocity = rb.linearVelocity;
        currentSpeed= Mathf.Clamp(currentSpeed,8,8);
        rbVelocity = rb.linearVelocity.magnitude;
    }

    public void pushBall()
    {
        // Initial direction with slight random horizontal component
        Vector2 direction = new Vector2(Random.Range(-0.2f, 0.2f), 1).normalized;
        rb.linearVelocity = direction * currentSpeed;
    }
    public void Launch()
    {
        Invoke("pushBall", 3f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            HandlePaddleCollision(collision);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Brick"))
        {
            HandleWallBrickCollision(collision);
        }
        else if(collision.gameObject.CompareTag("bottom"))
        {
            handleGameOver();
        }
        MaintainBallSpeed();
        audioSource.Play();
    }

    void HandlePaddleCollision(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];

        // Calculate hit position relative to paddle center
        Vector2 paddlePos = collision.transform.position;
        float paddleWidth = collision.collider.bounds.size.x;
        float offsetFromCenter = contact.point.x - paddlePos.x;
        float normalizedOffset = offsetFromCenter / (paddleWidth / 2f);

        // Get paddle velocity influence
        Rigidbody2D paddleRb = collision.rigidbody;
        float paddleVelocityInfluence = paddleRb ? paddleRb.linearVelocity.x * maxPaddleInfluence : 0f;

        // Calculate reflection angle with paddle influence
        float reflectionAngle = Mathf.Clamp(
            normalizedOffset * maxReflectionAngle + paddleVelocityInfluence,
            -maxReflectionAngle,
            maxReflectionAngle
        );

        // Convert angle to direction vector
        float angleInRadians = reflectionAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians));

        rb.linearVelocity = direction.normalized * (currentSpeed);
    }

    void HandleWallBrickCollision(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Vector2 normal = contact.normal;
        Vector2 reflection = Vector2.Reflect(lastFrameVelocity.normalized, normal);

        rb.linearVelocity = reflection;
        if(collision.gameObject.name == "Left" || collision.gameObject.name == "Right")
        {
            if (rb.linearVelocityY < 0.2f && rb.linearVelocityY >= 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY + 0.5f);
            }
            else if (rb.linearVelocityY > -0.2f && rb.linearVelocityY < 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY - 0.5f);
            }
        }
        
    }
    void handleGameOver()
    {
        gameManager.GameOver();
    }

    void MaintainBallSpeed()
    {
        //currentSpeed = baseSpeed * (1 + (Time.time / 300f)); // Optional: gradual speed increase
        rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
        
    }

}
