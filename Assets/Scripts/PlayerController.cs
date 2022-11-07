using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 normalisedMovement = Vector2.ClampMagnitude(movementVector, 1);
        rb.velocity = normalisedMovement * movementSpeed;

        animator.SetFloat("Horizontal", normalisedMovement.x);
        animator.SetFloat("Vertical", normalisedMovement.y);
    }
}
