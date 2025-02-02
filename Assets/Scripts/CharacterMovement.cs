using UnityEditorInternal;
using UnityEngine;


[RequireComponent (typeof(CharacterInput))]
[RequireComponent (typeof(GravityAgent))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterMovement : MonoBehaviour
{
    CharacterInput input;
    GravityAgent gravityAgent;
    Rigidbody rb;

    [SerializeField] Camera cam;

    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask ground;

    float playerHeight;
    Vector3 upDirection;

    void Start()
    {
        input = GetComponent<CharacterInput>();
        gravityAgent = GetComponent<GravityAgent>();
        rb = GetComponent<Rigidbody>();

        playerHeight = GetComponent<CapsuleCollider>().height;
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // handle input request
        if (input.move.magnitude > 0)
        {
            float xMov = input.move.x;
            float zMov = input.move.y;

            upDirection = gravityAgent.FindNormal(gravityAgent.gravityDirection);

            // project camera forward onto the normal plane
            Vector3 cameraForward = Vector3.ProjectOnPlane(cam.transform.forward, upDirection);
            Vector3 cameraRight = Vector3.ProjectOnPlane(cam.transform.right, upDirection);

            // project the input on the underneath surface (normal taken from the gravityAgent)
            Vector3 movementDirection = xMov * cameraRight + zMov * cameraForward;

            movementDirection = Vector3.ProjectOnPlane(movementDirection, upDirection).normalized;
            rb.AddForce(movementDirection * acceleration, ForceMode.Force);
        }

        // max speed control
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }

    public void JumpRequested()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -upDirection,out hit, playerHeight/2 + 0.2f, ground);

        if (hit.collider != null)
            Jump();
    }

    private void Jump()
    {
        rb.AddForce(upDirection.normalized * jumpForce, ForceMode.Impulse);
    }
}
