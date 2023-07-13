using UnityEditor;
using UnityEngine;

namespace Demo.Scripts.Editor
{
    [CustomEditor(typeof(Task))]
    public class TaskEditor : UnityEditor.Editor
    {
        private Task _task;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _task = target as Task;

            EditorGUILayout.Space();
            if (GUILayout.Button($"Update data"))
            {
                UpdateTask();
            }

            EditorGUILayout.Space();
            if (GUILayout.Button($"Delete data"))
            {
                DeleteTask();
            }
        }

        private void UpdateTask()
        {
            _task.name = _task.Name;
            _task.NPCName = _task.NPCName;
            _task.Dialogue = _task.Dialogue;
            _task.CompleteDialogue = _task.CompleteDialogue;
            _task.IncompleteDialogue = _task.IncompleteDialogue;
            _task.Type = _task.Type;
            _task.Status = _task.Status;

            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(_task);
        }

        private void DeleteTask()
        {
            _task.Quest.Tasks.Remove(_task);
            Undo.DestroyObjectImmediate(_task);
            AssetDatabase.SaveAssets();
        }
    }
}