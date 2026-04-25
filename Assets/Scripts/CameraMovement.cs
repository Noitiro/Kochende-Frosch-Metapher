using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveInput;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        rb.linearVelocity = moveInput * cameraSpeed;
    }

    public void Move(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();
    }
}
