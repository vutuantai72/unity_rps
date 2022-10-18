using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class UserInformation
{
    public string userName;
    public string dateOfBirth;
    public string gender;
    public string phoneNumber;
    public string email;
} 

public class AccountSetting : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_InputField inputDateOfBirth;
    [SerializeField] private TMP_InputField inputPhoneNumber;
    [SerializeField] private TMP_Dropdown dropGender;
    [SerializeField] private TextMeshProUGUI userEmail;

    [SerializeField] private DatePickerControl datePick;
    [SerializeField] private RectTransform datePickerObj;

    [SerializeField] private ImageLoader loader;
    [SerializeField] private Image blankScreen;

    public static AccountSetting instance;
    private const string KEY_SAVE_USER_INFORMATION = "KEY_SAVE_USER_INFORMATION";
    private bool isEmpty = true;
    GameService gameService = GameService.@object;


    private UserInformation GetUserInformation(string jsonString)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<UserInformation>(jsonString);
    }
    
    private void SetUserInformation(UserInformation user)
    {
        var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(user);
        PlayerPrefs.SetString(KEY_SAVE_USER_INFORMATION, jsonString);
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //UpdateUserInformation();
        GetUserInfomation();
    }

    public async void GetUserInfomation()
    {
        await GameDataServices.Instance.GetUserInfo(callback: (result) =>
        {
            userEmail.text = gameService.userEmail.Value;
            inputUsername.text = gameService.userName.Value;

            if(SceneManager.GetActiveScene().buildIndex == 1)
                loader.OnShowAvatar();

            Debug.Log($"Username: {gameService.userName.Value}");
            inputDateOfBirth.text = gameService.userBirthday.Value;
            inputPhoneNumber.text = gameService.userPhone.Value;

            if (gameService.userGender.Value == "Male")
            {
                dropGender.value = 0;
            }
            else if (gameService.userGender.Value == "Female")
            {
                dropGender.value = 1;
            }
            else if (gameService.userGender.Value == "Other")
            {
                dropGender.value = 2;
            }

        });
    }

    public void UpdateUserInformation()
    {
        string value = "";
        gameService.userName.Value = inputUsername.text;
        gameService.userBirthday.Value = inputDateOfBirth.text;

        if (dropGender.value == 0)
            value = "Male";
        else if (dropGender.value == 1)
            value = "Female";
        else if (dropGender.value == 2)
            value = "Other";

        gameService.userGender.Value = value;
        gameService.userPhone.Value = inputPhoneNumber.text;

        if (string.IsNullOrEmpty(inputUsername.text))
        {
            gameService.msgDialogError.Value = "Username cannot be empty ";
            gameService.msgDialogTitleError.Value = "Error!!";
            gameService.isShowDialogError.Value = true;
        }
        else
        {
            GameDataServices.Instance.UpdateInfo(null, gameService.userName.Value, gameService.accessToken.Value, gameService.userBirthday.Value,
                gameService.userGender.Value, gameService.userPhone.Value);

            gameService.msgDialogError.Value = "Update info success";
            gameService.msgDialogTitleError.Value = "";
            gameService.isShowDialogError.Value = true;
        }      
    }

    private void UpdateUsername()
    {
        gameService.userName.Value = inputUsername.text;
        if (string.IsNullOrEmpty(inputUsername.text))
        {
            gameService.msgDialogError.Value = "Username cannot be empty ";
            gameService.msgDialogTitleError.Value = "Error!!";
            gameService.isShowDialogError.Value = true;
        }
        else
        {
            GameDataServices.Instance.UpdateInfo(null, gameService.userName.Value, gameService.accessToken.Value, null,
                null, null);

            gameService.msgDialogError.Value = "Update info success";
            gameService.msgDialogTitleError.Value = "";
            gameService.isShowDialogError.Value = true;
        }
    }

    private void OnUserNameEndEdit(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        gameService.userName.Value = value;
        //GameDataServices.Instance.UpdateInfo(null, gameService.userName.Value, gameService.accessToken.Value,
        //    null, null, null);
    }

    public void EditUsername(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }
        inputUsername.text = value;
        UpdateUsername();
    }

    public void OnDateOfBirthEndEdit()
    {
        inputDateOfBirth.text = datePick.dateText.text;

        //GameDataServices.Instance.UpdateInfo(null, gameService.userName.Value, gameService.accessToken.Value,
        //    gameService.userBirthday.Value, null, null);

        datePickerObj.gameObject.SetActive(false);
        blankScreen.gameObject.SetActive(false);
    }
    public void OnGenderEndEdit()
    {
        string value = "";

        if (dropGender.value == 0)
            value = "Male";
        else if (dropGender.value == 1)
            value = "Female";
        else if (dropGender.value == 2)
            value = "Other";

        gameService.userGender.Value = value;
        //GameDataServices.Instance.UpdateInfo(null, gameService.userName.Value, gameService.accessToken.Value,
        //    null, gameService.userGender.Value, null);
    }
    private void OnPhoneNumberEndEdit(string value)
    {
        gameService.userPhone.Value = value;

        //GameDataServices.Instance.UpdateInfo(null, gameService.userName.Value, gameService.accessToken.Value,
        //    null, null, gameService.userPhone.Value);
    }

    public void OpenDatePicker()
    {
        datePickerObj.gameObject.SetActive(true);
        blankScreen.gameObject.SetActive(true);
    }
    
    public void OpenChooseAvatar()
    {
        gameService.isUserChangeAvatar.Value = true;
    }

    public void CloseChooseAvatar()
    {
        gameService.isUserChangeAvatar.Value = false;
    }
}
