using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float runnigSpeed = 2.5f;
    public float walkingSpeed = 1f;
    public float strafingSpeed = 1.5f;
    public float mouseSensitivityY = 100;
    private float mouseSensitivityX = 100;
    public float jumpForce = 1;
    public Transform transformCamera;
    private float cameraVerticalRotation = 0f;
    public Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(transform.forward * 5);
        Cursor.lockState = CursorLockMode.Locked;
        
        if (rb == null) 
            rb = GetComponent<Rigidbody>();
        
        if (transformCamera == null)
            transformCamera = Camera.main.GetComponent<Transform>();
      
    }

    // Update is called once per frame
    void Update()
    {
      
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivityX * Time.deltaTime;
       
        cameraVerticalRotation = mouseY;
   
        transformCamera.Rotate(Vector3.right * -(mouseY));
        
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivityY * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
           
            Cursor.visible = false;
        
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            Cursor.visible = true;

        }
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                transform.position += transform.forward * runnigSpeed * Time.deltaTime;
            else
                transform.position += transform.forward * walkingSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * -walkingSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -strafingSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * strafingSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }

    }
}
