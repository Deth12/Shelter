using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    [SerializeField] private AudioSource src = null;

    [SerializeField] private AudioClip buttonClickSound = null;
    
    public void PlayOneShot(AudioClip clip, float volume = 1f)
    {
        src.PlayOneShot(clip, volume);
    }

    public void ButtonClickSound(float volume = 1f)
    {
        PlayOneShot(buttonClickSound, volume);
    }
    
    public void MuteAudio(bool state)
    {
        src.mute = !state;
    }
}
