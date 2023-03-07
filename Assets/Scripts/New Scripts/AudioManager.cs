using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;

    public AudioClip[] sfxClips;

    private void Awake()
    {
        //si hay otra instancia, destruir la extra
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void HarvestSound()
    {
        sfxAudioSource.PlayOneShot(sfxClips[0]);
    }

    public void SeedSound()
    {
        sfxAudioSource.PlayOneShot(sfxClips[1]);
    }

    public void WaterSound()
    {
        sfxAudioSource.PlayOneShot(sfxClips[2]);
    }

    public void ShovelSound()
    {
        sfxAudioSource.PlayOneShot(sfxClips[3]);
    }
}
