using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Paddle : MonoBehaviour
{
    public string moveAxis = "Horizontal";
    public float moveSpeed = 10;

    private Transform t;
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    private float size;

    private void Awake()
    {
        t = transform;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        size = t.localScale.x * bc.size.x;
    }

    private void Update()
    {
        var axis = Input.GetAxisRaw(moveAxis);
        var newPosition = t.position + moveSpeed * Time.deltaTime * axis * Vector3.right;

        var gameFieldSize = GameManager.Instance.RightTop.x - GameManager.Instance.LeftBottom.x;
        newPosition.x = Mathf.Max(newPosition.x, (size - gameFieldSize) / 2);
        newPosition.x = Mathf.Min(newPosition.x, (gameFieldSize - size) / 2);
        
        rb.MovePosition(newPosition);
    }
}
