using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float _detectRadius;
    [SerializeField] private LayerMask _layerMask;
    private Collider2D _enemyCollider;
    private WaitForSeconds checkDelay = new WaitForSeconds(0.2f);
    private Enemy _enemy;
    private Transform _target;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        StartCoroutine(DetectorRoutine());
    }

    private IEnumerator DetectorRoutine()
    {
        while (true)
        {
            yield return checkDelay;
            ViewCkeck();
        }
    }

    private void ViewCkeck()
    {
        _enemyCollider = Physics2D.OverlapCircle(transform.position, _detectRadius, _layerMask);
        if (_enemyCollider != null)
        {
            _target = _enemyCollider.transform;
            _enemy.SetTarget(_target);
        }
        else
            _enemy.SetTarget(null);
    }
}
