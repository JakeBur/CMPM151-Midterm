using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public float rotationRate;
    public float bobSpeed;
    public float bobDistance;

    private Text scoreText;
    private Player player;

    private float initialY;
    private float currentPositionOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        scoreText = FindObjectOfType<Text>();
        initialY = transform.position.y;
        currentPositionOffset = 0;
    }

    private void Update()
    {
        transform.Rotate(transform.worldToLocalMatrix.MultiplyVector(Vector3.up), rotationRate * Time.deltaTime);
        currentPositionOffset += bobSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, initialY + Mathf.Sin(currentPositionOffset % (2 * Mathf.PI)) * bobDistance, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.Collect();
            scoreText.text = "Score: " + player.score;
            Destroy(gameObject);
        }
    }
}
