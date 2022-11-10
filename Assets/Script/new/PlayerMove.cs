using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator anim;
    private new Rigidbody rigidbody;

    public float speed = 10f;
    public float jumpHeight = 3f;
    public float rotSpeed = 5f;
    public float sensX;
    public float sensY;

    private Vector3 dir = Vector3.zero;

    public Transform camTarget;
    float xRotation;
    float yRotation;

    private bool ground = false;
    public LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Debug.Log("x " + xRotation);
        Debug.Log("y " + yRotation);

        transform.Translate(dir * speed * Time.deltaTime);
        camTarget.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        SetAnimation();
    }

    void SetAnimation()
    {
        if (dir.x != 0 || dir.z != 0)
        {
            //(dir.z > 0) ? 1f : ((dir.z < 0) ? -1f : dir.z)
            anim.SetFloat("hInput", (dir.x >= 0.7f) ? 1f : ((dir.x <= -0.7f) ? -1f : dir.x));
            anim.SetFloat("vInput", (dir.z >= 0.7f) ? 1f : ((dir.z <= -0.7f) ? -1f : dir.z));
            anim.SetBool("Walking", true);
        } 
        else
        {
            anim.SetBool("Walking", false);
        }
        //orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.4f, layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
    }
}
