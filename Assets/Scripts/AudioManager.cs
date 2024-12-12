using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;
    [SerializeField] public AudioSource SFXBackground;
    [SerializeField] public AudioSource SFXEnemy;

    [Header("------SFX Source------")]
    public AudioClip background;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;
    public AudioClip dash;
    public AudioClip checkpoint;

    [Header("------SFX Character------")]
    public AudioClip characterHit;

    [Header("------SFX Enemy------")]
    public AudioClip bat;
    public AudioClip batDead;
    public AudioClip enemyHit;
    public AudioClip phobosHit;
    public AudioClip slime;
    public AudioClip slimeDead;
    public AudioClip golemDead;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

}
