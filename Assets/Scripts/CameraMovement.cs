using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D border;
    private Vector2 moveInput;
    private float minX, maxX, minY, maxY;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        border = border.GetComponent<Collider2D>();
        minX = border.bounds.min.x;
        maxX = border.bounds.max.x;
        minY = border.bounds.min.y;
        maxY = border.bounds.max.y;
    }

    void FixedUpdate(){
        Vector3 targetPosition = transform.position + (Vector3)moveInput * cameraSpeed * Time.deltaTime;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minX + 2, maxX - 2);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY + 2, maxY - 2);

        transform.position = targetPosition;
    }

    public void Move(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();
    }
}
