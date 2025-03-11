using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PauseAnimatorController : MonoBehaviour
{
    private Animator _animator;

    public void IsPaused(bool paused)
    {
        _animator.enabled = !paused;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
