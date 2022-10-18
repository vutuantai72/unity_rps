using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UI.FortuneWheel;
using UnityEngine.UI;
using TMPro;

namespace UI.FortuneWheel
{
    [System.Serializable]
    public class WheelPiece
    {
        public Transform value;

        [HideInInspector] public int Index;
        [HideInInspector] public double _weight = 0f;
    }

}

public class FortuneWheel : MonoBehaviour
{
    [SerializeField] private Transform spinBoard;
    [SerializeField] private Transform spinKey;
    [SerializeField] private Button spinButton;
    [SerializeField] private Button closeButton;
    
    [Header("Wheel Pieces")]
    [SerializeField] private WheelPiece[] wheelPieces;
    [SerializeField] private Transform wheelPiecesParent;
    [SerializeField] private AudioSource spinSound;

    private GameService gameService = GameService.@object;

    [SerializeField] [Range(.2f, 2f)] private float wheelSize = 1f;
    [Range(1, 20)] public int spinDuration = 12;

    private List<int> nonZeroChancesIndices = new List<int>();
    private int[] wheelIndex = new int[8];

    private float pieceAngle;
    private float halfPieceAngle;

    private System.Random rand = new System.Random();

    private bool _isSpinning = false;
    public bool IsSpinning { get { return _isSpinning; } }

    //Events
    private UnityAction onSpinStartEvent;
    private UnityAction<WheelPiece> onSpinEndEvent;

    private void Start()
    {
        WheelPiece piece = wheelPieces[0];
        GameDataServices.Instance.SpinTurn();

        pieceAngle = 360 / wheelPieces.Length;
        halfPieceAngle = pieceAngle / 2f;
    }

    public void Spin()
    {
        gameService.isSpinButtonActive.Value = true;
        spinButton.interactable = false;
        closeButton.interactable = false;
        GameDataServices.Instance.SpinLuckyWheel((data) =>
        {
            if (data.result == null)
            {
                gameService.msgDialogTitleError.Value = "OOOPS!!";
                gameService.isShowDialogError.Value = true;
                gameService.msgDialogError.Value = $"You not enough spin turn!";
                gameService.isSpinButtonActive.Value = false;
                spinButton.interactable = true;
                closeButton.interactable = true;
                return;
            }

            gameService.spinTurn.Value -= 1;
            if (!_isSpinning)
            {
                _isSpinning = true;
                if (onSpinStartEvent != null)
                    onSpinStartEvent.Invoke();

                float randomAngle = Random.Range(0f, 360f);

                Debug.LogError("data.result.wheel_index: " + data.result.wheel_index);

                var startAngle = spinBoard.transform.eulerAngles.z;

                var endAngle = wheelPieces[data.result.wheel_index - 1].value.eulerAngles.z;

                Vector3 targetRotation = Vector3.back * (endAngle - startAngle + 1 * 360 * 7);

                float prevAngle, currentAngle;
                prevAngle = currentAngle = spinBoard.eulerAngles.z;

                spinSound.Play();
                spinBoard.DORotate(targetRotation, spinDuration, RotateMode.Fast)
                    .SetEase(Ease.OutQuart)
                    .OnUpdate(() =>
                    {
                        float diff = Mathf.Abs(prevAngle - currentAngle);
                        if (diff >= halfPieceAngle)
                        {
                            prevAngle = currentAngle;
                        }
                        currentAngle = spinBoard.eulerAngles.z;
                    })
                    .OnComplete(() =>
                    {
                        _isSpinning = false;

                        onSpinStartEvent = null;
                        onSpinEndEvent = null;

                        if (data.result.wheel_index == 5)
                        {
                            gameService.msgDialogTitleError.Value = "OOOPS!!";
                            gameService.isShowDialogError.Value = true;
                            gameService.msgDialogError.Value = $"Better lucky next time";
                        }
                        else
                        {
                            gameService.msgDialogTitleError.Value = "Congratulation";
                            gameService.isShowDialogError.Value = true;
                            gameService.msgDialogError.Value = $"You get {data.result.name}";
                        }
                        gameService.userCoin.Value = data.user.coin;
                        gameService.spinTurn.Value = data.currentTurn;
                        spinButton.interactable = true;
                        closeButton.interactable = true;
                        gameService.isSpinButtonActive.Value = false;
                        spinSound.Stop();
                    });
            }
        });
    }

    private void OnValidate()
    {
        if (spinBoard != null)
            spinBoard.localScale = new Vector3(wheelSize, wheelSize, 1f);
    }

    public void OnSpinStart(UnityAction action)
    {
        onSpinStartEvent = action;
    }

    public void OnSpinEnd(UnityAction<WheelPiece> action)
    {
        onSpinEndEvent = action;
    }

    void ResetValue()
    {
        wheelIndex[0] = 100;
        wheelIndex[1] = 200;
        wheelIndex[2] = 300;
        wheelIndex[3] = 400;
        wheelIndex[4] = 500;
        wheelIndex[5] = 600;
        wheelIndex[6] = 700;
        wheelIndex[7] = 800;
    }
}
