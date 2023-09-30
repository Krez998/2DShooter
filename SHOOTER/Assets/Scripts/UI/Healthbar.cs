using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _barImage;

    public void UpdateBar(float currHealth, float maxHealth)
    {
        _barImage.fillAmount = currHealth / maxHealth;
    }
}
