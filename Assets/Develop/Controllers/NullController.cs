using UnityEngine;

public class NullController : Controller
{
    public override ControllersTypes ControllerType => ControllersTypes.Null;

    protected override Vector3 GetTargetDirection()
    {
        return Vector3.zero;
    }

    protected override void UpdateLogic(float deltaTime)
    {

    }
}
