using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays a sequence of messages on a TMP text field.
/// Call Play() from another script to activate the sequence.
/// The UI starts invisible and fades in on Play().
/// An assigned image grows from imageSizeStart to imageSizeEnd over the full sequence duration.
/// After the final message, uiRoot is hidden.
/// </summary>
public class IntroMessageSequence : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The TextMeshPro field to display messages on.")]
    public TMP_Text messageText;

    [Tooltip("The root GameObject with all UI elements. Hidden after the last message. Defaults to this GameObject.")]
    public GameObject uiRoot;

    [Tooltip("CanvasGroup on uiRoot for alpha control. Created automatically if missing.")]
    public CanvasGroup uiCanvasGroup;

    [Tooltip("RectTransform of the image to grow over the full sequence.")]
    public RectTransform animatedImage;

    [Header("Image Size Animation")]
    public Vector2 imageSizeStart = new Vector2(960f, 540f);
    public Vector2 imageSizeEnd   = new Vector2(1344f, 756f);

    [System.Serializable]
    public class MessageEntry
    {
        [TextArea(2, 5)]
        public string message;
        [Tooltip("How long this message stays on screen (seconds).")]
        public float duration = 3f;
    }

    [Header("Messages")]
    public List<MessageEntry> messages = new List<MessageEntry>()
    {
        new MessageEntry { message = "Hello comrade, welcome to the Center for Digital Evaluation.", duration = 5f },
        new MessageEntry { message = "You've been recruited to aid with the annual citizen evaluation.", duration = 5f },
        new MessageEntry { message = "Your job is to take control of the test subject's media devices, in order to sway their mindset to trust our agenda.", duration = 5f },
        new MessageEntry { message = "An automated system will judge the subject as you go along.", duration = 5f },
        new MessageEntry { message = "Remember, the score received by the subject is largely a reflection of your own effort.", duration = 5f },
        new MessageEntry { message = "This assessment will run over a span of seven days, after which a final score and suggested action will be determined.", duration = 5f },
        new MessageEntry { message = "We expect the highest levels of effort, this position is highly respected, and as you know, the pay reflects it.", duration = 5f },
        new MessageEntry { message = "Glory to the Supreme Leader.", duration = 5f },
    };

    [Header("Settings")]
    [Tooltip("Duration of the fade-in when Play() is called (seconds).")]
    public float fadeInDuration = 0.5f;

    [Tooltip("Fade duration between messages (0 = instant swap).")]
    [Range(0f, 1f)]
    public float messageFadeDuration = 0.5f;

    // ── Internal ──────────────────────────────────────────────────────────────
    private bool _isPlaying = false;

    // ── Lifecycle ─────────────────────────────────────────────────────────────

    private void Awake()
    {
        if (uiRoot == null)
            uiRoot = gameObject;

        // Auto-create CanvasGroup if needed
        if (uiCanvasGroup == null)
            uiCanvasGroup = uiRoot.GetComponent<CanvasGroup>();
        if (uiCanvasGroup == null)
            uiCanvasGroup = uiRoot.AddComponent<CanvasGroup>();

        // Start fully invisible and non-interactive
        uiCanvasGroup.alpha          = 0f;
        uiCanvasGroup.interactable   = false;
        uiCanvasGroup.blocksRaycasts = false;

        // Snap image to start size immediately
        if (animatedImage != null)
            animatedImage.sizeDelta = imageSizeStart;

        //Play();
    }

    // ── Public API ────────────────────────────────────────────────────────────

    /// <summary>
    /// Activate and play the intro sequence.
    /// Ignored if already playing.
    /// </summary>
    public void Play()
    {
        if (_isPlaying) return;

        if (messageText == null)
        {
            Debug.LogError("[IntroMessageSequence] No TMP_Text assigned!", this);
            return;
        }

        // Ensure the GameObject is active so coroutines can run
        gameObject.SetActive(true);

        _isPlaying = true;
        StartCoroutine(RunSequence());
    }
    

    // ── Sequence ──────────────────────────────────────────────────────────────

    private IEnumerator RunSequence()
    {
        // Sum total duration (hold time + fade gaps between messages)
        float totalDuration = 0f;
        foreach (var entry in messages)
            totalDuration += entry.duration;
        if (messages.Count > 1)
            totalDuration += messageFadeDuration * 2f * (messages.Count - 1);

        // Fade the UI in
        yield return StartCoroutine(FadeUI(0f, 1f, fadeInDuration));
        uiCanvasGroup.interactable   = true;
        uiCanvasGroup.blocksRaycasts = true;

        // Image growth runs in parallel with the message loop
        StartCoroutine(AnimateImageSize(totalDuration));

        // Message loop
        for (int i = 0; i < messages.Count; i++)
        {
            MessageEntry entry = messages[i];

            if (messageFadeDuration > 0f)
            {
                yield return StartCoroutine(FadeText(1f, 0f, messageFadeDuration));
                messageText.text = entry.message;
                yield return StartCoroutine(FadeText(0f, 1f, messageFadeDuration));
            }
            else
            {
                messageText.text = entry.message;
            }

            yield return new WaitForSeconds(entry.duration);
        }

        // Done — hide everything
        uiRoot.SetActive(false);
        _isPlaying = false;
    }

    // ── Image animation ───────────────────────────────────────────────────────

    private IEnumerator AnimateImageSize(float totalDuration)
    {
        if (animatedImage == null || totalDuration <= 0f) yield break;

        float elapsed = 0f;
        while (elapsed < totalDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / totalDuration);
            animatedImage.sizeDelta = Vector2.Lerp(imageSizeStart, imageSizeEnd, t);
            yield return null;
        }
        animatedImage.sizeDelta = imageSizeEnd;
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private IEnumerator FadeUI(float from, float to, float duration)
    {
        if (duration <= 0f) { uiCanvasGroup.alpha = to; yield break; }
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            uiCanvasGroup.alpha = Mathf.Lerp(from, to, Mathf.Clamp01(elapsed / duration));
            yield return null;
        }
        uiCanvasGroup.alpha = to;
    }

    private IEnumerator FadeText(float from, float to, float duration)
    {
        if (duration <= 0f) { SetTextAlpha(to); yield break; }
        float elapsed = 0f;
        Color c = messageText.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, Mathf.Clamp01(elapsed / duration));
            messageText.color = c;
            yield return null;
        }
        SetTextAlpha(to);
    }

    private void SetTextAlpha(float alpha)
    {
        Color c = messageText.color;
        c.a = alpha;
        messageText.color = c;
    }
}