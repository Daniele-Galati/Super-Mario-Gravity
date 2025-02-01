using Unity.VisualScripting;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public int priority = 0;
    public float gravityStrength = 2f;
    [SerializeField] private FieldType fieldType;

    private void OnDrawGizmos()
    {
        switch (fieldType)
        {
            case FieldType.spherical:
                SphereCollider collider = GetComponent<SphereCollider>();
                if (collider != null)
                {
                    float radius = collider.radius;
                    Gizmos.color = new Color(255, 0, 0, 0.2f);
                    Gizmos.DrawSphere(transform.position, radius);
                }
                break;
            case FieldType.transition:
                Debug.Log("Point");
                break;
            case FieldType.area:
                Debug.Log("Area!");
                break;
        }

    }
}

public enum FieldType
{
    spherical,
    area,
    transition
}

