using UnityEngine;

public class Explodable : IExplodable
{
    private ITakeDamagable _healthComponent;
    private Transform _characterTransform;

    public Explodable(ITakeDamagable healthComponent, Transform characterTransform)
    {
        _healthComponent = healthComponent;
        _characterTransform = characterTransform;
    }

    public void TakeExplosionEffect(Vector3 explosionPosition, float explosionStrength, float explosionRadius, float upwardModifier)
    {
        Debug.Log("Ёффект от взрыва получен");
        Vector3 forceDirection = explosionPosition - _characterTransform.position;
        float distanceToExplosion = forceDirection.magnitude;

        int explosionForce = CalculateForce(distanceToExplosion, explosionStrength, explosionRadius);

        _healthComponent.TakeDamage(explosionForce);
    }

    private int CalculateForce(float distanceToExplosion, float explosionStrength, float explosionradius)
    {
        int force = Mathf.RoundToInt(explosionStrength * (1 - distanceToExplosion / explosionradius));
        Debug.Log($"”рон = {force}");
        return force;
    }
}
