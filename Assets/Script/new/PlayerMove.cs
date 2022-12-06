using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator anim;
    private new Rigidbody rigidbody;

    public float speed = 3f;
    public float runSpeed = 2f;
    public float jumpHeight = 3f;
    public float rotSpeed = 5f;
    public float sensX;
    public float sensY;

    private bool gun = false;
    private bool running = false;
    private bool aiming = false;

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

        Run();
        Gun();
        Aime();
        
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (running)
        {
            transform.Translate(dir * runSpeed * Time.deltaTime);
        } else
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
        camTarget.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //transform.rotation = Quaternion.Euler(0, yRotation, 0);

        SetAnimation();
    }

    void SetAnimation()
    {
        //(dir.z > 0) ? 1f : ((dir.z < 0) ? -1f : dir.z)
        anim.SetFloat("hInput", (dir.x >= 0.7f) ? 1f : ((dir.x <= -0.7f) ? -1f : dir.x));
        anim.SetFloat("vInput", (dir.z >= 0.7f) ? 1f : ((dir.z <= -0.7f) ? -1f : dir.z));
        //orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void Gun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun = !gun;
            anim.SetBool("Gun", gun);
        }
    }

    void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
            anim.SetBool("Running", running);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            anim.SetBool("Running", running);
        }
    }

    void Aime()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            aiming = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aiming = false;
        }

        if (aiming)
        {
            Debug.Log(anim.GetLayerWeight(1));
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
        }
        else
        {
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
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
