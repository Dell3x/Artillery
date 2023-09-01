using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptableActions;
using UnityEngine;

namespace Artilleries
{
    public sealed class Artillery : MonoBehaviour
    {
        [SerializeField] private string _cannonballResourcePath;
        [SerializeField] private ArtilleryActions _artilleryActions;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Animator _animator;
        [SerializeField] private int _canonsAtStartCount;
        [SerializeField] private float _launchForce;
        [SerializeField] private float _angle;
        [SerializeField] private float _gravity;
        [SerializeField] private float _despawnTime;
        [SerializeField] private float _delayBetweenShoots;

        private const string _IsShooting = "isShooting";
        private const string _IsReadyToFire = "isReadyToFire";

        private List<GameObject> _cannonballPool = new List<GameObject>();

        private void Awake()
        {
            InitializeCannonballPool();
        }

        private void OnEnable()
        {
            _artilleryActions.OnTargetDetected += SetPrepareForShootingAnimation;
        }

        private void OnDisable()
        {
            _artilleryActions.OnTargetDetected -= SetPrepareForShootingAnimation;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }

        private void InitializeCannonballPool()
        {
            for (int i = 0; i < _canonsAtStartCount; i++)
            {
                GameObject cannonBall = Instantiate(Resources.Load<GameObject>(_cannonballResourcePath));
                cannonBall.SetActive(false);
                _cannonballPool.Add(cannonBall);
            }
        }

        private GameObject GetCannonballFromPool()
        {
            if (_cannonballPool.Count > 0)
            {
                GameObject cannonBall = _cannonballPool[0];
                _cannonballPool.RemoveAt(0);
                cannonBall.SetActive(true);
                return cannonBall;
            }

            return null;
        }

        private void Fire()
        {
            if (!_animator.GetBool(_IsShooting))
            {
                GameObject cannonBallInstance = GetCannonballFromPool();
                SetShootingAnimation();
                cannonBallInstance.transform.position = _firePoint.position;
                Rigidbody canonRigidbody = cannonBallInstance.GetComponent<Rigidbody>();
                canonRigidbody.velocity = CalculateLaunchVelocity();
                ReturnCannonballAfterDelay(cannonBallInstance, _despawnTime);
            }
        }

        private async void SetShootingAnimation()
        {
            _animator.SetBool(_IsShooting, true);
            await Task.Delay(TimeSpan.FromSeconds(_delayBetweenShoots));
            _animator.SetBool(_IsShooting, false);
        }

        private void SetPrepareForShootingAnimation(bool isReady)
        {
            _animator.SetBool(_IsReadyToFire, isReady);
        }
        
        private async void ReturnCannonballAfterDelay(GameObject cannonBall, float delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            ReturnCannonballToPool(cannonBall);
        }

        private void ReturnCannonballToPool(GameObject cannonball)
        {
            if (cannonball != null)
            {
                cannonball.SetActive(false);
                _cannonballPool.Add(cannonball);
            }
        }

        private Vector3 CalculateLaunchVelocity()
        {
            Vector3 launchDirection = Quaternion.Euler(-_angle, 0, 0) * transform.forward;
            Vector3 launchVelocity = launchDirection * _launchForce;
            return launchVelocity;
        }
    }
}