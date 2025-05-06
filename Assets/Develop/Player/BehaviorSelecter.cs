using System.Collections.Generic;
using UnityEngine;

public class BehaviorSelecter
{
    private Character _character;
    private Dictionary<ControllersTypes, Controller> _controllers = new Dictionary<ControllersTypes, Controller>();
    private Controller _currentController;
    private Navigator _currentNavigator;
    private ControllerSwitcher _controllerSwitcher;
    private NavigatorSwitcher _navigatorSwitcher;
    private TargetPointView _targetPointView;

    private float _deltaTime;

    private float _lazyTimeBeforePatrol = 5;
    private float _lazyTime;
    private bool _isLazy;
    private readonly int leftMouseButton = 0;

    public BehaviorSelecter(Character character, TargetPointView targetPointView, ControllersTypes currentController, NavigatorTypes currentNavigator)
    {
        _character = character;
        _targetPointView = targetPointView;
        _navigatorSwitcher = new NavigatorSwitcher();
        _currentNavigator = _navigatorSwitcher.SetNavigator(currentNavigator);
        CreateControllers();
        _controllerSwitcher = new ControllerSwitcher(_controllers);
        _currentController = _controllerSwitcher.SetController(currentController);
        _isLazy = true;
        _lazyTime = 0;
    }

    private void CreateControllers()
    {
        _controllers.Add(ControllersTypes.Keyboard, new PlayerCharacterController(_character));
        _controllers.Add(ControllersTypes.MouseClick, new MouseClickController(_character, _currentNavigator, _targetPointView));
        _controllers.Add(ControllersTypes.Patrol, new PatrolController(_character, _currentNavigator, _targetPointView));
        _controllers.Add(ControllersTypes.Null, new NullController());
    }

    public void Update(float deltaTime)
    {
        if (_character.Health.Died)
        {
            _controllerSwitcher.SetController(ControllersTypes.Null);
            return;
        }

        _deltaTime = deltaTime;
        _currentController?.Update(_deltaTime);
        SwitchingBehaviorLogic();
    }

    private void SwitchingBehaviorLogic()
    {
        switch (_currentController.ControllerType)
        {
            case ControllersTypes.MouseClick:
                MouseClickLogic();
                break;
            case ControllersTypes.Patrol:
                PatrolLogic();
                break;
            case ControllersTypes.Null:
                break;
            default:
                break;
        }
    }

    private void PatrolLogic()
    {
        if (Input.GetMouseButtonDown(leftMouseButton))
        {
            MouseClickController controller = (MouseClickController)_controllers[ControllersTypes.MouseClick];

            if (controller.TryActivate())
            {
                _isLazy = true;
                _lazyTime = 0;
                SetBehavior(ControllersTypes.MouseClick);
            }
        }
    }

    private void MouseClickLogic()
    {
        if (_isLazy)
        {
            if (_character.CurrentVelocity.magnitude >= 0.05f)
            {
                _isLazy = false;
                _lazyTime = 0;
                return;
            }

            _lazyTime += _deltaTime;

            if (_lazyTime >= _lazyTimeBeforePatrol)
            {
                SetBehavior(ControllersTypes.Patrol);
            }
        }
        else
        {
            if (_character.CurrentVelocity.magnitude < 0.05f)
            {
                _isLazy = true;
                return;
            }
        }
    }

    private void SetBehavior(ControllersTypes controllerType)
    {
        _currentController = _controllerSwitcher.SetController(controllerType);
    }
}
