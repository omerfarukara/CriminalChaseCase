using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    EventData _eventData;
    Animator _animator;

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _eventData.OnAssignPlayer?.Invoke(this);
    }

    void Update()
    {
        float force = Mathf.Abs(UIController.Instance.Vertical()) + Mathf.Abs(UIController.Instance.Horizontal());
        force = force > 1 ? 1 : force;
        _animator.SetFloat(Constants.AnimationTags.FORCE, force);
    }
}
