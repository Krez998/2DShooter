using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void Start()
    {
        PrepareGameData();
        SpawnPlayer();
        SpawnEnemies();
    }

    private void PrepareGameData()
    {
        _gameData.Load();
    }

    private void SpawnPlayer()
    {
        Instantiate(_player,new Vector2(0,0), Quaternion.identity);
    }

    private void SpawnEnemies()
    {
        _enemySpawner.SpawnEnemies();
    }
}
