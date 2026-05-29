using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventSwipe : MonoBehaviour
{
    [Header("Ustawienia Swipe")]
    public float swipeThreshold = 100f;
    public float maxRotation = 15f;

    [Header("Ustawienia Ruchu")]
    public float maxXMovement = 300f; 
    public float maxYMovement = -50f; 

    [Header("P³ynnoœæ")]
    public float smoothSpeed = 30f;
    public float swipeSensitivity = 0.1f;

    public event Action OnSwipedLeft;
    public event Action OnSwipedRight;
    public event Action<float> OnDragProgress;
    public event Action OnDragCanceled;

    private RectTransform rect;
    private Vector2 startPos;
    private float currentDragX = 0f;

    private float targetRotation = 0f;
    private Vector2 targetPosition;

    private bool isDragging = false;
    private Vector2 lastPointerPosition;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
        targetPosition = startPos;
    }

    void Update()
    {
        HandleInput();
        ApplyTransform();
    }

    private void HandleInput()
    {
        var pointer = Pointer.current;
        if (pointer == null) return;


        if (pointer.press.wasPressedThisFrame)
        {
            isDragging = true;
            currentDragX = 0f;
            lastPointerPosition = pointer.position.ReadValue();
        }


        if (pointer.press.isPressed && isDragging)
        {
            Vector2 currentPointerPosition = pointer.position.ReadValue();

            float deltaX = (currentPointerPosition.x - lastPointerPosition.x) * swipeSensitivity;
            lastPointerPosition = currentPointerPosition;

            currentDragX += deltaX;
            float progress = Mathf.Clamp(currentDragX / swipeThreshold, -1f, 1f);

            targetRotation = -progress * maxRotation;

            float moveX = progress * maxXMovement;

            float moveY = Mathf.Abs(progress) * maxYMovement;

            targetPosition = startPos + new Vector2(moveX, moveY);

            OnDragProgress?.Invoke(progress);
        }

        if (pointer.press.wasReleasedThisFrame && isDragging)
        {
            isDragging = false;

            if (currentDragX <= -swipeThreshold)
            {
                OnSwipedLeft?.Invoke();
            }
            else if (currentDragX >= swipeThreshold)
            {
                OnSwipedRight?.Invoke();
            }
            else
            {
                OnDragCanceled?.Invoke();
                ResetPhysicalPosition();
            }
        }
    }

    private void ApplyTransform()
    {
        float currentZ = Mathf.LerpAngle(transform.localEulerAngles.z, targetRotation, Time.deltaTime * smoothSpeed);
        transform.localEulerAngles = new Vector3(0, 0, currentZ);

        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, targetPosition, Time.deltaTime * smoothSpeed);
    }

    public void ResetPhysicalPosition()
    {
        if (rect != null)
        {
            targetRotation = 0f;
            targetPosition = startPos;
            currentDragX = 0f;
        }
    }
}