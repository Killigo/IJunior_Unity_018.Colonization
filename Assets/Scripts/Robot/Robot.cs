using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RobotAnimation))]
[RequireComponent(typeof(RobotMover))]

public class Robot : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 4;

    private Base _base;
    private Resource _resourse;
    private Flag _flag;
    private GameObject _target;
    private RobotAnimation _robotAnimation;
    private RobotMover _robotMover;

    public bool IsIdle { get; private set; }

    public event Action<Robot> BaseBuilded;

    private void Start()
    {
        _robotAnimation = GetComponent<RobotAnimation>();
        _robotMover = GetComponent<RobotMover>();

        IsIdle = true;
    }

    private void Update()
    {
        if (_resourse != null)
        {
            if (_target == _resourse.gameObject && Vector3.Distance(transform.position, _resourse.transform.position) <= _interactionDistance)
                TakeResource();

            if (_target == _base.gameObject && Vector3.Distance(transform.position, _base.transform.position) <= _interactionDistance)
                PutResource();
        }
        else
        {
            if (_flag != null && _target == _flag.gameObject && Vector3.Distance(transform.position, _flag.transform.position) <= _interactionDistance)
            {
                _target = null;
                _flag = null;
                IsIdle = true;
                BaseBuilded?.Invoke(this);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            transform.LookAt(_target.transform);

            if (_target == _resourse?.gameObject)
                _robotMover.Roll(transform.forward);
            else if (_target == _base?.gameObject)
                _robotMover.Walk(transform.forward);
            else if (_target == _flag?.gameObject)
                _robotMover.Walk(transform.forward);
        }
    }

    public void BuildNewBase(Flag flag)
    {
        IsIdle = false;
        _flag = flag;
        _target = flag.gameObject;
        _robotAnimation.SetWalk();
    }

    public void SetBase(Base gameBase)
    {
        _base = gameBase;
    }

    public void SetResource(Resource resource)
    {
        _resourse = resource;
        IsIdle = false;
        StartCoroutine(RollAnimationActivate(_resourse.gameObject));
    }

    private void TakeResource()
    {
        _target = null;
        _resourse.transform.parent = transform;
        StartCoroutine(WalkAnimationActivate(_base.gameObject));
    }

    private void PutResource()
    {
        _resourse.Destroy();
        _base.CollectResource();
        _resourse = null;
        _target = null;
        IsIdle = true;
        _robotAnimation.SetIdle();
    }

    private IEnumerator RollAnimationActivate(GameObject target)
    {
        _robotAnimation.SetRoll();
        yield return new WaitForSeconds(2);
        _target = target;
    }

    private IEnumerator WalkAnimationActivate(GameObject target)
    {
        _robotAnimation.SetWalk();
        yield return new WaitForSeconds(2);
        _target = target;
    }
}