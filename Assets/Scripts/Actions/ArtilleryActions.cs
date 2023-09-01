using System;
using UnityEngine;

namespace ScriptableActions
{
    [CreateAssetMenu(fileName = "ArtilleryAction", menuName = "Game/Actions/ArtilleryAction")]
    public class ArtilleryActions : ScriptableObject
    {
        public Action<bool> OnTargetDetected;

        public void RaiseTargetDetected(bool isDetected)
        {
            OnTargetDetected?.Invoke(isDetected);
        }
    }
}