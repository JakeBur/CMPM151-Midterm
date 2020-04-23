using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaster : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 positionOffset;
    public Transform lookTarget;
    public float lerpStrength;
    public float velocityDeadzone;
    public float rotationRate;

    [HideInInspector] public float rotationAngle;

    private Quaternion rotation;
    private Player player;

    private void Start()
    {
        //FindObjectOfType<InputManager>().onMove;
        player = FindObjectOfType<Player>();
    }

    public void FixedUpdate()
    {
        Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
        if(velocity.magnitude > velocityDeadzone)
        {
            rotationAngle = Mathf.Lerp(rotationAngle, Vector3.SignedAngle(player.transform.forward, velocity, Vector3.up), rotationRate);
            rotation = Quaternion.Euler(0, rotationAngle, 0);
        }

        transform.position = Vector3.Lerp(transform.position, followTarget.position + followTarget.localToWorldMatrix.MultiplyVector(positionOffset), lerpStrength);
        //transform.LookAt(rotation * (lookTarget.position - player.transform.position) + player.transform.position, Vector3.up);
        transform.LookAt(lookTarget);
    }


}
