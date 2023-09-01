using ScriptableActions;
using UnityEngine;

namespace Artilleries
{
   public class CanonBall : MonoBehaviour
   {
      [SerializeField] private ArtilleryActions _artilleryActions;
      [SerializeField] private LineRenderer _canonBallLineRenderer;
      [SerializeField] private Rigidbody _canonBallRigidbody;

      public Rigidbody CanonBallRigidbody => _canonBallRigidbody;
      public LineRenderer CanonBallLineRenderer => _canonBallLineRenderer;

      private void OnCollisionEnter(Collision other)
      {
         _artilleryActions.RaiseHitDetected(this);
      }
   }
}