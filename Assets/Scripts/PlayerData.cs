using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour, ITakeHit
{
    [SerializeField] Projectile _projectilePrefab;
    UIAmmoBar _ammoBar;
    UIHealthBar _healthBar;
    [SerializeField] int _health;
    [SerializeField] int _ammo;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip damageSFX;
    [SerializeField] AudioClip healSFX;
    [SerializeField] AudioClip funnySFX;

    int _maxAmmo = 15;
    float _coolDown;
    int _maxHealth = 15;
    float _timeSinceLastAttack;
    bool _alive;
    bool isFunny = true;
    AudioSource audioSource;
    public int Ammo { get { return _ammo; } }
    public int MaxAmmo { get { return _maxAmmo; } }
    public int Health { get { return _health; } }
    public int MaxHealth { get { return _maxHealth; } }
    public UIAmmoBar AmmoBar { get { return _ammoBar; } }
    public bool IsAlive { get { return _alive; } }

    void Start()
    {
        //Physics2D.IgnoreLayerCollision(6, 8);
        _maxHealth = 15;
        _maxAmmo = 15;
        _coolDown = 1;
        _health = _maxHealth;
        _ammo = _maxAmmo;
        _alive = true;
        audioSource = GetComponent<AudioSource>();
        _ammoBar = GameObject.Find("PlayerUI").transform.Find("AmmoBar").transform.Find("AmmoTicks").GetComponent<UIAmmoBar>();
        _healthBar = GameObject.Find("PlayerUI").transform.Find("HealthBar").transform.Find("HealthTicks").GetComponent<UIHealthBar>();
    }

    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;

        if (ShouldFire())
            Fire();

        _healthBar.UpdateHealthBar(_health, _maxHealth);

        if(_health > _maxHealth)
        {
            _health = _maxHealth;
        }

        if(transform.position.y < -20 && IsAlive)
        {
            Die();
        }
    }

    bool ShouldFire()
    {
        return Input.GetButton("Fire1") && _timeSinceLastAttack >= _coolDown && _ammo > 0 && _alive;
    }

    void Fire()
    {
        Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        _ammo--;
        _ammoBar.UpdateAmmoBar(_ammo, _maxAmmo);
        _timeSinceLastAttack = 0;
        audioSource.PlayOneShot(shootSFX);
        GetComponent<PlayerController>().AnimateShoot();
    }

    public void TakeHit(IDealDamage attacker)
    {
        if (IsAlive)
        {
            _health -= attacker.Damage;
            _healthBar.UpdateHealthBar(_health, _maxHealth);
            if (_health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(Flash(Color.red));
                audioSource.PlayOneShot(damageSFX);
            }
        }
    }

    internal void Heal(int healthToHeal)
    {
        _health += healthToHeal;
        _healthBar.UpdateHealthBar(_health, _maxHealth);
        StartCoroutine(Flash(Color.green));
        audioSource.PlayOneShot(healSFX);
    }

    public void Reload()
    {
        _ammo = _maxAmmo;
        StartCoroutine(Flash(Color.yellow));
        audioSource.PlayOneShot(healSFX);
    }

    void Die()
    {
        _alive = false;
        GetComponent<PlayerController>().Die();
        SceneManager.LoadScene("Death");
    }

    IEnumerator Flash(Color color)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 5; i++)
        {
            sr.color = color;
            yield return new WaitForSeconds(0.05f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Amogus" && isFunny)
        {
            isFunny = false;
            audioSource.PlayOneShot(funnySFX);
        }
    }
}
