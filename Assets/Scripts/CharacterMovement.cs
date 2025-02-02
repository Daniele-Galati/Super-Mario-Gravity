using UnityEditorInternal;
using UnityEngine;


[RequireComponent (typeof(CharacterInput))]
[RequireComponent (typeof(GravityAgent))]
[RequireComponent (typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    CharacterInput input;
    GravityAgent gravityAgent;
    Rigidbody rb;

    [SerializeField] Camera cam;

    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;

    void Start()
    {
        input = GetComponent<CharacterInput>();
        gravityAgent = GetComponent<GravityAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        // handle input request
        if (input.move.magnitude > 0)
        {
            float xMov = input.move.x;
            float zMov = input.move.y;

            Vector3 planeNormal = gravityAgent.FindNormal(gravityAgent.gravityDirection);

            // project camera forward onto the normal plane
            Vector3 cameraForward = Vector3.ProjectOnPlane(cam.transform.forward, planeNormal);
            Vector3 cameraRight = Vector3.ProjectOnPlane(cam.transform.right, planeNormal);

            // project the input on the underneath surface (normal taken from the gravityAgent)
            Vector3 movementDirection = xMov * cameraRight + zMov * cameraForward;

            movementDirection = Vector3.ProjectOnPlane(movementDirection,planeNormal).normalized;
            rb.AddForce(movementDirection * acceleration, ForceMode.Force);
        }

        // max speed control
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }

    void JumpRequested()
    {

    }
}
