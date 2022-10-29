using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandCuff : MonoBehaviour
{
    bool _isCollected;

    public bool IsCollected
    {
        get { return _isCollected; }
        set { _isCollected = value; }
    }

    private void Start()
    {
        ScaleUpDown();
    }

    void Update()
    {
        if (!IsCollected)
        {
            transform.Rotate(Vector3.up);
        }
    }

    private void ScaleUpDown()
    {
        transform.DOScale(Vector3.one * 0.75f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}