using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHandler : MonoBehaviour
{
    [SerializeField] SOActionEvent clickableActionEvent;

    public SOActionEvent ClickableActionEvent { get => clickableActionEvent; set => clickableActionEvent = value; }
}
