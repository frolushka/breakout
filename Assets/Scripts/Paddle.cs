using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Paddle : MonoBehaviour
{
    [SerializeField] private string moveAxis = "Horizontal";
    [SerializeField] private float moveSpeed = 10;

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

        newPosition.x = Mathf.Max(newPosition.x, ScreenUtility.LeftSideCoordinate + size / 2);
        newPosition.x = Mathf.Min(newPosition.x, ScreenUtility.RightSideCoordinate - size / 2);
        
        rb.MovePosition(newPosition);
    }
}
