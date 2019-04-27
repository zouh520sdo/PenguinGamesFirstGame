using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

    private static BGM _instance;
    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();

        if (_instance == null)
        {
            PlayMusic();
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

}
