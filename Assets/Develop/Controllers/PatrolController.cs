using UnityEngine;

public class PatrolController : MoveToPointController
{
    private Vector3 _centerOfPatrolingZone;
    private float _patrolingRadius = 10;
    private Vector3 _potentialTargetPosition;
    private float _offset = 0.1f;

    public PatrolController(Character character, Navigator navigator, TargetPointView targetPointView)
    {
        _isMoving = false;
        _character = character;
        _navigator = navigator;
        _targetPointView = targetPointView;
    }

    public override ControllersTypes ControllerType => ControllersTypes.Patrol;

    public override void Enable()
    {
        base.Enable();
        _centerOfPatrolingZone = _character.CurrentPosition;
        _potentialTargetPosition = GetPotentialPatrolPoint();
        TrySetTargetPosition();
    }

    private Vector3 GetPotentialPatrolPoint()
    {

        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float randomDistance = Mathf.Sqrt(Random.Range(0f, 1f)) * _patrolingRadius;

        float x = _centerOfPatrolingZone.x + randomDistance * Mathf.Cos(randomAngle);
        float z = _centerOfPatrolingZone.z + randomDistance * Mathf.Sin(randomAngle);
        float y = 0;

        return new Vector3(x, y, z);
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_isMoving)
        {
            if (IsTargetReached())
            {
                SetDirection(Vector3.zero);
                _isMoving = false;
                _targetPointView.Disable();
                _targetPosition = _character.CurrentPosition;
                return;
            }

            SetDirection(GetTargetDirection());
        }
        else
        {
            _potentialTargetPosition = GetPotentialPatrolPoint();
            TrySetTargetPosition();
        }
    }

    protected override Ray GetRay()
    {
        Ray ray = new(_potentialTargetPosition + Vector3.up * _offset, Vector3.down);
        return ray;
    }
}
