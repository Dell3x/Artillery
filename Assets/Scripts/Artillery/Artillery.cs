using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptableActions;
using UnityEditor;
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
        [SerializeField] private float _delayBetweenShoots;
        [SerializeField] private float _prepareForShootDelay;

        private const string _IsShooting = "isShooting";
        private const string _IsReadyToFire = "isReadyToFire";

        private List<CanonBall> _cannonballPool = new List<CanonBall>();
        private bool _isReadyToFire;
        private Transform _enemyTransform;
        private float _gravity = -9.81f;
        private float _launchAngle = 45f;

        private void Awake()
        {
            InitializeCannonballPool();
        }

        private void OnEnable()
        {
            _artilleryActions.TargetDetected += OnSetPrepareForShootingAnimation;
            _artilleryActions.SetUpTargetTransform += OnSetUpEnemyTransform;
            _artilleryActions.HitDetected += OnCanonBallHitDetected;
        }

        private void OnDisable()
        {
            _artilleryActions.TargetDetected -= OnSetPrepareForShootingAnimation;
            _artilleryActions.SetUpTargetTransform -= OnSetUpEnemyTransform;
            _artilleryActions.HitDetected -= OnCanonBallHitDetected;
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
                CanonBall cannonBall = Instantiate(Resources.Load<CanonBall>(_cannonballResourcePath));
                cannonBall.gameObject.SetActive(false);
                _cannonballPool.Add(cannonBall);
            }
        }

        private CanonBall GetCannonballFromPool()
        {
            if (_cannonballPool.Count > 0)
            {
                CanonBall cannonBall = _cannonballPool[0];
                _cannonballPool.RemoveAt(0);
                cannonBall.gameObject.SetActive(true);
                cannonBall.transform.position = _firePoint.position;
                return cannonBall;
            }

            return null;
        }

        private void Fire()
        {
            if (!_animator.GetBool(_IsShooting) && _isReadyToFire)
            {
                CanonBall cannonBallInstance = GetCannonballFromPool();
                SetShootingAnimation();
                Vector3 distanceToTarget = _enemyTransform.position - _firePoint.position;
                Vector3 fromToXZ = new Vector3(distanceToTarget.x, 0f, distanceToTarget.z);
                float distanceXZ = fromToXZ.magnitude;
                float heightDifference = distanceToTarget.y;
                float initialVelocitySquared = (_gravity * distanceXZ * distanceXZ) / (2f * (heightDifference - Mathf.Tan(_launchAngle) * distanceXZ) *
                                                                                       Mathf.Pow(Mathf.Cos(_launchAngle), 2));
                float initialVelocity = Mathf.Sqrt(Mathf.Abs(initialVelocitySquared));
                cannonBallInstance.CanonBallRigidbody.velocity = _firePoint.forward * initialVelocity;
            }
        }

        private void OnSetUpEnemyTransform(Transform transform)
        {
            _enemyTransform = transform;
        }

        private async void SetShootingAnimation()
        {
            _animator.SetBool(_IsShooting, true);
            await Task.Delay(TimeSpan.FromSeconds(_delayBetweenShoots));
            _animator.SetBool(_IsShooting, false);
        }

        private async void OnSetPrepareForShootingAnimation(bool isReady)
        {
            _animator.SetBool(_IsReadyToFire, isReady);
            await Task.Delay(TimeSpan.FromSeconds(_prepareForShootDelay));
            _isReadyToFire = isReady;
        }

        private void OnCanonBallHitDetected(CanonBall cannonBall)
        {
            ReturnCannonballToPool(cannonBall);
        }

        private void ReturnCannonballToPool(CanonBall cannonball)
        {
            if (cannonball != null)
            {
                cannonball.transform.position = Vector3.zero;
                cannonball.CanonBallRigidbody.velocity = Vector3.zero;
                cannonball.gameObject.SetActive(false);
                _cannonballPool.Add(cannonball);
            }
        }
    }
}