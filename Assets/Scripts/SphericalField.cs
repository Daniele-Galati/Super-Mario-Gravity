using UnityEngine;

public class SphericalField : GravityField
{
    private void Start()
    {
        fieldType = FieldType.spherical;
    }

    private void OnDrawGizmos()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        if (collider != null)
        {
            float radius = collider.radius;
            Gizmos.color = new Color(100, 0, 0, 0.2f);
            Gizmos.DrawSphere(transform.position, radius);
        }

    }
}
