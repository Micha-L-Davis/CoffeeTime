using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace eMeLDi.CoffeeTime
{
    public class Timer : MonoBehaviour
    {
        System.Timers.Timer systemTimer;

        int _minutes, _seconds, _hundredthSeconds;
        [SerializeField]
        TMP_Text _minutesText;
        [SerializeField]
        TMP_Text _secondsText;
        [SerializeField]
        TMP_Text _hundredthSecondsText;

        [SerializeField]
        TMP_Text _bloomText;
        [SerializeField]
        TMP_Text _firstPourText;
        [SerializeField]
        TMP_Text _secondPourText;
        [SerializeField]
        TMP_Text _waitText;

        [SerializeField]
        AudioSource _audioSource;
        [SerializeField]
        AudioClip _startAudio;
        [SerializeField]
        AudioClip _stopAudio;
        [SerializeField]
        AudioClip _warningAudio;
        [SerializeField]
        AudioClip _completeAudio;
        [SerializeField]
        AudioClip _waitAudio;

        [SerializeField]
        bool _timerIsRunning;
        [SerializeField]
        bool _waitHasPlayed;
        [SerializeField]
        Button _resetButton;

        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            systemTimer = new System.Timers.Timer(10);
            systemTimer.Elapsed += DecrementTimer;
            ResetTimer();
        }

        public void ResetTimer()
        {
            _minutes = 4;
            _seconds = 0;
            _hundredthSeconds = 0;
            UpdateTimerDisplay();

            _bloomText.gameObject.SetActive(false);
            _waitText.gameObject.SetActive(false);
            _firstPourText.gameObject.SetActive(false);
            _secondPourText.gameObject.SetActive(false);
        }

        void FixedUpdate()
        {
            if (_timerIsRunning)
            {
                UpdateTimerDisplay();
                CheckStatus();
            }
        }

        private void UpdateTimerDisplay()
        {
            _minutesText.text = _minutes.ToString("D2");
            _secondsText.text = _seconds.ToString("D2");
            _hundredthSecondsText.text = _hundredthSeconds.ToString("D2");
        }

        private void CheckStatus()
        {
            if (_minutes == 3 && _seconds <= 59 && _seconds > 45)
            {
                _bloomText.gameObject.SetActive(true);
            }
            if (_minutes == 3 && _seconds <= 45 && _seconds > 30)
            {
                _bloomText.gameObject.SetActive(false);
                _waitText.gameObject.SetActive(true);
                if (!_audioSource.isPlaying && !_waitHasPlayed)
                {
                    _waitHasPlayed = true;
                    _audioSource.PlayOneShot(_waitAudio);
                }
            }
            if (_minutes == 3 && _seconds == 34 && _hundredthSeconds <= 50)
            {
                if (!_audioSource.isPlaying)
                    _audioSource.PlayOneShot(_warningAudio);
                _waitHasPlayed = false;
            }
            if (_minutes == 3 && _seconds <= 29 && _seconds > 0)
            {
                _waitText.gameObject.SetActive(false);
                _firstPourText.gameObject.SetActive(true);
            }
            if (_minutes == 2 && _seconds <= 59 && _seconds > 30)
            {
                _firstPourText.gameObject.SetActive(false);
                _waitText.gameObject.SetActive(true);
                if (!_audioSource.isPlaying && !_waitHasPlayed)
                {
                    _waitHasPlayed = true;
                    _audioSource.PlayOneShot(_waitAudio);
                }
            }
            if (_minutes == 2 && _seconds == 34 && _hundredthSeconds <= 50)
            {
                if (!_audioSource.isPlaying)
                    _audioSource.PlayOneShot(_warningAudio);
            }
            if (_minutes == 2 && _seconds <= 30 && _seconds > 0)
            {
                _waitText.gameObject.SetActive(false);
                _secondPourText.gameObject.SetActive(true);
                _waitHasPlayed = false;
            }
            if (_minutes <= 1 && _seconds <= 59)
            {
                _secondPourText.gameObject.SetActive(false);
                if (!_audioSource.isPlaying && !_waitHasPlayed)
                {
                    _waitHasPlayed = true;
                    _audioSource.PlayOneShot(_waitAudio);
                }
            }

            if (_hundredthSeconds <= 0 && _seconds <= 0 && _minutes <= 0)
            {
                _hundredthSeconds = 0;
                systemTimer.Stop();
                _resetButton.interactable = true;
                _timerIsRunning = false;
                if (!_audioSource.isPlaying)
                    _audioSource.PlayOneShot(_completeAudio);
            }
        }

        public void StartStopTimer()
        {
            if (!_timerIsRunning)
            {
                _resetButton.interactable = false;
                systemTimer.Start();
                _audioSource.PlayOneShot(_startAudio);
                _timerIsRunning = true;
            }
            else
            {
                _resetButton.interactable = true;
                systemTimer.Stop();
                _audioSource.PlayOneShot(_stopAudio);
                _timerIsRunning = false;
            }
        }

        void DecrementTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            _hundredthSeconds--;

            if (_hundredthSeconds < 0 && (_seconds > 0 || _minutes > 0))
            {
                _hundredthSeconds = 99;

                _seconds--;
            }

            if (_seconds < 0 && _minutes > 0)
            {
                _seconds = 59;
                _minutes--;
            }
            else if (_seconds < 0 && _minutes <= 0)
            {
                _seconds = 0;
            }

            if (_minutes < 0)
            {
                _minutes = 0;
            }
        }
    }
}

