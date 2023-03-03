using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneForAllSoundEffects : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]AudioClip lowTensionBackgroundMusic;
    [SerializeField]AudioClip highTensionBackgroundMusic;
    [SerializeField]List<AudioClip> oneForAllSounds;
    [SerializeField]List<AudioClip> enemySounds;

    private void OnEnable() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(1.5f);
    }
    
    public void ChangeBackgroundSound()
    {
        if(audioSource != null && audioSource.clip.name == lowTensionBackgroundMusic.name)
        {
            AudioClip background = audioSource.clip = highTensionBackgroundMusic;
            audioSource.clip = background;
            audioSource.Play();
        }
    }
    public void PlayAnimSound(int index)
    {
        audioSource.PlayOneShot(oneForAllSounds[index]);
    }
    public void PlayEnemySound(int index)
    {
        audioSource.PlayOneShot(enemySounds[index]);
    }
}
