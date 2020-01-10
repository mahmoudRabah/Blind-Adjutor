using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// New volunteer page manager Contains 2 functions :
/// 1-AddNewVolunteer : 
/// gets the data from user from 2 TMPro.TMP_InputFields (phoneNum_InputField,username_InputField) ,then
/// add them to player prefs. PlayerPrefs saves only one value,
/// So, I used PlayerPrefs2 (http://wiki.unity3d.com/index.php?title=ArrayPrefs2&oldid=18273) to Save String Array.
/// PlayerPrefs2 is in class called SaveData.
/// SaveData.SetStringArray takes 2 params (Key, string [] values)
/// to use SaveData.SetStringArray, I get SaveData.GetStringArray("Volunteer_Number") -Old Array- ,then
/// coppied it into new array with Length = oldArrayLength+1 to add new element
/// 2 - ResetUserAddedText : resets phoneNum_InputField and username_InputField texts and stop hint animation
/// </summary>
public class NewVolunteer : Page
{
    #region Vars
    #region SerializeField
    //Canvas/Add/Content/Phone InputField (TMP)
    [SerializeField] TMPro.TMP_InputField phoneNum_InputField;
    //Canvas/Add/Content/Username InputField (TMP)
    [SerializeField] TMPro.TMP_InputField username_InputField;
    //Canvas/Add/ User Added Text (TMP)
    [SerializeField] TMPro.TMP_Text userAddedText;
    //Canvas/Add/ User Added Text (TMP)
    [SerializeField] Animator userAddedAnimator;
    #endregion
    #region Private Vars
    string [] phoneNums;
    VolunteerData volunteer;

    #endregion
    #endregion

    #region Functions
    #region Public Functions
    #region Buttons Functions
    //Save new volunteer to playerprefs , used in button(Canvas/Add/Content/Add Button)
    public void AddNewVolunteer ()
    {
        if ( MoneyGameManager.Instance.SavingDataType == SavingDataType.Local )
            AddNewVolunteerLocally ();
        else if ( MoneyGameManager.Instance.SavingDataType == SavingDataType.Firebase )
            AddNewVolunteerFirebase ();
        //reset input field texts to start writing again
        ResetInputFieldTexts ();
        //Play fading animation of hit text
        ShowHint ();
    }
    //Used in button(Canvas/UpperBar/Content/BackButton)
    public void ResetUserAddedText ()
    {
        //reset input field texts to start writing again
        ResetInputFieldTexts ();
        //Stop fading animation of hit text
        HideHint ();
    }
    #endregion
    #endregion
    #region Private Functions
    void ResetInputFieldTexts ()
    {
        phoneNum_InputField.text = "";
        username_InputField.text = "";
    }
    void ShowHint ()
    {
        userAddedAnimator.SetBool ("isUserAdded" , true);
    }
    void HideHint ()
    {
        userAddedAnimator.SetBool ("isUserAdded" , false);
    }
    void UpdateHint (bool isSaved)
    {
        if ( isSaved )
        {
            userAddedText.text = username_InputField.text + " is added";
        }
        else
        {
            userAddedText.text = phoneNum_InputField.text + " is already added before!!!";
        }
    }
    //Saving Data on Playerprefs
    void AddNewVolunteerLocally ()
    {
        //Create new phoneNums array
        phoneNums = new string [SaveDataLocally.GetStringArray ("Volunteer_Number").Length + 1];
        //Initialize new phoneNums by old array and add new volunteer
        for ( int i = 0 ; i < SaveDataLocally.GetStringArray ("Volunteer_Number").Length ; i++ )
        {
            phoneNums [i] = SaveDataLocally.GetStringArray ("Volunteer_Number") [i]; // get old volunteers data from old array
        }
        phoneNums [phoneNums.Length - 1] = phoneNum_InputField.text; // get new volunteer data from InputField
        //Save new volunteer and check if it is already saved before
        bool isDataSaved = SaveDataLocally.SetStringArray ("Volunteer_Number" , phoneNums);
        //Update hint according to isDataSaved bool variable
        UpdateHint (isDataSaved);
    }
    //Saving Data on Firebase
    void AddNewVolunteerFirebase ()
    {
        volunteer = new VolunteerData (username_InputField.text , phoneNum_InputField.text);
        MoneyGameManager.Instance.JSON_Handler.WriteUserData (volunteer);
        if ( IsDataFilled () )
        {
            //TODO Update Hint
        }
        else
        {
            Debug.LogError ("One or more of InputFields are empty");
            //TODO Error Hint
        }
    }
    //Validate Data
    bool IsDataFilled ()
    {
        if ( username_InputField.text != "" && phoneNum_InputField.text != "" )
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion
    #endregion
}
