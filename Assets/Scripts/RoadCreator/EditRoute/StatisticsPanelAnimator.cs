using UnityEngine;

public class StatisticsPanelAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show()
    {
        _animator.SetTrigger("show");
    }

    public void Hide()
    {
        _animator.SetTrigger("hide");
    }
}
