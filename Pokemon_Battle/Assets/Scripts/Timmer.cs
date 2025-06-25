using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timmer : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private List<Sprite> timertextures;

    [SerializeField]
    private List<string> _soundName;

    [SerializeField]
    private Image _timerImage;

    [SerializeField]
    private string showTimmerAnimationName = "ShowSeconds";

    [SerializeField]
    private UnityEvent _onSecondpassed;

    [SerializeField]
    private UnityEvent _onTimerFinisehd;
    private Coroutine _timerCoroutine;
    public void StartTimer(float duration)
    {
        _timerCoroutine = StartCoroutine(TimerCoroutine(duration));
    }

    private IEnumerator TimerCoroutine(float duration)
    {
        while (duration >= 0)
        {
            int index = Mathf.FloorToInt(duration);
            _onSecondpassed?.Invoke();
            _timerImage.sprite = timertextures[index];
            animator.Play(showTimmerAnimationName);
            duration--;
            SoundManager.instance.Play(_soundName[index]);
            yield return new WaitForSeconds(1f);
        }
        _onTimerFinisehd?.Invoke();
    }

    public void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;

        }
    }
}
