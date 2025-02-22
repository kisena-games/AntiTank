using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsInputManager : MonoBehaviour
{
    public static Action<int> CheatHealthAction;

    private Queue<KeyCode> _inputQueue = new Queue<KeyCode>();
    private KeyCode[] _cheatCode = { KeyCode.I, KeyCode.D, KeyCode.D, KeyCode.Q, KeyCode.D };
    private int _maxLives = 1000;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            DetectKeyPress();
        }
    }

    void DetectKeyPress()
    {
        foreach (KeyCode key in _cheatCode)
        {
            if (Input.GetKeyDown(key))
            {
                _inputQueue.Enqueue(key);

                if (_inputQueue.Count > _cheatCode.Length)
                {
                    _inputQueue.Dequeue();
                }

                CheckCheatCode();
                return;
            }
        }
    }

    void CheckCheatCode()
    {
        if (_inputQueue.Count == _cheatCode.Length)
        {
            KeyCode[] currentSequence = _inputQueue.ToArray();
            bool isMatch = true;

            for (int i = 0; i < _cheatCode.Length; i++)
            {
                if (currentSequence[i] != _cheatCode[i])
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                ActivateCheat();
                _inputQueue.Clear();
            }
        }
    }

    void ActivateCheat()
    {
        CheatHealthAction?.Invoke(_maxLives);
    }
}
