using System;
using Artilleries;
using UnityEngine;

namespace ScriptableActions
{
    [CreateAssetMenu(fileName = "ArtilleryAction", menuName = "Game/Actions/ArtilleryAction")]
    public class ArtilleryActions : ScriptableObject
    {
        public Action<bool> TargetDetected;
        public Action<Transform> SetUpTargetTransform;
        public Action<CanonBall> HitDetected;

        public void RaiseTargetDetected(bool isDetected)
        {
            TargetDetected?.Invoke(isDetected);
        }

        public void RaiseSetUpTargetTransform(Transform targetTransform)
        {
            SetUpTargetTransform?.Invoke(targetTransform);
        }

        public void RaiseHitDetected(CanonBall currentBall)
        {
            HitDetected?.Invoke(currentBall);
        }
    }
}