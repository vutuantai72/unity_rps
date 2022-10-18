using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WalletConnectSharp.Unity;

[System.Serializable]
public enum WalletElement
{
    Withdraw = 0,
    Swap
}

public class MyWalletUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI walletAddress;
    [SerializeField] private Button copyButton;
    [SerializeField] private RectTransform successMessage;
    [SerializeField] private ImageLoader cardImage;
    [SerializeField] private GameObject NftCardImagePrefab;
    [SerializeField] private GameObject NftShowcasePrefab;
    [SerializeField] private Transform NftCardContentHolder;
    [SerializeField] private NFTInWallet NFTInWallet;

    #region For wallet in Menu Scene
    private WalletElement walletElement = WalletElement.Swap;
    [SerializeField] private RectTransform myWallet;
    [SerializeField] private RectTransform rootSwap;
    [SerializeField] private List<GameObject> walletElements;
    [SerializeField] private SwapMenu swapMenu;
    #endregion

    GameService gameService = GameService.@object;

    private void Start()
    {
        myWallet.gameObject.SetActive(false);
        OnCloseAll();
    }

    private void OnEnable()
    {
       GetMyNFTList();
    }
    private void Update()
    {
        if (string.IsNullOrEmpty(walletAddress.text))
            copyButton.gameObject.SetActive(false);
        else
            copyButton.gameObject.SetActive(true);
    }

    public void CopyWalletAddress()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = gameService.userWall.Value;
        textEditor.SelectAll();
        textEditor.Copy();
        successMessage.gameObject.SetActive(true);
        StartCoroutine(HideMessage());
    }

    public async Task GetMyNFTList()
    {
        //clear nft card parent, destroy all child gameobject in card holder. Prevent instanntiate multiple card when this function called
        foreach (Transform child in NftCardContentHolder)
        {
            Destroy(child.gameObject);
        }
        await GameDataServices.Instance.GetMyNFTList(result =>
        {
            if (result == null)
                return;
            gameService.myNFT.Value = result;
            for (int i = 0; i < gameService.myNFT.Value.total; i++)
            {
                //cardImage.OnShowImage(gameService.myNFT.Value.data[i].media);

                GameObject nftCard = Instantiate(NftCardImagePrefab, NftCardContentHolder);
                NFTCard nftInfo = nftCard.GetComponent<NFTCard>();

                nftInfo.LoadNFTImage(gameService.myNFT.Value.data[i].media);
                nftInfo.LoadNFTRankSprite(gameService.myNFT.Value.data[i].rank);
                nftInfo.LoadNFTInfo(gameService.myNFT.Value.data[i].name, gameService.myNFT.Value.data[i].nft_id);
                _AddEventMyNFT();
            }
        });
   
    }

    private void _AddEventMyNFT()
    {
            foreach (Transform child in NftCardContentHolder)
            {
                child.gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    int index = child.GetSiblingIndex();
                    string nftID = gameService.myNFT.Value.data[index].id;

                    NFTInWallet.nftIDToClaim = nftID;
                    gameService.isNFTShowcaseOpen.Value = true;
                    //ClaimNFTReward(nftID);
                    if (gameService.myNFT.Value.data[index].media_type == "VIDEO")
                    {
                        string videoUrl = gameService.myNFT.Value.data[index].media;
                        NFTInWallet.LoadNFTVideoUrl(videoUrl);
                    }
                    if (gameService.myNFT.Value.data[index].media_type == "IMAGE")
                    {
                        NFTInWallet.LoadNFTCard(gameService.myNFT.Value.data[index].media, gameService.myNFT.Value.data[index].rank, gameService.myNFT.Value.data[index].name, gameService.myNFT.Value.data[index].nft_id);
                    }

                    if (gameService.myNFT.Value.data[index].nft_classify == "AIRDROP_COIN")
                    {
                        NFTInWallet.claimNFTRewardButton.gameObject.SetActive(true);
                        if (gameService.myNFT.Value.data[index].status == "ENABLE")
                        {
                            NFTInWallet.claimNFTRewardButtonText.text = "Claim";
                            NFTInWallet.claimNFTRewardButton.interactable = true;
                        }
                        else
                        {
                            NFTInWallet.claimNFTRewardButtonText.text = "Claimed";
                            NFTInWallet.claimNFTRewardButton.interactable = false;
                        }
                    }
                    else
                    {
                        NFTInWallet.claimNFTRewardButton.gameObject.SetActive(false);
                    }
                });
            }
        
    }
    
    //private void ClaimNFTReward(string nftID)
    //{
    //    GameDataServices.Instance.ClaimNFTAirdropReward(nftID, result => {
    //        if (result == null)
    //        {
    //            return;
    //        }
                

    //        if(result.code != 0)
    //        {
    //            Debug.Log("@@@ nft sir " + result.code);

    //            //gameService.isNFTShowcaseOpen.Value = true;

    //            //gameService.msgDialogError.Value = result.msg;
    //            //gameService.msgDialogTitleError.Value = "";
    //            //gameService.isShowDialogError.Value = true;
    //        }
            
    //    });
    //}

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(5f);
        successMessage.gameObject.SetActive(false);
    }

    #region Interact wallet UI in menu scene 
    public void OpenWalletUI()
    {
        myWallet.gameObject.SetActive(true);
        if (string.IsNullOrEmpty(WalletConnect.ActiveSession?.Accounts?[0]))
        {
            gameService.userWall.Value = string.Empty;
        }
        else
        {
            gameService.userWall.Value = WalletConnect.ActiveSession.Accounts[0];
        }
    }

    public void CloseWalletUI()
    {
        myWallet.gameObject.SetActive(false);
    }

    public void OnShowSwap()
    {
        rootSwap.gameObject.SetActive(true);
        if (string.IsNullOrEmpty(WalletConnect.ActiveSession.Accounts?[0]))
        {
            swapMenu.OnShowConnect();
        }

        UpdateUI(WalletElement.Swap);
    }

    public void OnShowWithdraw()
    {
        UpdateUI(WalletElement.Withdraw);
    }

    public void OnComingSoon()
    {
        gameService.msgDialogTitleError.Value = "";
        gameService.msgDialogError.Value = "This feature will be coming soon";
        gameService.isShowDialogError.Value = true;
    }

    public void OnCloseAll()
    {
        AudioServices.Instance.PlayClickAudio();

        for (int i = 0; i < walletElements.Count; i++)
        {
            walletElements[i].SetActive(false);
        }
    }

    private void UpdateUI(WalletElement walletElement)
    {
        AudioServices.Instance.PlayClickAudio();

        this.walletElement = walletElement;
        for (int i = 0; i < walletElements.Count; i++)
        {
            walletElements[i].SetActive(i == (int)this.walletElement);
        }
    }
    #endregion
}
