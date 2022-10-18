using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class EnemySoundActive : RxSound
{
    private AudioSource audioSource;
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.gameState
                            .Select(g => g == GameState.Selecting);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
    }
}
