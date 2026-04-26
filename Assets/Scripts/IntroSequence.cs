using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Displays a sequence of messages on a TMP text field.
/// Each message has a configurable display duration.
/// After the final message, the root GameObject is hidden.
/// </summary>
public class IntroSequence : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The TextMeshPro field to display messages on.")]
    public TMP_Text messageText;

    [Tooltip("The root GameObject containing all UI elements (background, image, TMP field). This will be hidden after the last message.")]
    public GameObject uiRoot;

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
        new MessageEntry { message = "Hello comrade, welcome to the Center for Digital Evaluation.", duration = 3f },
        new MessageEntry { message = "You've been recruited to aid with the annual citizen evaluation.", duration = 3f },
        new MessageEntry { message = "Your job is to take control of the test subject's media devices, in order to sway their mindset to trust our agenda.", duration = 3f },
        new MessageEntry { message = "An automated system will judge the subject as you go along.", duration = 3f },
        new MessageEntry { message = "Remember, the score received by the subject is largely a reflection of your own effort.", duration = 3f },
        new MessageEntry { message = "This assessment will run over a span of seven days, after which a final score and suggested action will be determined.", duration = 3f },
        new MessageEntry { message = "We expect the highest levels of effort, this position is highly respected, and as you know, the pay reflects it.", duration = 3f },
        new MessageEntry { message = "Glory to the Supreme Leader.", duration = 3f },
    };

    [Header("Settings")]
    [Tooltip("Optional fade duration between messages (0 = instant swap).")]
    [Range(0f, 1f)]
    public float fadeDuration = 0.3f;

    private void Start()
    {
        if (messageText == null)
        {
            Debug.LogError("[IntroMessageSequence] No TMP_Text assigned!", this);
            return;
        }

        if (uiRoot == null)
            uiRoot = gameObject;

        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        for (int i = 0; i < messages.Count; i++)
        {
            MessageEntry entry = messages[i];

            if (fadeDuration > 0f)
            {
                // Fade out
                yield return StartCoroutine(FadeText(1f, 0f));
                messageText.text = entry.message;
                // Fade in
                yield return StartCoroutine(FadeText(0f, 1f));
            }
            else
            {
                messageText.text = entry.message;
            }

            // Hold for this message's duration
            yield return new WaitForSeconds(entry.duration);
        }

        // All messages shown — hide the UI
        uiRoot.SetActive(false);
    }

    private IEnumerator FadeText(float from, float to)
    {
        float elapsed = 0f;
        Color c = messageText.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            c.a = Mathf.Lerp(from, to, t);
            messageText.color = c;
            yield return null;
        }

        c.a = to;
        messageText.color = c;
    }
}
