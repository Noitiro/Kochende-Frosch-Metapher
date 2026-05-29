using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class TVMinigame : MonoBehaviour
{
    [SerializeField] private Animator animGrandma;
    [SerializeField] private TextAsset sentencesFile;
    [SerializeField] private TextMeshProUGUI wordText;
    [SerializeField] private int sentencesCount = 5;
    [SerializeField] private GameObject tvPanel;
    [SerializeField] private float textSpeed = 200f;
    [SerializeField] private float speedUp = 50f;

    public GameObject progressBarObject;
    private TextAnimation _textAnimation;

    public bool _isFinished;
    private List<string> _sentences;
    private int _charCount;
    private int _points;
    private string _currentSentence;
    private int _currentCharIndex;
    private Vector2 _resetTextPosition;
    private enum CharStates {
        Default, Bad, Good
    };
    private CharStates[] _charState;

    private void Awake()
    {
        _textAnimation = wordText.GetComponent<TextAnimation>();
    }

    public void StartTVMinigame()
    {
        _isFinished = false;
        _charCount = 0;
        _points = 0;
        _currentSentence = "";
        _currentCharIndex = 0;
        _resetTextPosition = new Vector2(Screen.width + 25, wordText.rectTransform.position.y);
        _textAnimation.SetTextSpeed(textSpeed);
        _textAnimation.enabled = true;
        _sentences = new List<string>(System.Array.ConvertAll(sentencesFile.text.Split('\n'), s => s.Trim()));
        Keyboard.current.onTextInput += OnTextInput;
        StartCoroutine(DisplaySentences());
        tvPanel.SetActive(true);
    }
    
    // void Start()
    // {
    // }
    //
    // private void OnDestroy()
    // {
    // }
    
    private void OnTextInput(char c)
    {
        
        if (_isFinished || _currentSentence == "") return;
        
        if(_currentCharIndex >= _currentSentence.Length)    return;

        if (c == _currentSentence[_currentCharIndex])
        {
            _charState[_currentCharIndex] = CharStates.Good;
        }
        else
        {
            _charState[_currentCharIndex] = CharStates.Bad;
        }

        _currentCharIndex++;
        UpdateText();
        
    }

    void Update()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            if (_currentCharIndex > 0)
            {
                _currentCharIndex--;
                _charState[_currentCharIndex] = CharStates.Default;
                UpdateText();
            }
        }
    }

    private IEnumerator DisplaySentences()
    {
        
        for (int i = 0; i < sentencesCount; i++)
        {
            
            int randomIndex = Random.Range(0, _sentences.Count);
            _currentSentence = _sentences[randomIndex];
            _sentences.RemoveAt(randomIndex);
            _charCount += _currentSentence.Length;
            
            _currentCharIndex = 0;
            if (_charState == null || _charState.Length != _currentSentence.Length)
            {
                _charState = new CharStates[_currentSentence.Length];
            }
            else
            {
                System.Array.Clear(_charState, 0, _charState.Length);
            }
            UpdateText();
            
            ResetTextPosition();
            
            yield return new WaitUntil(() =>
            {
                Rect r = wordText.rectTransform.rect;
                Vector3 rightEdge = wordText.rectTransform.TransformPoint(r.xMax, 0, 0);
                return rightEdge.x < 0f;
            });
            
            
            
            foreach (CharStates state in _charState)
            {
                if (state == CharStates.Good) _points++;
            }
            
            _textAnimation.SpeedUp(speedUp);
        }
        
        tvPanel.SetActive(false);
        animGrandma.SetTrigger("GrandmaTvOff");
        yield return new WaitForSeconds(3);
        _isFinished = true;
        Keyboard.current.onTextInput -= OnTextInput;
        _textAnimation.enabled = false;
        wordText.SetText("");
        Score();
    }

    private void UpdateText()
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < _currentSentence.Length; i++)
        {
            if (i == _currentCharIndex)
            {
                result.Append(_currentSentence[i] == ' '
                    ? "<color=white>_</color>"
                    : $"<u><color=white>{_currentSentence[i]}</color></u>");
            }
            else
            {
                switch (_charState[i])
                {
                    case CharStates.Good:
                        result.Append($"<color=green>{_currentSentence[i]}</color>");
                        break;
                    case CharStates.Bad:
                        result.Append($"<color=red>{_currentSentence[i]}</color>");
                        break;
                    default:
                        result.Append($"<color=grey>{_currentSentence[i]}</color>");
                        break;
                }
            }
        }
        
        wordText.SetText(result);
        wordText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, wordText.preferredWidth);
    }
    public void Score(){
        progressBarObject.GetComponent<ProgressBar>().gameResultHandler(((((float)_points / _charCount) * 100) + 33));
        Debug.Log("Score TV: "+ ((((float)_points / _charCount) * 100) + 33));
        Debug.Log(_isFinished);
    }

    private void ResetTextPosition()
    {
        wordText.rectTransform.position = _resetTextPosition;
    }
}
