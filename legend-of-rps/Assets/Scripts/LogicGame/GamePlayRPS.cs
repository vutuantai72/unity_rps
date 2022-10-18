using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayRPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI insertCoin;
    [SerializeField] private TextMeshProUGUI coinPlay;
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private Image aiChoices;
    [SerializeField] private Sprite[] aiSelected;
    [SerializeField] private string[] choices;

    private int InsertCoin, RewardCoin, GameCoin;
    private bool playButtonClicked;

    private void Update()
    {
        if (InsertCoin >= 1)
        {
            if (!playButtonClicked)
            {
                //InvokeRepeating("ChangeSpriteOvertime", 0.1f, 0.1f);
                StartCoroutine(ChangeSpriteOvertime());
            }
        }
    }

    public void Play(string playerChoice)
    {
        playButtonClicked = true;
        //aiChoices.sprite = null;

        string AIChoice = choices[Random.Range(0, choices.Length)];
        InsertCoin -= 1;
        insertCoin.text = InsertCoin.ToString();

        if (InsertCoin >= 0)
        {
            switch (AIChoice)
            {
                case "Rock":
                    switch (playerChoice)
                    {
                        case "Rock":
                            result.text = "Draw";
                            break;
                        case "Paper":
                            result.text = "Win";
                            RewardCoin = Random.Range(1, 7);
                            break;
                        case "Scissor":
                            result.text = "Lose";
                            break;
                    }
                    aiChoices.sprite = aiSelected[0];
                    break;

                case "Paper":
                    switch (playerChoice)
                    {
                        case "Rock":
                            result.text = "Lose";
                            break;
                        case "Paper":
                            result.text = "Draw";
                            break;
                        case "Scissor":
                            result.text = "Win";
                            RewardCoin = Random.Range(1, 7);
                            break;
                    }
                    aiChoices.sprite = aiSelected[1];
                    break;

                case "Scissor":
                    switch (playerChoice)
                    {
                        case "Rock":
                            result.text = "Win";
                            RewardCoin = Random.Range(1, 7);
                            break;
                        case "Paper":
                            result.text = "Lose";
                            break;
                        case "Scissor":
                            result.text = "Draw";
                            break;
                    }
                    aiChoices.sprite = aiSelected[2];
                    break;
            }
            reward.text = RewardCoin.ToString();
            GameCoin += RewardCoin;
            coinPlay.text = GameCoin.ToString();
            RewardCoin = 0;
        }
        else
            Debug.Log("YOU DONT HAVE ANY COIN, PLEASE INSERT MORE TO PLAY!!!");
    }

    public void StartGame()
    {
        InsertCoin += 1;
        insertCoin.text = InsertCoin.ToString();
        playButtonClicked = false;
    }

    IEnumerator ChangeSpriteOvertime()
    {
        yield return new WaitForSeconds(0.3f);

        if (!playButtonClicked)
        {
            var val = Random.Range(0, 3);
            if (val == 0)
            {
                aiChoices.sprite = aiSelected[0];
            }
            else if (val == 1)
            {
                aiChoices.sprite = aiSelected[1];
            }
            else
                aiChoices.sprite = aiSelected[2];
        }   
    }
}
