using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private TargetPointView _targetPointView;
    [SerializeField] private ControllersTypes _defaultController;
    [SerializeField] private NavigatorTypes _defaultNavigator;

    private void Awake()
    {
        _character.Initialize(_targetPointView, _defaultController, _defaultNavigator);
    }
}

