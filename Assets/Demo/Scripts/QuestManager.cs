﻿using System.Collections.Generic;using UnityEngine;namespace Demo.Scripts{    [CreateAssetMenu(menuName = "Demo/QuestManager", order = 0)]    public class QuestManager : MonoBehaviour    {        [Header("Data")] [SerializeField] private List<Quest> _quests;        [Header("Listen events")] [SerializeField]        private VoidEventChannelSO _onContinueWithTask = default;        [SerializeField] private VoidEventChannelSO _onPlayCompleteDealog = default;        [SerializeField] private VoidEventChannelSO _onPlayIncompleteDealog = default;        [Header("Raise events")] [SerializeField]        private VoidEventChannelSO _onStartCutscene = default;        [Header("For debug")] [SerializeField] private Quest _currentQuest;        [SerializeField] private Task _currentTask;        [SerializeField] private int _currentQuestIndex = 0;        [SerializeField] private int _currentTaskIndex = 0;        private void OnEnable()        {            _onContinueWithTask.OnEventRaised += CheckTaskValid;            StartQuest();        }        private void OnDisable()        {            _onContinueWithTask.OnEventRaised -= CheckTaskValid;        }        private void CheckTaskValid()        {            if (_currentTask == null) return;            Debug.Log($"Check task {_currentTask.name}");            switch (_currentTask.Type)            {                case TaskType.Dialogue:                    if (!string.IsNullOrEmpty(_currentTask.Dialogue))                    {                        _onPlayCompleteDealog.RaiseEvent();                        EndTask();                    }                    break;                default:                    EndTask();                    break;            }        }        private void EndTask()        {            _currentTask = null;            if (_currentTask != null) return;            if (_currentQuest.Status == Status.Complete)            {                _currentQuest.Tasks.ForEach(t => t.FinishTask());                Debug.Log($"End quest {_currentQuest.name}");                return;            }            if (_currentQuest.Tasks.Count > _currentTaskIndex)            {                _currentQuest.Tasks[_currentTaskIndex].FinishTask();                if (_currentQuest.Tasks.Count > _currentTaskIndex + 1)                {                    _currentTaskIndex++;                    StartTask();                }                else                {                    EndQuest();                }            }        }        private void EndQuest()        {            if (_currentQuest == null) return;            _currentQuest.FinishQuest();            _currentQuestIndex = -1;            _currentQuest = null;        }        private void StartTask()        {            if (_currentQuest.Tasks == null || _currentQuest.Tasks.Count == 0) return;            if (_currentQuest.Tasks.Count > _currentTaskIndex)            {                _currentTask = _currentQuest.Tasks[_currentTaskIndex];                _currentQuest.Tasks[_currentTaskIndex].Status = Status.InProgress;                Debug.Log($"Start task {_currentTask.name}");            }            else            {                Debug.Log($"You have completed all tasks in this quest");            }        }        private void StartQuest()        {            if (_quests == null || _quests.Count == 0) return;            if (_currentQuest != null)            {                if (_currentQuest.Status != Status.Complete) return;            }            if (_quests.Exists(q => q.Status != Status.Complete))            {                _currentQuestIndex = _quests.FindIndex(q => q.Status != Status.Complete);                if (_currentQuestIndex < 0) return;                _currentQuest = _quests.Find(q => q.Status != Status.Complete);                _currentQuest.Status = Status.InProgress;                if (_currentQuest.Tasks.Count > 0)                    StartTask();            }        }    }}