using UnityEngine;

public class MouseClickController : MoveToPointController
{
    private readonly int _leftMouseButton = 0;

    public MouseClickController(Character character, Navigator navigator, TargetPointView targetPointView)
    {
        _character = character;
        _navigator = navigator;
        _targetPosition = character.CurrentPosition;
        _targetPointView = targetPointView;
        _isMoving = false;
        ControllerType = ControllersTypes.MouseClick;
    }

    public override ControllersTypes ControllerType => ControllersTypes.MouseClick;

    protected override void UpdateLogic(float deltaTime)
    {
        InputHandle();

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
    }

    public void SetNavigator(Navigator navigator)
    {
        _navigator = navigator;
    }

    public bool TryActivate()
    {
        if (TrySetTargetPosition())
            return true;
        
        return false;
    }

    protected override Ray GetRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray;
    }

    private void InputHandle()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            TrySetTargetPosition();
        }
    }

}
