using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Add Button in main menu is used to to open add new volunteer page
/// </summary>
public class AddActions : MonoBehaviour
{
    //New volunteer page reference
    [SerializeField] NewVolunteer addNewVolunteer;
    //Open add new volunteer page (used in (Add Button) in Canvas/Main Menu/Menu Buttons/Add Button )
    public void OpenAddNewPage ()
    {
        addNewVolunteer.ShowPage ();
    }
}
