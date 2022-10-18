using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RouletteNumberActive : RxActive
{
    [SerializeField] private Animator anim;
    GameService gameService = GameService.@object;
    public int index;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService
            .gameState
            .Where(g => g == GameState.CoinRoulette)
            .SelectMany(gameService.roulettes.Select(i => i == index));

        anim.Play("GlowAnim");
    }
}
