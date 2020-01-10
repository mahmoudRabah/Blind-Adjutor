using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class HoldingOnScreen : MonoBehaviour
{
    [SerializeField] UnityEvent foundUI;
    PointerEventData m_PointerEventData;
    [SerializeField] GraphicRaycaster m_Raycaster;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] ClickTimer clickTimer;
    [SerializeField] float waitingRepeatingDuration = 2;
    bool check = true;

    public bool Check { get => check; set => check = value; }
    void Update ()
    {

        if ( Check )
        {
            //Check if the left Mouse button is clicked
            if ( Input.GetKey (KeyCode.Mouse0) )
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData (m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult> ();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast (m_PointerEventData , results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach ( RaycastResult result in results )
                {
                    Debug.Log ("Hit " + result.gameObject.name);
                    if ( result.gameObject.GetComponent<ClickableHandler> () )
                    {
                        Check = false;
                        clickTimer.CurrentClicked = result.gameObject.GetComponent<Button> ();
                        Handheld.Vibrate ();
                        foundUI.Invoke ();
                        Invoke (nameof (CheckAgain) , MoneyGameManager.Instance.ClipLength + waitingRepeatingDuration);
                    }
                }
            }
        }
    }
    void CheckAgain ()
    {
        Check = true;
    }
}
