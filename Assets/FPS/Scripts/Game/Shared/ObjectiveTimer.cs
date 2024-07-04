using UnityEngine;
using UnityEngine.UI;
using Unity;
using Unity.FPS;
using System;
using System.Collections;
using System.Collections.Generic;

// i'm going to eat my shoe

namespace Unity.FPS.Game
{
    public class ObjectiveTimer : MonoBehaviour
    {   
        // GENERIC ACTION DELEGATES
        public Action OnCreate; // invoked on creation
        public Action OnComplete; // invoked on completion
        public Action OnValueChange; // invoked on progress change

        // KILL COUNT FIELDS
        public string EventTrigger { get; } // triggers progress (can be empty if progress is managed elsewhere)
        public bool IsComplete { get; private set; }
        public int KillCount { get; private set; }

        // TIMER FIELDS
        public float TimeRemaining;
        public bool TimerIsRunning = false;
        public string TimerText { get; private set; }
        public int WarningThresh = 5;

        // used for basic formatting to display objective to player
        // uses string.Format() to provide KillCount and TimerText as format parameters, e.g. "Kill count: {0} / Time remaining: {1}"
        private readonly string _statusText;

        public ObjectiveTimer(string eventTrigger, string statusText)
        {
            EventTrigger = eventTrigger;
            _statusText = statusText;
            OnCreate?.Invoke();
        }

        public ObjectiveTimer(string statusText) : this("", statusText) {}

        private void CheckCompletion()
        {
            // if (KillCount >= MaxValue)
            // {
            //     IsComplete = true;
            //     OnComplete?.Invoke();
            // }
        }

        public void AddProgress(int value)
        {
            if (IsComplete)
            {
                return;
            }
            KillCount += value;
            OnValueChange?.Invoke();
            CheckCompletion();
        }

        void Update()
        {
            if (TimerIsRunning)
            {
                if (TimeRemaining > 0)
                {
                    TimeRemaining -= Time.deltaTime;
                    DisplayTime(TimeRemaining);
                }
                else
                {
                    Debug.Log("timer done");
                    TimeRemaining = 0;
                    TimerIsRunning = false;
                }
            }
        }

        public string GetStatusText()
        {
            return string.Format(_statusText, KillCount);
        }

        private void Start() {
            TimerIsRunning = true;
        }

        

        void DisplayTime(float timeToDisplay)
        {
            timeToDisplay++;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            TimerText = string.Format("{0:00}:{1:00}", minutes, seconds);

        }
    }
}