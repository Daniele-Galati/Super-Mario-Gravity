using UnityEngine;

public class DirectionalField : GravityField
{
    public Vector3 gravityVector;

    private void Start()
    {
        fieldType = FieldType.directional;
        gravityVector = -transform.up * gravityStrength;
    }

    private void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            Vector3 size = collider.size;
            Gizmos.color = new Color(0, 100, 0, 0.2f);
            Gizmos.DrawCube(transform.position, size);
            Gizmos.color = new Color(0, 100, 0, 1f);
            Gizmos.DrawLine(transform.position, transform.position - transform.up);
            Gizmos.DrawSphere(transform.position - transform.up, 0.2f);
        }

    }
}
