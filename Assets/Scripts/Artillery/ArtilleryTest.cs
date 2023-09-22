using UnityEngine;

public class ArtilleryTest : MonoBehaviour
{
   [SerializeField] private Transform _firePoint;
   [SerializeField] private Rigidbody _CanonPrefab;
   [SerializeField] private Animator _animator;
   [SerializeField] private ParticleSystem _shootParticle;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space) && !_animator.GetBool("isShooting"))
      {
         var canon = Instantiate(_CanonPrefab, _firePoint.position, Quaternion.identity);
         float initialSpeed = 50f;
         canon.velocity = _firePoint.forward * initialSpeed;
         _shootParticle.Play();
         _animator.SetBool("isShooting", true);
         Invoke("DisableAnimation", 1f);
      }
   }

   private void DisableAnimation()
   {
      _animator.SetBool("isShooting", false);
   }
   
}
