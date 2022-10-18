using System.Collections;
using UnityEngine;

//For firebase storage
using Firebase.Storage;

//For choose file
using System.IO;
using SimpleFileBrowser;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UserAvatar
{
	public string id;
	public long created_at;
	public long updated_at;
	public string email;
	public string wallet;
	public float coin;
	public string name;
	public string avatar;
}

public class UploadModel
{
	public UserAvatar user;
	public int coinInserted;
	public string accessToken;
}

public class SelectedAvatarModel
{
	public string nameAvatar;
	public bool isSelected;
}

public static class ButtonExtension
{	
	public static void AddEventListener<T, S> (this Button button, T param, S isSelected ,Action<T, S> OnClick, RectTransform rect,
		AvatarContain avatar, ImageLoader load)
    {
		button.onClick.AddListener(delegate ()
		{
			OnClick(param, isSelected);
			//rect.gameObject.SetActive(GameService.@object.isAvatarSelected.Value);

			load.avatar.sprite = avatar.transform.GetChild(1).GetComponent<Image>().sprite;
			GameService.@object.userAvatar.Value = load.avatar.sprite.name;
			PlayerPrefs.SetString("Avatar", GameService.@object.userAvatar.Value);
			var listAvatar = new List<SelectedAvatarModel>();
			GameService.@object.listAvatar.Value.ForEach(x =>
			{
				x.isSelected = false;
				if (x.nameAvatar == load.avatar.sprite.name)
                {
					x.isSelected = true;
                }
				listAvatar.Add(x);
			});

			GameService.@object.listAvatar.Value = listAvatar;
		});		
    }
}

public class UploadImage : MonoBehaviour
{
	#region Interact with server variable
	//private FirebaseStorage storage;
	//private StorageReference storageReference;
	//private readonly static string host = "https://dev.nftmarble.games/api";
	//private byte[] avatarByte;
	#endregion

	private const float STEP = 0.05f;
    [SerializeField] private AvatarContain contain;
    [SerializeField] private ImageLoader load;
	[SerializeField] private RectTransform imageContent;
	[SerializeField] private GridLayoutGroup imageLayout;
	[SerializeField] private Transform containItem;
	[SerializeField] private Sprite[] maleAvatar;
	[SerializeField] private Sprite[] femaleAvatar;

	[SerializeField] private Image userMenuAvatar;
	[SerializeField] private Button leftArrow;
	[SerializeField] private Button rightArrow;
	[SerializeField] private Scrollbar avatarScrollbar;
	private GameService gameService = GameService.@object;
	private ApiController apiController;

	void Start()
	{
		#region For open image dialog
		//FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));

		//FileBrowser.SetDefaultFilter(".jpg");

		//storage = FirebaseStorage.DefaultInstance;
		//storageReference = storage.GetReferenceFromUrl("gs://jackpot-cf8ad.appspot.com");

		//apiController = new ApiController(new JsonSerializationOption());

		//NativeGallery.CheckPermission(NativeGallery.PermissionType.Read);
		#endregion
		InitButtonEvent();
	}

    #region Upload image to server
 //   [System.Obsolete]
 //   public void ShowExplorerAndUpload()
 //   {
	//	StartCoroutine(ShowLoadDialogCoroutine(512));

	//	//ShowLoadDialogCoroutine(512);
	//}

 //   [System.Obsolete]
 //   IEnumerator ShowLoadDialogCoroutine(int maxSize)
	//{
	//	yield return new WaitForEndOfFrame();
	//	NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
	//	{
	//		Debug.Log("Image path: " + path);
	//		if (path != null)
	//		{
	//			// Create Texture from selected image
	//			Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
	//			if (texture == null)
	//			{
	//				Debug.Log("Couldn't load texture from " + path);
	//				return;
	//			}

	//			byte[] bytesS = File.ReadAllBytes(path);
	//			Debug.LogError("SIZE: " + bytesS.Length);

 //               if (bytesS.Length > 1000000)
 //               {
 //                   Debug.LogError("SIZE: " + texture.width + " / " + texture.height);
	//				gameService.msgDialogTitleError.Value = "OOOPS!!";
	//				gameService.isShowDialogError.Value = true;
 //                   gameService.msgDialogError.Value = "This image is too large to upload. Image size larger than 1MB is not supported";
 //               }
 //               else if (bytesS.Length > 1 && bytesS.Length < 999999)
 //               {
 //                   avatarByte = bytesS;
 //                   UploadAvatar(gameService.accessToken.Value);
 //               }
	//			else if(bytesS.Length < 1)
 //               {
	//				Debug.LogError("User not choose avatar");
 //               }

 //               // If a procedural texture is not destroyed manually, 
 //               // it will only be freed after a scene change
 //               Destroy(texture, 5f);
	//			}
	//		});

	//	Debug.Log("Permission result: " + permission);

	//}

 //   [System.Obsolete]
 //   public async void UploadAvatar(string accessToken)
 //   {
	//	var sub = "/profile/changeAvatar";
	//	var url = string.Format("{0}{1}", host, sub);

