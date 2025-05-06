using UnityEngine;

public abstract class Controller
{
    private bool _isEnabled;

    public virtual void Enable() => _isEnabled = true;
    public virtual void Disable() => _isEnabled = false;

    public virtual ControllersTypes ControllerType { get; protected set; }

    public void Update(float deltaTime)
    {
        if (_isEnabled == false)
            return;

        UpdateLogic(deltaTime);
    }

    protected abstract Vector3 GetTargetDirection();
    protected abstract void UpdateLogic(float deltaTime);    
}
