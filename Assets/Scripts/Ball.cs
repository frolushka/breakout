using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Ball : MonoBehaviour
{
    public float moveSpeed;
    
    private Transform t;
    private Rigidbody2D rb;
    private CircleCollider2D cc;

    private bool _leftScreen;
    
    private void Awake()
    {
        t = transform;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        Invoke(nameof(StartMoving), 1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contactCount != 1) return;
        if (other.gameObject.CompareTag("Player"))
            rb.velocity = (t.position - other.transform.position).normalized * moveSpeed;
        else
            rb.velocity = Vector3.Reflect(rb.velocity, other.contacts[0].normal);
    }

    private void OnBecameInvisible()
    {
        GameManager.Instance.BallLeftScreen();
        _leftScreen = true;
        Destroy(gameObject);
    }

    private void StartMoving()
    {
        var velocity = Random.insideUnitCircle.normalized;
        rb.velocity = moveSpeed * velocity;
    }

    private void OnDestroy()
    {
        if (!_leftScreen)
            GameManager.Instance.ballSpawnQueue++;
    }
}
