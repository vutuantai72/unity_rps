using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class NFTInWallet : MonoBehaviour
{
    public VideoPlayer NFTVideoPlayer;
    public Button claimNFTRewardButton;
    public TextMeshProUGUI claimNFTRewardButtonText;

    private MyWalletUI myWalletUI;
    public string nftIDToClaim;

    [SerializeField] GameObject NFTVideo;
    [SerializeField] NFTCard NFTImage;

    GameService gameService = GameService.@object;

    private void Awake()
    {
        myWalletUI = GameObject.FindObjectOfType<MyWalletUI>();
        
    }

    public async void ClaimMoneyFromNFT()
    {
        gameService.isLoading.Value = true;

        ClaimNFTReward(nftIDToClaim);
        await myWalletUI.GetMyNFTList();
        await GameDataServices.Instance.GetNotifications();

        gameService.isLoading.Value = false;
        gameService.isNFTShowcaseOpen.Value = false;
    }

    private void ClaimNFTReward(string nftID)
    {
        GameDataServices.Instance.ClaimNFTAirdropReward(nftID, result => {
            if (result == null)
            {
                return;
            }

            if (result.code == 0)//send reward through noti
            {
                //gameService.isNFTShowcaseOpen.Value = true;

                //gameService.msgDialogError.Value = result.msg;
                //gameService.msgDialogTitleError.Value = "";
                //gameService.isShowDialogError.Value = true;
            }

        });
    }

    public void CloseButton()
    {
        gameService.isNFTShowcaseOpen.Value = false;
    }

    public void LoadNFTVideoUrl(string url)
    {
        NFTVideo.SetActive(true);
        NFTImage.gameObject.SetActive(false);

        NFTVideoPlayer.url = url;
        NFTVideoPlayer.Play();
    }
    public void LoadNFTCard(string media, string rank, string name, string nft_id)
    {
        NFTVideo.SetActive(false);
        NFTImage.gameObject.SetActive(true);

        NFTImage.LoadNFTImage(media);
        NFTImage.LoadNFTRankSprite(rank);
        NFTImage.LoadNFTInfo(name, nft_id);
    }
}
