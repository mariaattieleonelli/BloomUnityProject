using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;

    public AudioClip[] sfxClips;
    public AudioClip[] musicClips;

    [SerializeField] public Slider sfxSlider;
    [SerializeField] public Slider musicSlider;

    public AudioMixer audioMixer;

    //Parámetros expuestos del audio mixer
    const string mixerMusic = "musicVolume";
    const string mixerSfx = "sfxVolume";

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

        //Cuando cambia el valor de los sliders de sonido
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    private void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(mixerMusic, Mathf.Log10(value) * 20); //Se hace la multiplicación para llegar a menos 80 decibeles
    }

    private void SetSfxVolume(float value)
    {
        audioMixer.SetFloat(mixerSfx, Mathf.Log10(value) * 20); //Se hace la multiplicación para llegar a menos 80 decibeles
    }

    #region Sound Effects
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
    #endregion


}
