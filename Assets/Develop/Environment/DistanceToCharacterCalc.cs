using UnityEngine;

public class DistanceToCharacterCalc
{
    private Character _character;

    public DistanceToCharacterCalc(Character character)
    {
        _character = character;
    }

    public float Distance(Vector3 position)
    {
        return (_character.CurrentPosition - position).magnitude;
    }
}
