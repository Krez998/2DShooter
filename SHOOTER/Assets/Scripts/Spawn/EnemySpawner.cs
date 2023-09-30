using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _posXamplitude;
    [SerializeField] private float _posYamplitude;

    public void SpawnEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(_enemy, new Vector3(Random.Range(-_posXamplitude, _posXamplitude), Random.Range(-_posYamplitude, _posYamplitude), 0), Quaternion.identity);
        }
    }
}
