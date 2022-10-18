using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlay : UnityActiveSingleton<UIGamePlay>
{
    //[SerializeField] private JobStatusBar jobStatusBar;
    //[SerializeField] private DialogDetails dialogDetails;
    //private List<Job> jobList;
    //public List<Job> JobList { get => jobList; set => jobList = value; }

    //private Job currentJob;

    //private void OnEnable()
    //{
    //    EventSystem.Instance.Subscribe(EventType.ShowDialogDetails, HandleShowDialogDetails);
    //}
    //private void OnDisable()
    //{
    //    EventSystem.Instance.Unsubscribe(EventType.ShowDialogDetails, HandleShowDialogDetails);
    //}

    //private void HandleShowDialogDetails(Message message)
    //{
    //    currentJob = (Job)message.Data;
    //    dialogDetails.Setup(currentJob.JobTypee);
    //}


    //public void PostNotifications(JobType jobType, int value = 1)
    //{
    //    jobStatusBar.PostNotifications(jobType, value);
    //}
}
