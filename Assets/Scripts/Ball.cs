using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Ball : MonoBehaviour
{
    public float moveSpeed;
    
    private Transform t;
    private Rigidbody2D rb;
    private CircleCollider2D cc;

    private bool _isDestroying;
    
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

    public void StartMoving()
    {
        var velocity = Random.insideUnitCircle.normalized;
        rb.velocity = moveSpeed * velocity;
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    private void DestroyAfterLifetime()
    {
        if (!GameManager.Instance) return;
        GameManager.Instance.ReuseBall(gameObject);
    }

    private void OnBecameInvisible()
    {
        if (!_isDestroying)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_isDestroying || !GameManager.Instance) return;
        _isDestroying = true;
        GameManager.Instance.BallsCount--;
        GameManager.Instance.SpawnBall();
    }

    private void OnApplicationQuit()
    {
        _isDestroying = true;
    }
}
