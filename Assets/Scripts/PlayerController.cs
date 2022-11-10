using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;

    [Header("Interaction")]
    [SerializeField] private KeyCode interactKey = KeyCode.O;

    private Rigidbody2D rb;
    private Animator animator;

    private GridController gridController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        gridController = FindObjectOfType<GridController>();
    }

    private void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 clampedMovement = Vector2.ClampMagnitude(movementVector, 1);
        Vector2 normMovement = movementVector.normalized;

        rb.velocity = clampedMovement * movementSpeed;

        animator.SetFloat("Horizontal", clampedMovement.x);
        animator.SetFloat("Vertical", clampedMovement.y);

        Vector2 highlightPos = (Vector2)transform.position + normMovement;
        gridController.HighlightTile(highlightPos);

        Debug.DrawRay(transform.position, normMovement, Color.green);

        if (Input.GetKeyDown(KeyCode.O) && gridController.GetTileGameObject(highlightPos, out GameObject tileObject))
        {
            print(tileObject.name);
        }
    }
}
