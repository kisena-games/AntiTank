using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraMode : MonoBehaviour
{
    [SerializeField] private GameObject _head;
    [SerializeField] private MWHeadMovement _headMovement;
    [SerializeField] private GameObject _sniperCamera;

    private CameraMode _currentMode = CameraMode.Default;

    private void OnEnable()
    {
        InputManager.SwitchCameraModeAction += Switch;
    }

    private void OnDisable()
    {
        InputManager.SwitchCameraModeAction -= Switch;
    }

    private void Switch()
    {
        if (GamePause.Instance.IsPause)
        {
            return;
        }

        if (_currentMode == CameraMode.Default)
        {
            
            _headMovement.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _sniperCamera.SetActive(true);
            _currentMode = CameraMode.Sniper;
        }
        else
        {
            _sniperCamera.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _headMovement.enabled = true;
            _currentMode = CameraMode.Default;
        }
    }
}

public enum CameraMode
{
    Default,
    Sniper
}
