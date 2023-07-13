using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Demo.Scripts
{
    [CreateAssetMenu(menuName = "Demo/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private List<Task> _tasks = new List<Task>();
        [SerializeField] private Status _status = Status.Incomplete;
        [SerializeField] private VoidEventChannelSO _onDoneQuest = default;

        public string Id => _id;
        public List<Task> Tasks => _tasks;

        public Status Status
        {
            get => _status;
            set => _status = value;
        }

        public VoidEventChannelSO OnDoneQuest => _onDoneQuest;

        public void FinishQuest()
        {
            if (_onDoneQuest != null)
            {
                _onDoneQuest.RaiseEvent();
            }

            _status = Status.Complete;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            var path = AssetDatabase.GetAssetPath(this);
            _id = AssetDatabase.AssetPathToGUID(path);
        }
#endif
    }
}