	//	WWWForm form = new WWWForm();
	//	form.AddBinaryData("file", avatarByte, null, mimeType: "image/png");

	//	UploadModel result = await apiController.Post<UploadModel>(url, form, accessToken);

	//	StartCoroutine(load.LoadImage(result.user.avatar));

	//	gameService.userAvatar.Value = result.user.avatar;

	//	Debug.LogError("Link: " + result.user.avatar);

	//}
    #endregion

	private void InitButtonEvent()
    {
		leftArrow.onClick.AddListener(() => ScrollAvatar(leftArrow));
		rightArrow.onClick.AddListener(() => ScrollAvatar(rightArrow));
    }
	public IEnumerator DisplayAvatar()
    {
		yield return new WaitForSeconds(0.1f);

		//GameDataServices.Instance.UpdateInfo(null, gameService.userName.Value, gameService.accessToken.Value, gameService.userBirthday.Value,
		//		 gameService.userGender.Value, gameService.userPhone.Value);

		Destroy();

        if (gameService.isMale.Value)
        {
			imageContent.sizeDelta = new Vector2((imageLayout.cellSize.x * maleAvatar.Length) + 360f, imageContent.sizeDelta.y);
			var listAvatar = new List<SelectedAvatarModel>();
			for (int i = 0; i < maleAvatar.Length; i++)
			{
				var item = Instantiate(contain, containItem);
				var bg = item.transform.GetChild(0).GetComponent<RectTransform>();
				var buttons = item.transform.GetChild(1).GetComponent<Button>();

				item.SetUpAvatar(maleAvatar[i], maleAvatar[i].name);
				item.transform.position = imageContent.transform.position;

				listAvatar.Add(new SelectedAvatarModel { nameAvatar = maleAvatar[i].name, isSelected = false });

				gameService.listAvatar.Value = listAvatar;

				buttons.AddEventListener(i, gameService.isAvatarSelected.Value, ItemClicked, bg, item, load);

				//gameService.spriteAvatar[i].sprite = maleAvatar[i];
			}
		}
		else if (gameService.isFemale.Value)
        {
			imageContent.sizeDelta = new Vector2((imageLayout.cellSize.x * femaleAvatar.Length) + 360f, imageContent.sizeDelta.y);
			var listAvatar = new List<SelectedAvatarModel>();
			for (int i = 0; i < femaleAvatar.Length; i++)
			{
				var item = Instantiate(contain, containItem);
				var bg = item.transform.GetChild(0).GetComponent<RectTransform>();
				var buttons = item.transform.GetChild(1).GetComponent<Button>();

				item.SetUpAvatar(femaleAvatar[i], femaleAvatar[i].name);
				item.transform.position = imageContent.transform.position;

				listAvatar.Add(new SelectedAvatarModel { nameAvatar = femaleAvatar[i].name, isSelected = false });

				gameService.listAvatar.Value = listAvatar;

				buttons.AddEventListener(i, gameService.isAvatarSelected.Value, ItemClicked, bg, item, load);

				//gameService.spriteAvatar[i].sprite = femaleAvatar[i];
			}
		}
    }

	public void MaleAvatar()
    {
		gameService.isUserChooseAvatar.Value = true;

		gameService.isMale.Value = true;
		gameService.isFemale.Value = false;

		StartCoroutine(DisplayAvatar());
    }

	public void FemaleAvatar()
	{
		gameService.isUserChooseAvatar.Value = true;

		gameService.isFemale.Value = true;
		gameService.isMale.Value = false;

		StartCoroutine(DisplayAvatar());
	}

	public void CloseSelectDisplyAvatar()
    {
		Destroy();
		gameService.isUserChooseAvatar.Value = false;
	}

	public void ConfirmAvatar()
    {
		GameDataServices.Instance.UpdateInfo(null, gameService.userName.Value, gameService.accessToken.Value, null,
				 null, null, gameService.userAvatar.Value);

		if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
			userMenuAvatar.sprite = load.avatar.sprite;

		Destroy();
		gameService.isUserChooseAvatar.Value = false;
		gameService.isUserChangeAvatar.Value = false;
	}

	private void Destroy()
	{
		foreach (Transform child in containItem.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

	private void ScrollAvatar(Button arrowButton)
    {
		if (arrowButton == leftArrow)
			avatarScrollbar.value = Mathf.Clamp(avatarScrollbar.value - STEP, 0, 1);
		else
			avatarScrollbar.value = Mathf.Clamp(avatarScrollbar.value + STEP, 0, 1);
	}
	void ItemClicked(int index, bool selected)
    {
		gameService.isAvatarSelected.Value = true;
		int preIndex = PlayerPrefs.GetInt("Index");

		PlayerPrefs.SetInt("Index", index);
		
		if(preIndex == index)
        {
			gameService.isAvatarSelected.Value = false;
		}
    }
}
