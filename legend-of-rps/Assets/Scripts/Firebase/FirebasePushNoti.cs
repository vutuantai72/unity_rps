using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Messaging;
using System;

public class FirebasePushNoti : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseMessaging.TokenReceived += TokenReceived;
        FirebaseMessaging.MessageReceived += MessageReceived;
        Subscribe();
    }

    private void Update()
    {
        Subscribe();
    }

    void Subscribe()
    {
        FirebaseMessaging.SubscribeAsync("/topics/LegendOfRPS");
    }

    private void MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log("Notification: " + e.Message);
    }

    private void TokenReceived(object sender, TokenReceivedEventArgs e)
    {
        Debug.Log("Notification: " + e.Token);
    }
}
