using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OffenderController : MonoBehaviour
{
    [SerializeField] private float followSpeed;

    private Transform _target;
    private Vector3 offset;

    bool canFollow = false;

    private void Update()
    {
        if (canFollow)
        {
            Vector3 relativePosition = _target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePosition);

            if (Vector3.Distance(_target.position, transform.position) < 1) return;

            transform.position += transform.forward * (followSpeed * Time.deltaTime);
        }
    }

    public void RemoveTarget()
    {
        canFollow = false;
        _target = null;
    }

    public void AssignTarget(Transform target)
    {
        _target = target;
        StartCoroutine(SetCanFollow());
    }

    IEnumerator SetCanFollow()
    {
        yield return new WaitForSeconds(0.3f);
        offset = transform.position - _target.position;
        canFollow = true;
    }
}