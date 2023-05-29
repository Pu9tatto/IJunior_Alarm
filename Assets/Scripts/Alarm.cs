using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeChangeDuration = 2f;

    private float _runningTime;
    private IEnumerator _currentCoroutine;

    public void StartAlarm()
    {
        _audioSource.Play();

        StartNewCoroutine(VolumeUp());
    }

    public void StopAlarm()
    {
        StartNewCoroutine(VolumeDown());
    }

    private IEnumerator VolumeUp()
    {
        while (_audioSource.volume < 1)
        {
            _runningTime += Time.deltaTime;
            _audioSource.volume = _runningTime / _volumeChangeDuration;

            yield return null;
        }
    }

    private IEnumerator VolumeDown()
    {
        while (_audioSource.volume > 0)
        {
            _runningTime -= Time.deltaTime;
            _audioSource.volume = _runningTime / _volumeChangeDuration;

            yield return null;
        }

        _audioSource.Stop();
    }

    private void StartNewCoroutine(IEnumerator coroutine)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = coroutine;
        StartCoroutine(coroutine);
    }
}
