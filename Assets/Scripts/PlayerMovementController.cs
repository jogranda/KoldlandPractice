using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5f;

    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 10f;
        }
        else
        {
            _speed = 5f;
        }
    }
}
