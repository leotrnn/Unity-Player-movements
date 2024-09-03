/**
 * Triano L�o
 * 
 * 
 * bas� sur https://www.youtube.com/watch?v=rTfCZcLsS9s&list=PLS7jk2aVN8G6s4KM7TV0EJz8_04KquJbX
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rigidbody;
    public Transform head;
    public Camera camera;

    [Header("Configurations")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;

    [Header("Runtime")]
    Vector3 newVelocity;
    bool isGrounded = false;
    bool isJumping = false;

    void Start()
    {
        Cursor.visible = false;

        // bloque la souris au centre de l'�cran
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);


        // Garde la v�locit� y en rejetant la v�locit� x et z
        newVelocity = Vector3.up * rigidbody.velocity.y;

        // Si on maintient shift, la vitesse prend celle de run sinon walk
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Horizontal prend les touches A et D
        newVelocity.x = Input.GetAxis("Horizontal") * speed;

        // Vertical prend les touches A et S
        newVelocity.z = Input.GetAxis("Vertical") * speed;

        if (isGrounded)
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                newVelocity.y = jumpSpeed;
                isJumping = true;
            }

        // transform.TransformDirection = applique les directions par rapport a l'angle de la cam�ra
        rigidbody.velocity = transform.TransformDirection(newVelocity);

    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        Vector3 e = head.eulerAngles;

        e.x -= Input.GetAxis("Mouse Y") * 2f;
        e.x = RestrictAngle(e.x, -85f, 85f);
        head.eulerAngles = e;
    }

    // Pr�vient la cam�ra de faire des angles pas r�alistes
    public static float RestrictAngle(float angle, float angleMin, float angleMax)
    {
        if(angle > 180)
            angle -= 360;
        else if(angle < -180)
            angle += 360;

        if(angle > angleMax)
            angle = angleMax;
        if ( angle < angleMin)
            angle = angleMin;
        

        return angle;
    }

    private void OnCollisionStay(Collision col)
    {
        isGrounded = true;
        isJumping = false;
    }

    void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }
}
