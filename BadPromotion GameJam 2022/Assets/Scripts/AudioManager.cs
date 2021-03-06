using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource source;

    [SerializeField] private float lowPitch = 0.95f;
    [SerializeField] private float highPitch = 1.05f;
    [SerializeField] private bool loop = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.loop = loop;
    }

    public void PlayClip(AudioClip clip, bool loop)
    {
        source.loop = loop;
        source.clip = clip;
        source.Play();
    }

    public void PlayRandomClip(bool loop, params AudioClip[] clips)
    {
        source.loop = loop;
        source.pitch = Random.Range(lowPitch, highPitch);
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
