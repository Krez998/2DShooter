using System.Collections;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _distanceToShot;
    [SerializeField] private float _shotForce;
    [SerializeField] private int _ammo;
    private WaitForSeconds _shotDelay = new WaitForSeconds(0.5f);
    private Coroutine _fireRoutine;
    private Transform _enemyPos;
    public bool TargetReceived => _enemyPos != null;

    private void OnEnable()
    {
        EventBus.EnemyEliminated += StopShooting;
    }

    private void OnDisable()
    {
        EventBus.EnemyEliminated -= StopShooting;
    }

    private void Update()
    {
        Aiming();
    }

    private void StopShooting()
    {
        StopCoroutine(_fireRoutine);
        _enemyPos = null;
    }

    private void Aiming()
    {
        if (TargetReceived)
        {
            Vector2 dir = _enemyPos.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _weapon.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void EnemySpotted(Transform enemyPos)
    {       
        if (!TargetReceived)
        {
            _enemyPos = enemyPos;
            if (Vector2.Distance(transform.position, _enemyPos.position) < _distanceToShot)
            {
                _fireRoutine = StartCoroutine(KillEnemyRoutine());
            }
            else
                _enemyPos = null;
        }
    }

    private IEnumerator KillEnemyRoutine()
    {
        while (true)
        {
            if(TargetReceived && Vector2.Distance(transform.position, _enemyPos.position) > _distanceToShot){
                _enemyPos = null;
                yield break;
            }
            yield return _shotDelay;
            Fire();
        }
    }

    public void Fire()
    {
        if (_ammo > 0)
        {
            Bullet bullet = Instantiate(_bullet, _weapon.position, Quaternion.identity);
            Destroy(bullet, 3f);
            bullet.transform.rotation = _weapon.rotation;
            bullet.transform.Rotate(0, 0, -90f);
            bullet.GetComponent<Rigidbody2D>().velocity = _weapon.transform.right * _shotForce;
            _ammo--;
        }
    }
}
