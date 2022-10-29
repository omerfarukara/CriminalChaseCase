using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;

    Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float vertical = UIController.Instance.Vertical();
        float horizontal = UIController.Instance.Horizontal();
        _rigidbody.velocity = new Vector3(horizontal * speed, 0, vertical * speed); // HAREKET

        Vector3 direction = Vector3.right * horizontal + Vector3.forward * vertical;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime); // HAREKET ROTASYON
    }
}
