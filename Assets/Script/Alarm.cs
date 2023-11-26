using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Events;
using static System.Net.WebRequestMethods;
using Unity.VisualScripting;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] public AudioSource _audioSource;
    [SerializeField] private AlarmTrigger _alarmTrigger;

    private float _step = 0.1f;
    private float _targetVolume;
    private float delay = 0.5f;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _alarmTrigger.Entered += OnPlaySound;
        _alarmTrigger.Exited += OnDecreaseSound;
    }

    private void OnDisable()
    {
        _alarmTrigger.Entered -= OnPlaySound;
        _alarmTrigger.Exited -= OnDecreaseSound;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnPlaySound()
    {
        _audioSource.Play();

        _targetVolume = 1;

        PlayAlarm(_targetVolume);
    }

    private void OnDecreaseSound()
    {
        _targetVolume = 0;

        PlayAlarm(_targetVolume);
    }

    private void PlayAlarm(float targetVolume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeSound());
    }

    private IEnumerator ChangeSound()
    {
        var wait = new WaitForSeconds(delay);

        while (_targetVolume == 1 || _targetVolume == 0)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _step);
            yield return wait;
        }
    }
}
