using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioServices : UnityActiveSingleton<AudioServices>
{
    [SerializeField] private AudioSource[] bgAudioSources;
    [SerializeField] private AudioSource[] countdownAudio;

    [SerializeField] private AudioSource playerWinAudioSource;
    [SerializeField] private AudioSource playerDieAudioSource;

    [SerializeField] private AudioSource gameOverAudioSource;

    [SerializeField] private AudioSource songConfirmClick;
    [SerializeField] private AudioSource menuClick2;
    [SerializeField] private AudioSource startGameClick;
    [SerializeField] private AudioSource introSound;

    public AudioSource insertCoinClick;
    public AudioSource menuClick;
    public AudioSource backgroundAudioSource;
    private GameService gameService = GameService.@object;

    //for MultiAudio
    private Tweener tweenAudioStop;
    private bool isInTweenTime;
    private bool isPlayingBackgroundMusic;
    private bool isFirstTimeBGAudio = true;

    public bool isAudioFinished;
    public float bgVolume;
    private void Awake() => LoadSoundSetting();

    private void LoadSoundSetting()
    {
        gameService.musicVolumeVal.Value = PlayerPrefs.GetFloat("MusicVolume");
        introSound.volume = gameService.musicVolumeVal.Value;
        menuClick.volume = gameService.musicVolumeVal.Value;
        
    }
    private void Update()
    {
        if (backgroundAudioSource != null && !backgroundAudioSource.isPlaying)
        {
            ChangeBgAudio();
        }
    }

    public AudioSource GetMainBackgroundAudioSource()
    {
        return backgroundAudioSource;
    }

    private void OnOpenAdsStatus(Message message)
    {
        if (!GameService.@object.isTurnOnMusic.Value)
        {
            return;
        }

        if (!isPlayingBackgroundMusic)
            return;

        bool isAdsOpen = (bool)message.Data;

        tweenAudioStop?.Kill();
        if (isAdsOpen)
        {
            backgroundAudioSource.Pause();
        }
        else
        {
            backgroundAudioSource.UnPause();
            OnPlayBackgroundMusic(isPlayingBackgroundMusic);
        }
    }

    private void OnResetForNewGame(Message message)
    {
        backgroundAudioSource.Stop();
    }

    public void OnPlayBackgroundMusic(Message message)
    {
        OnPlayBackgroundMusic((bool)message.Data);
    }

    public void ChangeBgAudio()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            foreach (var audioSource in bgAudioSources)
            {
                audioSource.Stop();
            }

            int randIOS = Random.Range(1, bgAudioSources.Length);
            backgroundAudioSource = bgAudioSources[randIOS];
            return;
        }

        if (isFirstTimeBGAudio)
        {
            isFirstTimeBGAudio = false;
            return;
        }

        foreach (var audioSource in bgAudioSources)
        {
            audioSource.Stop();
        }
        int rand = Random.Range(0, bgAudioSources.Length);
        backgroundAudioSource = bgAudioSources[rand];
        backgroundAudioSource.Play();
        backgroundAudioSource.volume = gameService.musicVolumeVal.Value;
    }

    public void OnPlayBackgroundMusic(bool isPlay)
    {
        isPlayingBackgroundMusic = isPlay;
        foreach (var audioSource in bgAudioSources)
        {
            audioSource.Stop();
        }
        int rand = Random.Range(0, bgAudioSources.Length);
        backgroundAudioSource = bgAudioSources[rand];

        if (isPlay)
        {
            if (!GameService.@object.isTurnOnMusic.Value)
            {
                if (isInTweenTime)
                {
                    isInTweenTime = false;
                    tweenAudioStop?.Kill();
                    backgroundAudioSource.volume = gameService.musicVolumeVal.Value;
                    backgroundAudioSource.Play();
                }
                else
                {
                    if (!backgroundAudioSource.isPlaying)
                    {
                        backgroundAudioSource.volume = gameService.musicVolumeVal.Value;
                        backgroundAudioSource.Play();
                    }
                }
            }
        }
        else
        {
            if (backgroundAudioSource.isPlaying || isInTweenTime)
            {
                tweenAudioStop?.Kill();
                float audioVolume = 1;
                isInTweenTime = true;
                tweenAudioStop = DOTween.To(
                    () => audioVolume,
                    x => audioVolume = x, 0,
                    1);
                tweenAudioStop.OnUpdate(() =>
                {
                    backgroundAudioSource.volume = audioVolume;
                });
                tweenAudioStop.OnComplete(() =>
                {
                    //backgroundAudioSource.volume = 0;
                    //backgroundAudioSource.Stop();
                    isInTweenTime = false;

                    Debug.LogError("STOPPING " + backgroundAudioSource.isPlaying);
                });

            }

        }
    }

    public void PlayerWinAudio()
    {
        if (!GameService.@object.isTurnOnSound.Value)
        {
            return;
        }
        playerWinAudioSource.Play();
    }

    public void PlayGameOverSong(bool isPlay)
    {
        if (GameService.@object.isTurnOnMusic.Value)
        {
            if (isPlay)
            {
                gameOverAudioSource.Play();
            }
            else
            {
                gameOverAudioSource.Stop();
            }

        }
    }

    public void PlayerDieAudio()
    {
        if (!GameService.@object.isTurnOnSound.Value)
        {
            return;
        }
        playerDieAudioSource.Play();
    }

    public void PlayCountDown(int index)
    {
        if (!GameService.@object.isTurnOnSound.Value)
        {
            return;
        }

        if (index >= 0 && index < countdownAudio.Length)
        {
            countdownAudio[index].Play();
        }
    }

    public void PlayClickAudio()
    {
        //if (!GameService.@object.isTurnOnSound.Value)
        //{
        //    return;
        //}
        menuClick.Play();
    }


    public void PlayClickAudio2()
    {
        if (!GameService.@object.isTurnOnSound.Value)
        {
            return;
        }
        menuClick2.Play();
    }

    public void PlaySelectedSongAudio()
    {
        if (!GameService.@object.isTurnOnSound.Value)
        {
            return;
        }
        songConfirmClick.Play();
    }

    public void PlayAudioStartGame()
    {
        startGameClick.Play();
    }

    public void PlayInsertCoin()
    {
        insertCoinClick.Play();
    }
}
