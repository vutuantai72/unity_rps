using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMission_Item : MonoBehaviour
{
    [SerializeField] private Image logo;
    [SerializeField] private TextMeshProUGUI missionName;
    [SerializeField] private TextMeshProUGUI missionValue;
    [SerializeField] private TextMeshProUGUI missionProgress;
    [SerializeField] private TextMeshProUGUI missionId;
    [SerializeField] private Button claimMissionReward;
    [SerializeField] private Image barMissionProcess;
    [SerializeField] private RectTransform completedMission;

    private GameService gameService = GameService.@object;
    public void SetupMission(Sprite logoMission, string name, string value, string progress, bool missionStatus, float process
        , string missionID
        , bool claimStatus)
    {
        
        logo.sprite = logoMission;
        missionName.text = name;
        missionValue.text = value;
        missionProgress.text = progress;
        claimMissionReward.interactable = missionStatus;
        barMissionProcess.fillAmount = process;
        missionId.text = missionID;
        claimMissionReward.gameObject.SetActive(claimStatus);
        completedMission.gameObject.SetActive(!claimStatus);
    }

    public void MissionReward(Transform missionID)
    {
        GameDataServices.Instance.ClaimMissonReward(missionID.GetComponent<TextMeshProUGUI>().text, gameService.userEmail.Value, callback: (result) =>
        {
            if (result == null)
                return;

            gameService.msgDialogError.Value = "Claim reward success";
            gameService.msgDialogTitleError.Value = "";
            gameService.isShowDialogError.Value = true;

            GameMissions.Instance.GetMissionLists();
        });

        //StartCoroutine(GameMissions.Instance.UpdateMissionLists());
        //GameMissions.Instance.GetMissionLists();
    }
}