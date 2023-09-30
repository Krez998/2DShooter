using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public string Name => _name;
    public int Count => _count;
    public Item Item => _item;
    [SerializeField] private Text _countText;
    [SerializeField] private Image _image;   
    [SerializeField] private Button _deleteButton;
    private string _name;
    private int _count;
    private Item _item;
    private bool _isDropButtonShowed;

    public void SetData(Item item)
    {
        _name = name;
        _count = 1;
        _image.sprite = item.Icon;
        _item = item;
        UpdateCountInfo();
    }

    public void DropItem()
    {
        if( _count <= 1)
        {
            Destroy(gameObject); 
        }
        else
        {
            _count--;
            UpdateCountInfo();
        }             
    }

    public void IncreaseCount()
    {
        _count++;
        UpdateCountInfo();
    }  

    public void UpdateCountInfo()
    {
        if (_count > 1)
        {
            _countText.gameObject.SetActive(true);
            _countText.text = _count.ToString();
        }
        else
            _countText.gameObject.SetActive(false);
    }

    public void ShowDropButton()
    {
        _isDropButtonShowed = !_isDropButtonShowed;
        _deleteButton.gameObject.SetActive(_isDropButtonShowed);
    }
}
