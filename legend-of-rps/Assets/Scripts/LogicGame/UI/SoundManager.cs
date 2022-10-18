using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private List<AudioSource> soundVfx;
    private GameService gameService = GameService.@object;

    // Start is called before the first frame update
    void Start()
    {
        AddVfxSound();
        
        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume", 0.5f);
            LoadSound();
        }
        else
        {
            LoadSound();
        }

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.65f);
            LoadMusic();
        }
        else
        {
            LoadMusic();
        }
    }
    
    private void AddVfxSound()
    {
        soundVfx.Add(AudioServices.Instance.menuClick);
        soundVfx.Add(AudioServices.Instance.insertCoinClick);
        for(int i = 0; i < ObjectPooler.SharedInstance.pooledObjects.Count; i++)
            soundVfx.Add(ObjectPooler.SharedInstance.pooledObjects[i].GetComponent<AudioSource>());
    }
    public void ChangeSound()
    {
        for (int i = 0; i < soundVfx.Count; i++)
        {
            //soundVfx.Add(AudioServices.Instance.menuClick);
            soundVfx[i].volume = soundSlider.value;
        }

        SaveSound();
    }

    public void ChangeMusic()
    {
        AudioServices.Instance.backgroundAudioSource.volume = musicSlider.value;

        gameService.musicVolumeVal.Value = musicSlider.value;
        SaveMusic();
        
    }

    private void LoadSound()
    {
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        for (int i = 0; i < soundVfx.Count; i++)
            soundVfx[i].volume = PlayerPrefs.GetFloat("SoundVolume");
    }

    private void SaveSound()
    {
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
    }
    
    private void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        AudioServices.Instance.backgroundAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
       
    }

    private void SaveMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
}
