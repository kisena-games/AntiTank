using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PauseAnimatorController : MonoBehaviour, IPauseHandler
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

    private void OnEnable()
    {
        GamePause.Instance.AddPauseList(this);
    }

    private void OnDisable()
    {
        GamePause.Instance.RemovePauseList(this);
    }
}
