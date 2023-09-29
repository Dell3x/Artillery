using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class AntiAirAnimation : MonoBehaviour
{
    [SerializeField] private Animation _cogitatorAnimation;
    [SerializeField] private float _mountPlaySpeed;
    [SerializeField] private Animation _mountAnimation;
    [SerializeField] private Animation _crankLeftAnimation;
    [SerializeField] private Animation _crankRightAnimation;
    [SerializeField] private Animation _gearLeftAnimation;
    [SerializeField] private Animation _gearRightAnimation;
    [SerializeField] private Animation _pistonLeftAnimation;
    [SerializeField] private Animation _pistonRightAnimation;
    [SerializeField] private float _gunPlaySpeed;
    [SerializeField] private Animation _gunLeftAnimation;
    [SerializeField] private Animation _gunRightAnimation;

    [ProButton]
    private void ReadyFire()
    {
        _cogitatorAnimation.Play(AnimationList.AACogitatorAttack.ToString());
        _crankLeftAnimation.Play(AnimationList.AACrankLeftClose.ToString());
        _crankRightAnimation.Play(AnimationList.AACrankRightClose.ToString());
        _pistonLeftAnimation.Play(AnimationList.AAPistonLeftClose.ToString());
        _pistonRightAnimation.Play(AnimationList.AAPistonRightClose.ToString());
    }

    [ProButton]
    private void ToIdleState()
    {
        _cogitatorAnimation.Play(AnimationList.AACogitatorIdle.ToString());
        _crankLeftAnimation.Play(AnimationList.AACrankLeftOpen.ToString());
        _crankRightAnimation.Play(AnimationList.AACrankRightOpen.ToString());
        _pistonLeftAnimation.Play(AnimationList.AAPistonLeftOpen.ToString());
        _pistonRightAnimation.Play(AnimationList.AAPistonRightOpen.ToString());
    }

    [ProButton]
    private void ShootLeftGun()
    {
        _gunLeftAnimation[AnimationList.AAGunLeftShoot_Lvl2.ToString()].speed = _gunPlaySpeed;
        _gunLeftAnimation.Play(AnimationList.AAGunLeftShoot_Lvl2.ToString());
        _mountAnimation[AnimationList.AAMountVibrationLeft.ToString()].speed = _mountPlaySpeed;
        _mountAnimation.Play(AnimationList.AAMountVibrationLeft.ToString());
    }
    
    [ProButton]
    private void ShootRigtGun()
    {
        _gunRightAnimation[AnimationList.AAGunRightShoot_Lvl2.ToString()].speed = _gunPlaySpeed;
        _gunRightAnimation.Play(AnimationList.AAGunRightShoot_Lvl2.ToString());
        _mountAnimation[AnimationList.AAMountVibrationRight.ToString()].speed = _mountPlaySpeed;
        _mountAnimation.Play(AnimationList.AAMountVibrationRight.ToString());
    }


    private enum AnimationList
    {
        AACogitatorAttack,
        AACogitatorIdle,
        AAMountVibrationLeft,
        AAMountVibrationRight,
        AACrankLeftClose,
        AACrankLeftOpen,
        AACrankRightClose,
        AACrankRightOpen,
        AAGearLeftBckwd,
        AAGearLeftFrwd,
        AAGearRightBckwd,
        AAGearRightFrwd,
        AAPistonLeftClose,
        AAPistonLeftOpen,
        AAPistonRightClose,
        AAPistonRightOpen,
        AAGunLeftShoot_Lvl2,
        AAGunRightShoot_Lvl2,
        
    }
}

