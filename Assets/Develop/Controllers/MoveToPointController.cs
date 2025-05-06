using UnityEngine;

public abstract class MoveToPointController : Controller
{
    protected readonly string _walkableMask = "Walkable";
    protected Character _character;
    protected Vector3 _targetPosition;
    protected float _reachingDistance = 0.2f;
    protected bool _isMoving;
    protected TargetPointView _targetPointView;
    protected Navigator _navigator;
    protected bool _isInvalidTarget;

    protected abstract Ray GetRay();

    protected override Vector3 GetTargetDirection()
    {
        return _navigator.GetDirection(_character.CurrentPosition, _targetPosition).normalized;
    }

    protected bool IsTargetReached()
    {
        Vector3 toTarget = _character.CurrentPosition - _targetPosition;
        toTarget.y = 0;
        float distance = toTarget.magnitude;

        return distance <= _reachingDistance;
    }

    protected void SetDirection(Vector3 direction)
    {
        _character.SetMoveDirection(direction);
        _character.SetRotationDirection(direction);
    }

    protected bool TrySetTargetPosition()
    {
        _targetPosition = GetTargetWorldPosition(out _isInvalidTarget);
        if (_isInvalidTarget)
            return false;

        Vector3 direction = GetTargetDirection();
        
        if (direction != Vector3.zero) 
        {
            _isMoving = true;
            _targetPointView.Enable(_targetPosition);
            return true;
        }

        return false;
    }

    protected Vector3 GetTargetWorldPosition(out bool _isInvalidTarget)
    {        
        _isInvalidTarget = false;
        Vector3 position = new();
        Ray ray = GetRay();

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask(_walkableMask)))
            position = hit.point;
        else 
            _isInvalidTarget = true;

        return position;
    }
}
