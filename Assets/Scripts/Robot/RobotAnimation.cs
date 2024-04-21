using UnityEngine;

[RequireComponent(typeof(Animator))]

public class RobotAnimation : MonoBehaviour
{
    private Animator _animator;

    //private int _openHash = Animator.StringToHash("Open");
    private int _walkHash = Animator.StringToHash("Walk");
    private int _rollHash = Animator.StringToHash("Roll");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        _animator.SetBool(_walkHash, false);
        _animator.SetBool(_rollHash, false);
    }

    public void SetWalk()
    {
        _animator.SetBool(_walkHash, true);
        _animator.SetBool(_rollHash, false);
    }

    public void SetRoll()
    {
        _animator.SetBool(_rollHash, true);
        _animator.SetBool(_walkHash, false);
    }
}