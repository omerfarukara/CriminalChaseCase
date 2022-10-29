using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Chain : MonoBehaviour
{
    [SerializeField] private float yOffset;
    
    private LineRenderer _lineRenderer;
    private EventData _eventData;

    List<Transform> targetTransforms = new List<Transform>();

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _eventData = Resources.Load("EventData") as EventData;
    }

    private void OnEnable()
    {
        _eventData.OnConnectOffender += AddNewTarget;
        _eventData.OnJudgment += RemoveLastTarget;
    }

    private void Start()
    {
        AddNewTarget(transform);
    }

    private void Update()
    {
        if (targetTransforms.Count == 0) return;

        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _lineRenderer.SetPosition(i, targetTransforms[i].position + Vector3.up * yOffset);
        }
    }

    private void OnDisable()
    {
        _eventData.OnConnectOffender -= AddNewTarget;
        _eventData.OnJudgment -= RemoveLastTarget;
    }

    private void AddNewTarget(Transform target)
    {
        targetTransforms.Add(target);
        _lineRenderer.positionCount++;
    }

    private void RemoveLastTarget(Transform lastTargetTransform)
    {
        targetTransforms.Remove(lastTargetTransform);
        _lineRenderer.positionCount--;
    }
}