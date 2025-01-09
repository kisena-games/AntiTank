using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    // Move - клавиша F, Fire - клавиша G

    [SerializeField] private Animator _headAnimator;
    [SerializeField] private Animator _bodyAnimator;
    [SerializeField] private float _moveSpeed = 10f;

    private bool _isBodyMove = false;
    private bool _isHeadFire = false;

    private void Update()
    {
        UpdateHeadAnimator();
        UpdateBodyAnimator();

        if (_isBodyMove)
        {
            transform.Translate(transform.forward * _moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void UpdateHeadAnimator()
    {
        if (InputManager.IsTestFireTank && !_isHeadFire)
        {
            _isHeadFire = true;
            _headAnimator.SetBool("Fire", true);
        }
        else if (!InputManager.IsTestFireTank && _isHeadFire)
        {
            _isHeadFire = false;
            _headAnimator.SetBool("Fire", false);
        }
    }

    private void UpdateBodyAnimator()
    {
        if (InputManager.IsTestMoveTank && !_isBodyMove)
        {
            _isBodyMove = true;
            _bodyAnimator.SetBool("Move", true);
        }
        else if (!InputManager.IsTestMoveTank && _isBodyMove)
        {
            _isBodyMove = false;
            _bodyAnimator.SetBool("Move", false);
        }
    }
}
