using UnityEngine;

/// <summary>
/// MenuActions is used in main menu page buttons to open other pages[callsPage, detectPage, mapPage]
/// </summary>
public class MenuActions : MonoBehaviour
{
    [SerializeField] Page callsPage;
    [SerializeField] Page detectPage;
    [SerializeField] Page mapPage;
    [Space (10)]
    [Header ("Action Events")]
    [SerializeField] SOActionEvent detectEvent;
    [SerializeField] SOActionEvent callEvent;
    [SerializeField] SOActionEvent mapEvent;
    [Space (10)]
    [Header ("Action Audios")]
    [SerializeField] AudioClip detectClip;
    [SerializeField] AudioClip callClip;
    [SerializeField] AudioClip mapClip;
    [Space (10)]
    [Header ("Timer")]
    [SerializeField] ClickTimer clickTimer;

    void OpenMap ()
    {
        mapPage.ShowPage ();
        DisableDetect ();
        Application.OpenURL ("http://maps.google.com/maps?q=10th+of+Ramadan+City,+Ash+Sharqia+Governorate");
    }
    void OpenDetect ()
    {
        detectPage.ShowPage ();
        EnableDetect ();
        MoneyGameManager.Instance.MainMenu.gameObject.SetActive (false);
    }
    void OpenCall ()
    {
        MoneyGameManager.Instance.JSON_Handler.GetAllUsersData ();
        callsPage.ShowPage ();
        DisableDetect ();
    }
    void EnableDetect ()
    {
        for ( int i = 0 ; i < MoneyGameManager.Instance.AudioPlayers.Length ; i++ )
        {
            MoneyGameManager.Instance.AudioPlayers [i].CanDetect = true;
        }
    }
    void DisableDetect ()
    {
        for ( int i = 0 ; i < MoneyGameManager.Instance.AudioPlayers.Length ; i++ )
        {
            MoneyGameManager.Instance.AudioPlayers [i].CanDetect = false;
            MoneyGameManager.Instance.AudioPlayers [i].StopSound ();
        }
    }
    public void MainMenuFirstClickHandler ()
    {
        if ( clickTimer.CurrentClicked.GetComponent<ClickableHandler> ().ClickableActionEvent == detectEvent )
        {
            MoneyGameManager.Instance.PlaySound (detectClip);
        }
        else if ( clickTimer.CurrentClicked.GetComponent<ClickableHandler> ().ClickableActionEvent == callEvent )
        {
            MoneyGameManager.Instance.PlaySound (callClip);
        }
        else if ( clickTimer.CurrentClicked.GetComponent<ClickableHandler> ().ClickableActionEvent == mapEvent )
        {
            MoneyGameManager.Instance.PlaySound (mapClip);
        }
    }
    public void MainMenuSecondClickHandler ()
    {
        if ( clickTimer.CurrentClicked.GetComponent<ClickableHandler> ().ClickableActionEvent == detectEvent )
        {
            OpenDetect ();
        }
        else if ( clickTimer.CurrentClicked.GetComponent<ClickableHandler> ().ClickableActionEvent == callEvent )
        {
            OpenCall ();
        }
        else if ( clickTimer.CurrentClicked.GetComponent<ClickableHandler> ().ClickableActionEvent == mapEvent )
        {
            OpenMap ();
        }
    }
}
