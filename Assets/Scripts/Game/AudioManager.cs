using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;

    public AudioClip[] sfxClips;
    public AudioClip[] musicClips;

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

        //Reproduce la música de fondo del juego inicial
        musicAudioSource.PlayOneShot(musicClips[0]);
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

    public void ButtonClick()
    {
        sfxAudioSource.PlayOneShot(sfxClips[4]);
    }
    public void EatingSound()
    {
        sfxAudioSource.PlayOneShot(sfxClips[6]);
    }
}
