using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    
    private Transform t;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    
    private void Awake()
    {
        t = transform;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        var velocity = Random.insideUnitCircle;
        rb.velocity = moveSpeed * velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contactCount != 1) return;
        rb.velocity = Vector3.Reflect(rb.velocity, other.contacts[0].normal);
    }
}
