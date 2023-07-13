using UnityEngine;

namespace Demo.Scripts
{
    [CreateAssetMenu(menuName = "Demo/Task")]
    public class Task : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _npcName;

        [Header("Dialogue")] [SerializeField] private string _dialogue;
        [SerializeField] private string _compeleteDialogue;
        [SerializeField] private string _incompleteDialogue;
        [SerializeField] private TaskType _type = TaskType.None;
        [SerializeField] private Status _status = Status.Incomplete;

        [Header("Raise event")] [SerializeField]
        private VoidEventChannelSO _onDoneTask = default;

        public string Name => _name;

        private Quest _quest;
        public Quest Quest => _quest;

        public string NPCName
        {
            get => _npcName;
            set => _npcName = value;
        }

        public string Dialogue
        {
            get => _dialogue;
            set => _dialogue = value;
        }

        public string CompleteDialogue
        {
            get => _compeleteDialogue;
            set => _compeleteDialogue = value;
        }

        public string IncompleteDialogue
        {
            get => _incompleteDialogue;
            set => _incompleteDialogue = value;
        }

        public TaskType Type
        {
            get => _type;
            set => _type = value;
        }

        public Status Status
        {
            get => _status;
            set => _status = value;
        }

        public VoidEventChannelSO OnDoneTask => _onDoneTask;

#if UNITY_EDITOR
        public void Initialize(Quest quest)
        {
            _quest = quest;
        }
#endif

        public void FinishTask()
        {
            if (_onDoneTask != null)
            {
                _onDoneTask.RaiseEvent();
            }

            _status = Status.Complete;
        }
    }
}