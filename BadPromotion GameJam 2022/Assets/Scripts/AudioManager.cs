using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Random Pitch")]
    [SerializeField] private float lowPitch = .95f;
    [SerializeField] private float highPitch = 1.05f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void StopClip()
    {
        audioSource.Stop();
    }

    public void PlayRandomClip(params AudioClip[] clips)
    {
        audioSource.pitch = Random.Range(lowPitch, highPitch);
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
