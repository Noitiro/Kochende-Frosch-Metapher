using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 40f;
    private RectTransform _rectTransform;
    private SceneManagerScript _sceneManager;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _sceneManager = FindFirstObjectByType<SceneManagerScript>();
    }
    
    void Update()
    {
        _rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            _sceneManager.LoadScene("Menu");
        }
    }
}
