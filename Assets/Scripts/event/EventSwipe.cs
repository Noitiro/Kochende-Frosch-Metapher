using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSwipe : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public float swipeThreshold = 100f;
    public float maxRotation = 15f;
    public float smoothSpeed = 15f;

    public event Action OnSwipedLeft;
    public event Action OnSwipedRight;
    public event Action<float> OnDragProgress;
    public event Action OnDragCanceled;

    private RectTransform rect;
    private Vector2 startPos;
    private float currentDragX = 0f;
    private float targetRotation = 0f;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
    }

    void Update()
    {
        float currentZ = Mathf.LerpAngle(transform.localEulerAngles.z, targetRotation, Time.deltaTime * smoothSpeed);
        transform.localEulerAngles = new Vector3(0, 0, currentZ);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentDragX = 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentDragX += eventData.delta.x;
        float progress = Mathf.Clamp(currentDragX / swipeThreshold, -1f, 1f);

        targetRotation = -progress * maxRotation;
        OnDragProgress?.Invoke(progress);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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

    public void ResetPhysicalPosition()
    {
        if (rect != null)
        {
            rect.anchoredPosition = startPos;
            targetRotation = 0f;
            currentDragX = 0f;
        }
    }
}