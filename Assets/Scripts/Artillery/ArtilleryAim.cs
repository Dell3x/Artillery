using Enemies;
using ScriptableActions;
using UnityEngine;

namespace Artilleries
{
    public sealed class ArtilleryAim : MonoBehaviour
    {
        [SerializeField] private ArtilleryActions _artilleryActions;
        [SerializeField] private Transform _towerHead;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _detectionRange;
        [SerializeField] private float _targetCheckInterval;

        private GameObject _target;

        private void Start()
        {
            InvokeRepeating("FindTarget", 0, _targetCheckInterval);
        }

        private void Update()
        {
            if (_target != null)
            {
                AimAtTarget();
            }
        }

        private void FindTarget()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRange);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.GetComponent<Enemy>())
                {
                    _target = collider.gameObject;
                    _artilleryActions.RaiseTargetDetected(true);
                    _artilleryActions.RaiseSetUpTargetTransform(_target.transform);
                    return;
                }
            }

            _target = null;
            _artilleryActions.RaiseTargetDetected(false);
        }

        private void AimAtTarget()
        {
            Vector3 targetDirection = _target.transform.position - _towerHead.position;
            float targetRotationY = Quaternion.LookRotation(targetDirection).eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0);
            _towerHead.rotation = Quaternion.Slerp(_towerHead.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}