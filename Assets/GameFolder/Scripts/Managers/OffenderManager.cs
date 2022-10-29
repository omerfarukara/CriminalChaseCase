using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffenderManager : MonoSingleton<OffenderManager>
{
    internal List<GameObject> offenders = new List<GameObject>();

    EventData _eventData;
    PlayerController _player;

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
    }

    private void OnEnable()
    {
        _eventData.OnAssignPlayer += SetPlayer;
    }
    private void OnDisable()
    {
        _eventData.OnAssignPlayer -= SetPlayer;
    }

    void Start()
    {
        Singleton();
    }

    private void SetPlayer(PlayerController player)
    {
        _player = player;
    }


    public Transform GetTarget()
    {
        if (offenders.Count == 1)
        {
            return _player.transform;
        }
        else
        {
            return offenders[offenders.Count - 2].transform;
        }
    }

}
