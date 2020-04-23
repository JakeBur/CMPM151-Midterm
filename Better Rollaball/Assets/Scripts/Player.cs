using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Transform deathPlane;

    public float gravityAngle;
    public float gravityStrength;
    public float turnRate;
    public float groundDistance;

    [HideInInspector] public int score;

    public event Action<int> OnCollect;

    private Vector3 spawnLocation;
    private CameraMaster cam;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InputManager>().onMove += Move;
        GetComponent<InputManager>().onRotate += Rotate;
        spawnLocation = transform.position;

        cam = FindObjectOfType<CameraMaster>();
    }

    private void Update()
    {
        if(transform.position.y < deathPlane.position.y)
        {
            transform.position = spawnLocation;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        IsGrounded();
    }

    public void IsGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance))
        {
            GetComponent<Renderer>().sharedMaterial.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().sharedMaterial.color = Color.white;
        }
    }

    public void Collect()
    {
        score += 1;
        OnCollect?.Invoke(score);
    }

    private void Move(Vector2 inputAxis)
    {
        Quaternion rotation = Quaternion.Euler(gravityAngle * -inputAxis.y, 0, gravityAngle * inputAxis.x);
        GetComponent<Rigidbody>().AddForce(transform.localToWorldMatrix.MultiplyVector(rotation * Vector3.up) * -gravityStrength);
    }

    private void Rotate(Vector2 mouseDelta)
    {
        if (mouseDelta != Vector2.zero)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, transform.eulerAngles + new Vector3(0, mouseDelta.x, 0), turnRate);
        }
    }
}
