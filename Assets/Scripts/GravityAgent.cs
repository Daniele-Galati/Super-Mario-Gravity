using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(GameObject))]
public class GravityAgent : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    private List<GravityField> touchingFields = new List<GravityField>();
    private GravityField priorityField;
    private Rigidbody rb;

    [System.NonSerialized] public Vector3 gravityDirection = Vector3.down;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        gravityDirection = CalculateGravityVector(priorityField);
        rb.AddForce(gravityDirection * rb.mass);

        if (priorityField != null)
            RotateTowardsGravity(gravityDirection, priorityField);
    }

    #region Trigger collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GravityField>() != null)
        {
            touchingFields.Add(other.gameObject.GetComponent<GravityField>());
            priorityField = DeterminePriorityField();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // this prevents not following gravity if the player spawns inside a field

        GravityField otherField = other.GetComponent<GravityField>();
        if (otherField != null && !touchingFields.Contains(otherField))
            touchingFields.Add(otherField);


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GravityField>() != null)
        {
            touchingFields.Remove(other.gameObject.GetComponent<GravityField>());
            priorityField = DeterminePriorityField();
        }
    }
    #endregion

    private GravityField DeterminePriorityField()
    {
        // determines which field has higher priority,
        // preventing the object to be attracted by the "strongest" force field in case of collider overlaps.

        int highestPriority = -1;
        GravityField highestPriorityField = null;

        foreach (GravityField field in touchingFields)
        {
            if (field.priority >= highestPriority)
            {
                highestPriority = field.priority;
                highestPriorityField = field;
            }
        }

        return highestPriorityField;
    }

    private Vector3 CalculateGravityVector(GravityField field)
    {
        // calculates both direction and magnitude, based upon the gravityStrength variable in the GravityField class

        if (field != null)
        {
            switch (field.fieldType)
            {
                case FieldType.spherical:
                    Vector3 direction = field.transform.position - transform.position;
                    return direction.normalized * field.gravityStrength;
                case FieldType.directional:
                    return field.GetComponent<DirectionalField>().gravityVector;
                case FieldType.cylinder:
                    Vector3 radialDir = Vector3.ProjectOnPlane(field.transform.position - transform.position, field.transform.up);
                    return radialDir;
                case FieldType.transition:
                    // get direction from the player to the gravity origin, projected on a plane that is perpendicular to the surface normal
                    GameObject gravityOrigin = field.GetComponent<TransitionField>().gravityOrigin;
                    Vector3 transitionDir = Vector3.ProjectOnPlane(-gravityOrigin.transform.position + transform.position,gravityOrigin.transform.right);
                    return transitionDir;

            }
            
        }

        return Vector3.zero;
    }

    private void RotateTowardsGravity(Vector3 gravityDir, GravityField field)
    {
        Vector3 currentUp = transform.up;
        Vector3 upDir = -gravityDir;

        // only if the field is not a cylinder or a directional type try to find the plane normal
        // (because in these types the player MUST orientate towards the gravity direction only, not the normal)
        if (field.fieldType != FieldType.cylinder && field.fieldType != FieldType.directional)
            upDir = FindNormal(gravityDir);

        // smooth rotation towards local up (plane normal only if there is a plane underneath)
        Quaternion targetRotation = Quaternion.FromToRotation(currentUp, upDir) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    public Vector3 FindNormal(Vector3 gravityDir)
    {
        // get underneath surface normal
        RaycastHit hit;
        Physics.Raycast(transform.position, gravityDir, out hit, 5f);


        if (hit.collider != null)
        {
            Vector3 planeNormal = hit.normal;
            return planeNormal;
        }

        return -gravityDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, gravityDirection);
    }
}
