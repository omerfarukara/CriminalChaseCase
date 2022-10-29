using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "Data/EventData")]
public class EventData : ScriptableObject
{
    public Action OnWin;
    public Action OnFollow;
    public Action<PlayerController> OnAssignPlayer;
    public Action<Transform> OnConnectOffender;
    public Action<Transform> OnJudgment;
    
}
