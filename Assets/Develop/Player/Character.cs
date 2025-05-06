using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
{
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;
    private Health _health;
    private Explodable _explodable;

    [SerializeField] private CharacterView _characterView;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _timeBeforePatrolMax;
    [SerializeField] private float _radiusOfPatrol;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _woundPercent;

    private BehaviorSelecter _behaviorSelecter;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Vector3 CurrentPosition => transform.position;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public int HP => _health.HP;
    public IExplodable Explodable => _explodable;
    public Health Health => _health;

    public void Initialize(TargetPointView targetPointView, ControllersTypes controller, NavigatorTypes navigator)
    {
        _health = new Health(_characterView, _maxHealth, _woundPercent);
        _behaviorSelecter = new BehaviorSelecter(this, targetPointView, controller, navigator);
        _explodable = new Explodable(_health, transform);
        
        _mover = new DirectionalMover(GetComponent<CharacterController>(), _moveSpeed);
        _mover.AddModifier(new HealthSpeedModifier(_health));

        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }
        
    private void Update()
    { 
        _behaviorSelecter.Update(Time.deltaTime);
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
}
