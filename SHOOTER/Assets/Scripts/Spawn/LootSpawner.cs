using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private List<Item> _items = new List<Item>();
    [SerializeField] private Pickup _pickup;

    public void SpawnLoot(Transform transform)
    {
        Pickup loot = Instantiate(_pickup, transform.position, Quaternion.identity);
        loot.SetItem(_items[Random.Range(0, _items.Count)]);
    }
}
