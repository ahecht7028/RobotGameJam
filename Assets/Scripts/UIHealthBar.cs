using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    Image _image;
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateHealthBar(int health, int maxHealth)
    {
        _image.fillAmount = (float)health / maxHealth;
    }
}
