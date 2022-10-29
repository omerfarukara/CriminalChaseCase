using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class JudgeManager : MonoSingleton<JudgeManager>
{
    [Header("Judgment---Process")] [SerializeField]
    Transform firstPoint;

    [SerializeField] Transform secondPoint;

    [SerializeField] private Transform handCuffSpawnPoint;
    [SerializeField] private GameObject handCuff;
    [SerializeField] float duration;

    Animator _animator;

    EventData _eventData;

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Singleton();
    }


    private void SpawnHandCuff()
    {
        GameObject newHandCuff = Instantiate(this.handCuff, handCuffSpawnPoint);
        ColliderOperations.Instance.handCuffs.Push(newHandCuff);
        float count = ColliderOperations.Instance.handCuffs.Count;
        Vector3 distance = new Vector3(0, count * 0.3f, 0);
        newHandCuff.GetComponent<HandCuff>().IsCollected = true;
        newHandCuff.transform.parent = ColliderOperations.Instance.CollectableParent;
        newHandCuff.transform.DOLocalJump(distance, 1.5f, 1, 0.3f);
        newHandCuff.transform.DOLocalRotate(new Vector3(90, 180, 0), 0.3f);
    }

    public void Judgment(Transform lastTargetTransform)
    {
        StartCoroutine(JudgmentCoroutine(lastTargetTransform));
    }


    private IEnumerator JudgmentCoroutine(Transform lastTargetTransform)
    {
        lastTargetTransform.DOMove(firstPoint.position, duration).OnComplete(() => { lastTargetTransform.DOMove(secondPoint.position, duration).OnComplete(() =>
            {
                SpawnHandCuff();
                Destroy(lastTargetTransform.gameObject);
                GameController.Instance.OffenderControllers.Remove(lastTargetTransform.gameObject.GetComponent<OffenderController>());
                if (GameController.Instance.OffenderControllers.Count == 0)
                {
                    UIController.Instance.Win();
                }
            }); }
        );
        _animator.SetTrigger(Constants.AnimationTags.THUMBS);
        yield return null;
    }
}