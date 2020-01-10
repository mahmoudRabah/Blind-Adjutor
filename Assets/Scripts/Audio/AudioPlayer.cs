using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AudioPlayer : DefaultTrackableEventHandler
{
    // Start is called before the first frame update
    [SerializeField] AudioClip clip;
    //To Prevent detect when menu is active 
    bool canDetect;

    public bool CanDetect { get => canDetect; set => canDetect = value; }

    protected override void OnTrackingFound ()
    {
        if ( CanDetect )
            PalySound ();
    }
    protected override void OnTrackingLost ()
    {
            StopSound ();
    }
    void PalySound ()
    {
        MoneyGameManager.Instance.Au.Stop ();
        MoneyGameManager.Instance.Au.clip = clip;
        MoneyGameManager.Instance.Au.Play ();
    }
    public void StopSound ()
    {
        MoneyGameManager.Instance.Au.clip = null;
        MoneyGameManager.Instance.Au.Stop ();
    }
}
