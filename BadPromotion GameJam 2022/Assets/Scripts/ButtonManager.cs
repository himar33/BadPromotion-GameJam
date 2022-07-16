using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonManager : MonoBehaviour
{
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip pressSound;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayHover()
    {
        audio.PlayOneShot(hoverSound);
    }
    
    public void PlayPress()
    {
        audio.PlayOneShot(pressSound);
    }
}
