using UnityEngine;

public class MainMenuAudioManager : MonoBehaviour
{
    public static MainMenuAudioManager instance { get; private set; }

    public AudioSource sfxAudioSource;

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
    public void ButtonClick()
    {
        sfxAudioSource.PlayOneShot(sfxClips[0]);
    }
}
