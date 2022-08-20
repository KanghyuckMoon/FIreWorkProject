using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BaseAI : MonoBehaviour
{
    [SerializeField] private float range = 10.0f;
    private Animator Animator
	{
        get
		{
            _animator ??= GetComponent<Animator>();
            return _animator;

        }
	}

    private NavMeshAgent NavMeshAgent
	{
        get
		{
            _navMesh ??= GetComponent<NavMeshAgent>();
            return _navMesh;

        }
	}

    private NavMeshAgent _navMesh = null;
    private Animator _animator = null;
    private bool _isMove;

	private void Start()
	{
        SetMove();
    }

	// Update is called once per frame
	private void Update()
    {

        if (_isMove)
        {
            if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
            {
                _isMove = false;
                Invoke("SetMove", 3);
            }
        }
        AnimationSetting();
    }

    private void SetMove()
    {
        Vector3 point = Vector3.zero;
        if (RandomPoint(transform.position, range, out point))
        {
            NavMeshAgent.SetDestination(point);
            _isMove = true;
        }
    }

    private void IsMoveFalse()
    {
        _isMove = false;
    }

    private void AnimationSetting()
    {
        if (NavMeshAgent.velocity.magnitude > 0f)
        {
            Animator.SetBool("IsMove", true);
        }
        else
        {
            Animator.SetBool("IsMove", false);
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

}
