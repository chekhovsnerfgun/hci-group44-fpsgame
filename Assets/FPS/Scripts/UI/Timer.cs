using Unity.FPS.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class Timer : MonoBehaviour
    {
        public float timeRemaining = 120;
        public bool timerIsRunning = false;
        public Text timeText;

        private void Start()
        {
            timerIsRunning = true;
        }

        void Update()
        {   
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    Debug.Log("timer done");
                    timeRemaining = 0;
                    timerIsRunning = false;
                }
            }
        }

        void DisplayTime(float timeToDisplay)
        {
            timeToDisplay++;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}