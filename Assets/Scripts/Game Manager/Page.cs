using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public void ShowPage ()
    {
        for ( int i = 0 ; i < MoneyGameManager.Instance.Pages.Length ; i++ )
        {
            if ( MoneyGameManager.Instance.Pages [i] != this )
            {
                MoneyGameManager.Instance.Pages [i].gameObject.SetActive (false);
            }
            else
            {
                MoneyGameManager.Instance.Pages [i].gameObject.SetActive (true);
            }
        }
    }
    public virtual void PageActions ()
    {

    }
}
