using UnityEngine;

public class TransitionField : GravityField
{
    [System.NonSerialized] public Vector3 gravityVector;
    
    public GameObject gravityOrigin;

    private void Start()
    {
        fieldType = FieldType.transition;
        gravityVector = -transform.up * gravityStrength;
    }

    private void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (collider != null && meshFilter != null)
        {
            Mesh mesh = meshFilter.sharedMesh;

            Vector3 size = collider.size;
            Gizmos.color = new Color(100, 0, 100, 0.2f);
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation, size);

            // draw
            Gizmos.color = new Color(100, 0, 100, 0.8f);
            Gizmos.DrawMesh(mesh, gravityOrigin.transform.position, transform.rotation, new Vector3(1f, 0.2f, 0.2f));
        }

    }
}
