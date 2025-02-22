using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VlumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetEffectsVolume();
        }
    }

    public void SetMusicVolume() 
    {
        float volume = _musicSlider.value;
        _audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetEffectsVolume()
    {
        float volume = _effectsSlider.value;
        _audioMixer.SetFloat("effects", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("effectsVolume", volume);
    }

    private void LoadVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume");
        SetEffectsVolume();
        SetMusicVolume();
    }
}
