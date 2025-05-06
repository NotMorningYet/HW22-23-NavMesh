using UnityEngine;

public class SimpleNavigator : Navigator
{
    public override Vector3 GetDirection(Vector3 currentPosition, Vector3 targetPosition)
    {
        return (targetPosition - currentPosition);
    }
}
