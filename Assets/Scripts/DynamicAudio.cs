using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAudio : MonoBehaviour
{
   public IEnumerator play(AudioClip sfx, float volume, float pitch) 
    {
        AudioSource audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = sfx;
        audioSource.loop = false;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        yield return new WaitForSeconds(sfx.length);
        Destroy(audioSource);
    }
}
