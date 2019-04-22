using UnityEngine;
using UnityEngine.UI;

public class MusicSetup : MonoBehaviour
{
    public GameObject music;
    public Slider musicVolumeSlider, soundEffectsSlider;
    static AudioSource audioSource;

    void Awake()
    {
        if (audioSource == null )
        {
            audioSource = Instantiate(music.GetComponent<AudioSource>());
        }
        GameObject.DontDestroyOnLoad(audioSource);
    }

    private void Start()
    {
        audioSource.ignoreListenerVolume = true;
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        AudioListener.volume = PlayerPrefs.GetFloat("SoundEffects");
        musicVolumeSlider.value = audioSource.volume;
        soundEffectsSlider.value = AudioListener.volume;
    }

    public void SetMusicVolume()
    {
        audioSource.volume = musicVolumeSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", audioSource.volume);
    }

    public void SetSoundEffectsVolume()
    {
        AudioListener.volume = soundEffectsSlider.value;
        PlayerPrefs.SetFloat("SoundEffects", soundEffectsSlider.value);
    }
}
