using UnityEngine;

public class RadialField : GravityField
{
    private void Start()
    {
        fieldType = FieldType.cylinder;
    }

    private void OnDrawGizmos()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

        if (mesh != null)
        {
            Gizmos.color = new Color(0, 0, 100, 0.2f);
            Gizmos.DrawMesh(mesh,transform.position,transform.rotation,transform.localScale);
        }

    }
}
