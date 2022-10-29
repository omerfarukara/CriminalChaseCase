using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Security.Cryptography;
using UnityEngine;
using DG.Tweening;

public class ColliderOperations : MonoSingleton<ColliderOperations>
{
    [SerializeField] Transform collectableParent;
    [SerializeField] private int gap;
    [SerializeField] private int MoveSpeed;


    internal Stack<GameObject> handCuffs = new Stack<GameObject>();
    internal Stack<Transform> offendersTransforms = new Stack<Transform>();

    EventData _eventData;

    public Transform CollectableParent => collectableParent;

    private void Awake()
    {
        Singleton();
        _eventData = Resources.Load("EventData") as EventData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.HANDCUFF)) // KELEP�E �ZER�M�ZE GEL�YOR
        {
            other.tag = "Untagged";
            other.GetComponent<HandCuff>().IsCollected = true;
            GameObject handCuff = other.gameObject;
            handCuffs.Push(handCuff);
            float count = handCuffs.Count;
            Vector3 distance = new Vector3(0, count * 0.3f, 0);
            handCuff.transform.parent = collectableParent;
            handCuff.transform.DOLocalJump(distance, 1.5f, 1, 0.3f);
            handCuff.transform.DOLocalRotate(new Vector3(90, 180, 0), 0.3f);
        }

        if (other.CompareTag(Constants.Tags.OFFENDER))
        {
            if (handCuffs.Count > 0)
            {
                other.tag = "Untagged";

                if (offendersTransforms.Count == 0)
                {
                    other.GetComponent<OffenderController>().AssignTarget(transform);
                }
                else
                {
                    other.GetComponent<OffenderController>().AssignTarget(offendersTransforms.Peek());
                }

                _eventData.OnConnectOffender?.Invoke(other.transform);

                offendersTransforms.Push(other.transform);

                GameObject handCuff = handCuffs.Pop();
                handCuff.transform.DOLocalJump(Vector3.zero, 1.5f, 1, 0.3f);
                handCuff.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>
                {
                   Destroy(handCuff);
                });
            }
        }

        if (other.CompareTag(Constants.Tags.JUDGE))
        {
            if (offendersTransforms.Count > 0)
            {
                offendersTransforms.Peek().GetComponent<OffenderController>().RemoveTarget();
                _eventData.OnJudgment?.Invoke(offendersTransforms.Peek());
                other.GetComponent<JudgeManager>().Judgment(offendersTransforms.Pop());
            }
        }
    }
}