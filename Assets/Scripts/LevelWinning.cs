using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class LevelWinning : MonoBehaviour
{
    [SerializeField] int _nextLevel;

    AudioSource _audioSource;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
            StartCoroutine(WinLevel());
    }

    IEnumerator WinLevel()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_nextLevel);
    }
}
