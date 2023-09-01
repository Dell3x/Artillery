using System;
using System.Collections;
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
        [SerializeField] private float _delayBetweenShoots;
        [SerializeField] private float _prepareForShootDelay;

        [Header("Trajectory Settings"), Space(5f)] 
        [SerializeField] private bool _isTrajectoryEnabled;
        [SerializeField] private float _launchHeight;
        [SerializeField] private int _numPoints;
        
        [Header("CanonBall LaunchSettings"),Space(5f)]
        [SerializeField] private float maxDistance;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxLaunchForce;
        [SerializeField] private float minLaunchForce;

        private const string _IsShooting = "isShooting";
        private const string _IsReadyToFire = "isReadyToFire";

        private List<CanonBall> _cannonballPool = new List<CanonBall>();
        private bool _isReadyToFire;
        private Transform _enemyTransform;

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

                Vector3[] points = CalculateBezierPoints(_firePoint.position, _enemyTransform.position, _launchHeight,
                    _numPoints);
                
                if (_isTrajectoryEnabled)
                {
                    cannonBallInstance.CanonBallLineRenderer.positionCount = points.Length;
                    cannonBallInstance.CanonBallLineRenderer.SetPositions(points);
                }

                float distanceToTarget = Vector3.Distance(_firePoint.position, _enemyTransform.position);
                float launchForce = Mathf.Lerp(minLaunchForce, maxLaunchForce,
                    (distanceToTarget - minDistance) / (maxDistance - minDistance));

                StartCoroutine(MoveCannonballThroughTrajectory(cannonBallInstance.CanonBallRigidbody, launchForce, points));
            }
        }

        private Vector3[] CalculateBezierPoints(Vector3 start, Vector3 end, float height, int numPoints)
        {
            Vector3[] points = new Vector3[numPoints];

            for (int i = 0; i < numPoints; i++)
            {
                float t = i / (float)(numPoints - 1);
                float u = 1 - t;
                float tt = t * t;
                float uu = u * u;
                float uuu = uu * u;
                float ttt = tt * t;

                Vector3 p = uuu * start;
                p += 3 * uu * t * (start + Vector3.up * height);
                p += 3 * u * tt * (end + Vector3.up * height);
                p += ttt * end;

                points[i] = p;
            }

            return points;
        }

        private IEnumerator MoveCannonballThroughTrajectory(Rigidbody cannonRigidbody, float launchForce,
            Vector3[] points)
        {
            for (int i = 1; i < points.Length; i++)
            {
                Vector3 startPoint = points[i - 1];
                Vector3 endPoint = points[i];
                float distance = Vector3.Distance(startPoint, endPoint);
                float flightDuration = distance / launchForce;

                float journeyStartTime = Time.time;

                while (Time.time - journeyStartTime < flightDuration)
                {
                    float distanceCovered = (Time.time - journeyStartTime) * launchForce;
                    float fractionOfJourney = distanceCovered / distance;
                    Vector3 newPosition = Vector3.Lerp(startPoint, endPoint, fractionOfJourney);
                    cannonRigidbody.MovePosition(newPosition);

                    yield return null;
                }
                cannonRigidbody.MovePosition(endPoint);

                yield return null;
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