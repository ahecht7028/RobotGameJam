using UnityEngine;
using UnityEngine.UI;

public class UIAmmoBar : MonoBehaviour
{
    Image _image;
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateAmmoBar(int ammo, int manAmmo)
    {
        _image.fillAmount = (float)ammo / manAmmo;
    }
}