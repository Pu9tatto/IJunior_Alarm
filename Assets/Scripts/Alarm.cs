using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeChangeDuration = 2f;

    private bool _isFinishedRunningTime = false;
    private float _runningTime;
    private float _minRunningTime = 0;
    private float _normalizedVolume;
    private float _minVolume = 0;
    private float _maxVolume = 1;
    private IEnumerator _currentCoroutine;

    public void StartAlarm()
    {
        StartNewCoroutine(VolumeChange(true));
    }

    public void StopAlarm()
    {
        StartNewCoroutine(VolumeChange(false));
    }

    private IEnumerator VolumeChange(bool isVolumeUp)
    {
        while (_isFinishedRunningTime == false)
        {
            if (isVolumeUp)
                _runningTime += Time.deltaTime;
            else
                _runningTime -= Time.deltaTime;

            _normalizedVolume = _runningTime / _volumeChangeDuration;

            _audioSource.volume = Mathf.MoveTowards(_minVolume, _maxVolume, _normalizedVolume);

            CheckFinishRunningTime();

            yield return null;
        }
    }

    private void CheckFinishRunningTime()
    {
        if (_runningTime > _volumeChangeDuration)
        {
            _runningTime = _volumeChangeDuration;
            _isFinishedRunningTime = true;
        }
        else if (_runningTime < _minRunningTime)
        {
            _runningTime = _minRunningTime;
            _isFinishedRunningTime = true;
        }
    }

    private void StartNewCoroutine(IEnumerator coroutine)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _isFinishedRunningTime = false;
        _currentCoroutine = coroutine;
        StartCoroutine(coroutine);
    }
}
