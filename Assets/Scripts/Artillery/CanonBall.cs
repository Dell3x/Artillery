using System;
using ScriptableActions;
using UnityEngine;

namespace Artilleries
{
   public class CanonBall : MonoBehaviour
   {
      [SerializeField] private LineRenderer _canonBallLineRenderer;
      [SerializeField] private Rigidbody _canonBallRigidbody;
      [SerializeField] private ParticleSystem _explosionPrefab;

      public Rigidbody CanonBallRigidbody => _canonBallRigidbody;
      public LineRenderer CanonBallLineRenderer => _canonBallLineRenderer;
      
      private void OnCollisionEnter(Collision collision)
      {
         Vector3 contactNormal = collision.contacts[0].normal;
         Quaternion rotation = Quaternion.LookRotation(contactNormal);
         var effect = Instantiate(_explosionPrefab, transform.position, rotation);
         Destroy(gameObject);
      }

      private void Awake()
      {
         _canonBallRigidbody.velocity = transform.forward * 50f;
      }
   }
}