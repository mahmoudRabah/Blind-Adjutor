using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ClickTimer : MonoBehaviour
{
    Button currentClicked;
    Button prevClicked;
    [SerializeField] float duration;
    [SerializeField] float repeatRate;
    [Space (10)]
    [Header ("Click Actions")]
    [SerializeField] UnityEvent singleClickAction;
    [SerializeField] UnityEvent doubleClickAction;
    float initialDuration;
    int clicksCounter;

    public Button CurrentClicked { get => currentClicked; set => currentClicked = value; }

    private void Start ()
    {
        initialDuration = duration;
    }
    public void IncreaseClicksCounterButton ()
    {
        if ( prevClicked != currentClicked )
        {
            ResetValues ();
            prevClicked = currentClicked;
            print ("prevClicked != currentClicked");
            CancelInvoke (nameof (CalculateTime));
        }
        if ( clicksCounter == 0 )
        {
            StartTimer ();
        }
        clicksCounter++;
    }
    void StartTimer ()
    {
        InvokeRepeating (nameof (CalculateTime) , 0 , repeatRate);
    }
    void CalculateTime ()
    {
        duration -= repeatRate;
        if ( duration <= 0.0f )
        {
            CancelInvoke (nameof (CalculateTime));
            AfterDurationPassedActions ();
            ResetValues ();
        }
    }
    void AfterDurationPassedActions ()
    {
        if ( clicksCounter >= 2 )
        {
            doubleClickAction.Invoke ();
            print ($"{CurrentClicked.gameObject.name} doubleClickAction");
            CancelInvoke (nameof (CalculateTime));
        }
        else
        {
            singleClickAction.Invoke ();
            print ($"{CurrentClicked.gameObject.name} singleClickAction");
        }
    }
    void ResetValues ()
    {
        duration = initialDuration;
        clicksCounter = 0;
    }
}
