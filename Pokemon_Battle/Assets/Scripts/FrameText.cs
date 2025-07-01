using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class FrameText : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private float _timeBetweenLetters = 0.05f;
    [SerializeField]
    private float _timeToDisappear = 1f;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _ShowTextAnimationname = "ShowText";
    [SerializeField]
    private string _hideTextAnimationName = "HideText";
    private string _fulltext;
    private Coroutine _showTextCoroutine;

    public static FrameText Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    public void StopText(bool stopAnimation = false)
    {
        if (_showTextCoroutine != null)
        {
            StopCoroutine(_showTextCoroutine);
            _showTextCoroutine = null;
        }
        _text.text = "";
        if (stopAnimation)
        {
            _animator.Play(_hideTextAnimationName, 0 , 0f);
        }
    }

    public void ShowText(string text)
    {
        StopText();
        _animator.Play(_ShowTextAnimationname, 0, 0f);
        _showTextCoroutine = StartCoroutine(ShowTextCoroutine(text));
    }

    private IEnumerator ShowTextCoroutine(string text)
    {
        _fulltext = text;
        _text.text = "";
        foreach (char letter in _fulltext)
        {
            _text.text += letter;
            yield return new WaitForSeconds(_timeBetweenLetters);
        }
        yield return new WaitForSeconds(_timeToDisappear);
        _showTextCoroutine = null;
        _animator.Play(_hideTextAnimationName, 0, 0f);
    }
}
