using UnityEngine;
using System;
using System.Collections.Generic;

namespace Unity.FPS.Game
{
    public class ObjectivesManager : MonoBehaviour
    {
        public Action<Objective> OnObjectiveAdded;

        public List<Objective> Objectives { get; } = new();
        public static ObjectivesManager Instance { get; private set; }
        private readonly Dictionary<string, List<Objective>> _objectiveMap = new();

        private void Awake()
        {
            Instance = this;
        }

        public void AddObjective(Objective objective)
        {
            /*
              Adds an objective to the objective manager. 
              If the objective has an EventTrigger, its progress will be incremented by
              AddProgress when the event is triggered. Multiple objectives can have the
              same EventTrigger (i.e. MobKilled, ItemCollected, etc).
            */ 

            Objectives.Add(objective);

            if (!string.IsNullOrEmpty(objective.EventTrigger))
            {
                if (!_objectiveMap.ContainsKey(objective.EventTrigger))
                {
                    _objectiveMap.Add(objective.EventTrigger, new List<Objective>());
                }

                _objectiveMap[objective.EventTrigger].Add(objective);
            }

            OnObjectiveAdded?.Invoke(objective);
        }
        
        public void AddProgress(string eventTrigger, int value)
        {
            /*
              Updates progress based on event triggers.
            */

            if (!_objectiveMap.ContainsKey(eventTrigger)) 
            {
                return;
            }
            foreach (var objective in _objectiveMap[eventTrigger])
            {
                objective.AddProgress(value);
            }
        }
    }
}