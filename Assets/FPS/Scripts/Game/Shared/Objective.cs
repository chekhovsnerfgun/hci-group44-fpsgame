using UnityEngine;
using System;

namespace Unity.FPS.Game
{
    public class Objective : MonoBehaviour
    {
        // action delegates that can be used to listen for state changes to update things like UI and objective manager
        public Action OnCreate; // invoked on creation
        public Action OnComplete; // invoked on completion
        public Action OnValueChange; // invoked on progress change

        public string EventTrigger { get; } // triggers progress (can be empty if progress is managed elsewhere)
        public bool IsComplete { get; private set; }
        public int MaxValue { get; }
        public int CurrentValue { get; private set; }

        // used for basic formatting to display objective to player
        // uses string.Format() to provide CurrentValue and MaxValue as format parameters, e.g. "Kill {0} of {1} enemies"
        private readonly string _statusText;

        public Objective(string eventTrigger, string statusText, int maxValue)
        {
            EventTrigger = eventTrigger;
            _statusText = statusText;
            MaxValue = maxValue;
            OnCreate?.Invoke();
        }

        public Objective(string statusText, int maxValue) : this("", statusText, maxValue) {}

        private void CheckCompletion()
        {
            if (CurrentValue >= MaxValue)
            {
                IsComplete = true;
                OnComplete?.Invoke();
            }
        }

        public void AddProgress(int value)
        {
            if (IsComplete)
            {
                return;
            }

            CurrentValue += value;

            if (CurrentValue > MaxValue)
            {
                CurrentValue = MaxValue;
            }

            OnValueChange?.Invoke();
            CheckCompletion();
        }

        public string GetStatusText()
        {
            return string.Format(_statusText, CurrentValue, MaxValue);
        }
    }
}