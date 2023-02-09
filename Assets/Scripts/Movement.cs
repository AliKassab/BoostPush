using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float thrust = 800f;
    [SerializeField] float rotationThrust = 75f;
    [SerializeField] ParticleSystem exhaustUp;
    [SerializeField] ParticleSystem exhaustDown;
    [SerializeField] ParticleSystem RotateOne;
    [SerializeField] ParticleSystem RotateTwo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W))
        {
            startThrustingUp();
        }
        else if(Input.GetKey(KeyCode.S))
        {
            startThrustingDown();
        }
        else
        {
            exhaustUp.Stop();
            exhaustDown.Stop();

        }
    }

    void startThrustingDown()
    {
        rb.AddRelativeForce(Vector3.down * thrust * (3 / 4) * Time.deltaTime);
        if (!exhaustDown.isPlaying)
            exhaustDown.Play();
    }

    void startThrustingUp()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!exhaustUp.isPlaying)
            exhaustUp.Play();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotateRight();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotateLeft();
        }
        else
        {
            RotateTwo.Stop();
            RotateOne.Stop();
        }
    }

    void rotateLeft()
    {
        RotationController(-rotationThrust);
        if (!RotateOne.isPlaying)
            RotateOne.Play();
    }

    void rotateRight()
    {
        RotationController(rotationThrust);
        if (!RotateTwo.isPlaying)
            RotateTwo.Play();
    }

    void RotationController(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation for taking rotation control
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation
    }
}
