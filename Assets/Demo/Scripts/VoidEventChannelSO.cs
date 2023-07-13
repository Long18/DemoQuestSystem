using UnityEngine;
using UnityEngine.Events;

namespace Demo.Scripts
{
    [CreateAssetMenu(menuName = "Demo/Void Event Channel")]
    public class VoidEventChannelSO : ScriptableObject
    {
        public event UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            if (OnEventRaised == null)
            {
                Debug.LogWarning($"An event {name} was raised but nobody picked it up.");
                return;
            }

            OnEventRaised.Invoke();
        }
    }
}