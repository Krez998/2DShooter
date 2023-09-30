using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private bool _isOpen;
    [SerializeField] private Cell _cell;
    [SerializeField] private Transform _container;
    [SerializeField] private List<Cell> _inventory;
    [SerializeField] private int _collectCount;
    private GameData _gameData;

    private void Awake() => _gameData = FindObjectOfType<GameData>();

    private void Start()
    {
        _container.gameObject.SetActive(false);
        LoadCountData();
    }

    public void CheckInventory()
    {
        _isOpen = !_isOpen;
        _container.gameObject.SetActive(_isOpen);
    }

    public void AddItem(Item newItem)
    {
        bool isSimilarItemExist = false;
        foreach (var cell in _inventory)
        {
            if (cell.Item == newItem)
            {
                isSimilarItemExist = true;
                cell.IncreaseCount();
                break;
            }
        }

        if (!isSimilarItemExist)
        {
            Cell newCell = Instantiate(_cell, _container);
            newCell.SetData(newItem);
            _inventory.Add(newCell);
        }

        _collectCount++;
    }

    private void LoadCountData()
    {
        _collectCount = _gameData.GetData();
    }

    public void SaveCountData()
    {
        _gameData.SetData(_collectCount);
    }
}
