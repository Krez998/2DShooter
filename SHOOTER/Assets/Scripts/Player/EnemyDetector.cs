using System.Collections;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float _detectRadius;
    [SerializeField] private LayerMask _layerMask;
    private Collider2D _enemyCollider;
    private WaitForSeconds _detectDelay = new WaitForSeconds(0.2f);
    private WeaponHolder _weaponHolder;
    private Transform _target;

    private void Awake()
    {
        _weaponHolder = GetComponent<WeaponHolder>();
        StartCoroutine(DetectorRoutine());
    }

    private IEnumerator DetectorRoutine()
    {
        while (true)
        {
            yield return _detectDelay;
            ViewCkeck();
        }
    }

    private void ViewCkeck()
    {
        if (!_weaponHolder.TargetReceived)
        {
            _enemyCollider = Physics2D.OverlapCircle(transform.position, _detectRadius, _layerMask);
            if (_enemyCollider != null)
            {
                _target = _enemyCollider.transform;
                _weaponHolder.EnemySpotted(_target);
            }
        }
    }



}
