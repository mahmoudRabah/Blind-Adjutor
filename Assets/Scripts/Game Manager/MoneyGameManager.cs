using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGameManager : MonoBehaviour
{
    #region Vars
    #region Serialize Fields
    [Header ("Audio Players (Money Trackables Objects)")]
    [SerializeField] AudioPlayer [] audioPlayers;
    [Header ("Audio Source")]
    [Space (10)]
    [SerializeField] AudioSource au;
    [Header ("Main Menu")]
    [Space (10)]
    [SerializeField] MainMenuManager mainMenu;
    [Header ("UI Pages from Canvas")]
    [Space (10)]
    [SerializeField] Page [] pages;
    [Header ("Choose Saving Data Type")]
    [Space (10)]
    [SerializeField] SavingDataType savingDataType;
    [Space (10)]
    [SerializeField] JSON_Firebase jSON_Handler;
    #endregion
    #region private vars
    AudioClip [] auClips;
    string lastCallNum;
    float clipLength;
    int audioClipsIndex = 0;
    float duration;
    #region staice vars
    #region singleton instance
    static MoneyGameManager instance;
    #endregion
    #endregion
    #endregion
    #region properties
    //singleton property instance
    public static MoneyGameManager Instance { get => instance; set => instance = value; }
    //Vuforia Trackables Objects, Children of (ARObjects) in Hierarchy
    public AudioPlayer [] AudioPlayers { get => audioPlayers; set => audioPlayers = value; }
    //AudioSource is used to say currency's name (AudioManager) in Hierarchy
    public AudioSource Au { get => au; set => au = value; }
    //MainMenu page in canvas
    public MainMenuManager MainMenu { get => mainMenu; set => mainMenu = value; }
    //All pages in canvas 
    public Page [] Pages { get => pages; set => pages = value; }
    //Data will be saved in player prefs if  SavingDataType is Local, Or saving on Firebase if SavingDataType is Firebase 
    public SavingDataType SavingDataType { get => savingDataType; set => savingDataType = value; }
    public JSON_Firebase JSON_Handler { get => jSON_Handler; set => jSON_Handler = value; }
    public string LastCallNum { get => lastCallNum; set => lastCallNum = value; }
    public float ClipLength { get => clipLength; set => clipLength = value; }
    //if Play array of Clips
    public AudioClip [] AuClips { get => auClips; set => auClips = value; }
    #endregion
    #endregion
    private void Awake ()
    {
        //Create Singleton
        if ( Instance == null )
        {
            Instance = this;
        }
    }
    private void Start ()
    {
        //Open main menu automatically on start app
        ReturnToMainMenu ();
    }
    //Open main menu
    public void ReturnToMainMenu ()
    {
        MainMenu.ShowPage ();
    }
    public float PlaySound (AudioClip clip)
    {
        Au.Stop ();
        Au.clip = clip;
        Au.Play ();
        ClipLength = clip.length;
        return ClipLength;
    }
}
