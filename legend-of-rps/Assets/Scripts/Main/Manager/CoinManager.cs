using System;
using System.Collections;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Threading.Tasks;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private int coinIndex = 0;
    [SerializeField] private Animator animator;

    private GameService gameService = GameService.@object;
    private ObjectPooler OP;
    private Sequence sequence;
    private float MinCapForGetCoin = 0.1f;
    private float lastTimeGetCoin;
    private float coindropLeft = Screen.width * -0.1f;
    private float coindropRight = Screen.width * 0.001f;

    [Obsolete]
    private void Start()
    {
        OP = ObjectPooler.SharedInstance;
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        sequence = DOTween.Sequence();
        gameService.targetTransform = targetTransform;
    }

    [Obsolete]
    private void Update()
    {
        float now = Time.realtimeSinceStartup;
        if (now - lastTimeGetCoin < MinCapForGetCoin || gameService.gameState.Value != GameState.GetCoins)
        {
            return;
        }

        lastTimeGetCoin = now;
        GetCoins();
    }

    [Obsolete]
    private void GetCoins()
    {
        Debug.Log("GET_COIN " + DateTime.Now + " / " + gameService.betCoin.Value);
        gameService.insCoin.Value = gameService.betCoin.Value;
        if (gameService.insCoin.Value > 0 && gameService.isGameStart.Value)
        {
            if (gameService.insCoin.Value > 100)
            {
                gameService.insCoin.Value -= 50;
            }

            var value = gameService.insCoin.Value > 20 ? 20 : 1;

            StartCoroutine(MakeCoinPrefab(value));

   
        }
        else
        {
            gameService.InsertingCoin();
        }
    }

    public IEnumerator MakeCoinPrefab(int value)
    {
        for (int i = 0; i < value; i++)
        {
            gameService.GetCoins();
            GameObject @object = OP.GetPooledObject(coinIndex);
            sequence.Append(@object.transform.DOMove(gameService.targetTransform.position, 0.5f)
                .SetEase(Ease.InOutSine)
                .SetDelay(1.5f)
                .OnComplete(() =>
                {
                    animator.Play("Play");
                    @object.SetActive(false);
                }));
            @object.transform.localScale = Vector3.one * 30000;

            @object.transform.localPosition = new Vector3(UnityEngine.Random.Range(coindropLeft, coindropRight), 0.0f, -100.0f);
            @object.transform.localRotation = new Quaternion(UnityEngine.Random.Range(0.0f, 180.0f), UnityEngine.Random.Range(0.0f, 180.0f),
                UnityEngine.Random.Range(0.0f, 180.0f), UnityEngine.Random.Range(0.0f, 180.0f));
      
            @object.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
