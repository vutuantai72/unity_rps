using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacpostManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform makeTransform;
    GameService gameService = GameService.@object;

    public void OnGetJacpos()
    {
        //StartCoroutine(MakeCoinPrefab());
    }

    public IEnumerator MakeCoinPrefab()
    {
        for (int i = 0; i < GameService.@object.jackpotCoin.Value; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject @object = Instantiate(coinPrefab, makeTransform);
            @object.transform.localPosition = new Vector3(Random.Range(-70.0f, 290.0f), 355.0f, -50.0f);
            @object.transform.localRotation = new Quaternion(Random.Range(45.0f, 135.0f), Random.Range(0.0f, 180.0f),
                Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f));
        }
    }
}
