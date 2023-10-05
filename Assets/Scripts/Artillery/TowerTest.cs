using System;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class TowerTest : MonoBehaviour
{
   [SerializeField] private Transform _firePoint;
   [SerializeField] private Rigidbody _CanonPrefab;
   [SerializeField] private Animator _animator;
   [SerializeField] private ParticleSystem _gunParticleRight;
   [SerializeField] private ParticleSystem _gunParticleLeft;
   [SerializeField] private ParticleSystem _gunParticleMain;

   [ProButton]
   private void ReadyToFire()
   {
      _animator.SetBool(GetAnimatorParameter(AnimatorParameters.IsReadyToFire), true);
      _animator.SetBool(GetAnimatorParameter(AnimatorParameters.IsPistonDown), true);

   }
   
   [ProButton]
   private void FromFireToIdle()
   {
      _animator.SetBool(GetAnimatorParameter(AnimatorParameters.IsReadyToFire), false);
      _animator.SetBool(GetAnimatorParameter(AnimatorParameters.IsPistonDown), false);

   }
   
   [ProButton]
   private void FireRightGun()
   {
      _animator.SetTrigger(GetAnimatorParameter(AnimatorParameters.FireRight));
      _animator.SetTrigger(GetAnimatorParameter(AnimatorParameters.MountVibrationRight));
      _gunParticleRight.Play();
   }
   
   [ProButton]
   private void FireLeftGun()
   {
      _animator.SetTrigger(GetAnimatorParameter(AnimatorParameters.FireLeft));
      _animator.SetTrigger(GetAnimatorParameter(AnimatorParameters.MountVibrationLeft));
      _gunParticleLeft.Play();
   }
   
   [ProButton]
   private void FireMainGun()
   {
      _animator.SetTrigger(GetAnimatorParameter(AnimatorParameters.Fire));
      _gunParticleMain.Play();
   }

   private string GetAnimatorParameter(Enum enumState)
   {
      return enumState.ToString();
   }

   private enum AnimatorParameters
   {
      IsPistonDown,
      IsReadyToFire,
      MountVibrationRight,
      MountVibrationLeft,
      FireRight,
      FireLeft,
      Fire
   }

   
   
   
   
   
   
   
   
   
   
   
   
   
   
   // private void Update()
   // {
   //    if (Input.GetKeyDown(KeyCode.Space) && !_animator.GetBool("isShooting"))
   //    {
   //       var canon = Instantiate(_CanonPrefab, _firePoint.position, Quaternion.identity);
   //       float initialSpeed = 50f;
   //       canon.velocity = _firePoint.forward * initialSpeed;
   //       _shootParticle.Play();
   //       _animator.SetBool("isShooting", true);
   //       Invoke("DisableAnimation", 1f);
   //    }
   // }

   // private void DisableAnimation()
   // {
   //    _animator.SetBool("isShooting", false);
   // }
   
}
