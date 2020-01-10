using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CallsManager : Page
{
    private List<string> allNumbers = new List<string> ();
    private int maxIndex;
    private string phoneNum;

    private void GetAllNumbers ()
    {
        allNumbers.Clear ();
        if (MoneyGameManager.Instance.SavingDataType == SavingDataType.Local)
        {
            for (int i = 0; i < SaveDataLocally.GetStringArray ("Volunteer_Number").Length; i++)
            {
                allNumbers.Add (SaveDataLocally.GetStringArray ("Volunteer_Number") [i]);
            }
        }
        else if (MoneyGameManager.Instance.SavingDataType == SavingDataType.Firebase)
        {
            for (int i = 0; i < MoneyGameManager.Instance.JSON_Handler.UserValue.Count; i++)
            {
                allNumbers.Add (MoneyGameManager.Instance.JSON_Handler.UserValue [i]);
            }
        }
        print (allNumbers.Count);
        for (int i = 0; i < MoneyGameManager.Instance.JSON_Handler.UserValue.Count; i++)
        {
            print (allNumbers [i]);
        }
    }
    private void CallAction (string num)
    {

        //For accessing static strings(ACTION_DIAL) from android.content.Intent
        AndroidJavaClass intentStaticClass = new AndroidJavaClass ("android.content.Intent");
        string actionCall = intentStaticClass.GetStatic<string> ("ACTION_DIAL");

        //Create Uri
        AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse", num);

        //Pass ACTION_CALL and Uri.parse to the intent
        AndroidJavaObject intent = new AndroidJavaObject ("android.content.Intent", actionCall, uriObject);
        AndroidJavaClass  unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");

        try
        {
            //Start Activity
            unityActivity.Call ("startActivity", intent);
        }
        catch (AndroidJavaException e)
        {
            Debug.LogWarning ("Failed to Dial number: " + e.Message);
        }
    }
    public void CallAnyNumber ()
    {
        GetAllNumbers ();
        if (allNumbers.Count > 0) // To Prevnt Get Data if the array is empty
        {
            maxIndex = allNumbers.Count - 1;
            //var randomNum = allNumbers [Random.Range (0 , maxIndex)];
            var randomNum = GetRandomNumber (allNumbers);
            phoneNum = "tel:" + randomNum;
            if (phoneNum != MoneyGameManager.Instance.LastCallNum)
            {

                CallAction (phoneNum);
                maxIndex = 0;
                MoneyGameManager.Instance.LastCallNum = randomNum;
                print (phoneNum);
            }
            else
            {
                CallAnyNumber ();
            }
        }
    }

    public string GetRandomNumber (List<string> numbers)
    {
        int index = Random.Range (0, maxIndex);
        string num = numbers [index];
        if (numbers.Count > 1)
        {
            if (num != MoneyGameManager.Instance.LastCallNum)
            {
                return num;
            }
            else
            {
                List<string> newNumbers = numbers;
                newNumbers.Remove (num);
                index = Random.Range (0, maxIndex);
                num = numbers [index];
            }
        }
        return num;
    }
}