using UnityEditor;
using UnityEngine;

namespace Demo.Scripts.Editor
{
    [CustomEditor(typeof(Quest))]
    public class QuestEditor : UnityEditor.Editor
    {
        private Quest _quest;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _quest = target as Quest;

            EditorGUILayout.Space();
            if (GUILayout.Button($"Create new task"))
            {
                CreateTask();
            }
        }

        private void CreateTask()
        {
            Task task = CreateInstance<Task>();
            task.name = "New Task";
            task.Initialize(_quest);

            AssetDatabase.AddObjectToAsset(task, _quest);
            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(_quest);
            EditorUtility.SetDirty(task);
        }
    }
}