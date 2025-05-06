using UnityEngine;

public class PlayerCharacterController : Controller
{
    private Character _character;

    public PlayerCharacterController(Character character)
    {
        _character = character;
    }

    protected override Vector3 GetTargetDirection()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        return inputDirection;
    }


    protected override void UpdateLogic(float deltaTime)
    {
        _character.SetMoveDirection(GetTargetDirection());
        _character.SetRotationDirection(GetTargetDirection());
    }
}
