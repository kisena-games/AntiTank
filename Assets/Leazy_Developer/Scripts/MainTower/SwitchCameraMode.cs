using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraMode : MonoBehaviour
{
    [SerializeField] private GameObject _sniperCamera;
    //[SerializeField] private GameObject _sniperPanel;

    public static CameraMode CurrentMode { get; private set; }

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

        if (CurrentMode == CameraMode.Default)
        {
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //_sniperPanel.SetActive(true);
            _sniperCamera.SetActive(true);
            CurrentMode = CameraMode.Sniper;
        }
        else
        {
            //_sniperPanel.SetActive(false);
            _sniperCamera.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CurrentMode = CameraMode.Default;
        }
    }
}

public enum CameraMode
{
    Default,
    Sniper
}
