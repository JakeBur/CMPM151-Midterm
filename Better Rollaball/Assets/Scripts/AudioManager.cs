using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;


public class AudioManager : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        player.OnCollect += CollectObject;
        player.OnImpact += Impact;

        Application.runInBackground = true; //allows unity to update when not in focus
        Application.runInBackground = true; //allows unity to update when not in focus

        // use osc to listen on localhost
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
    }

    // Update is called once per frame
    void Update()
    {
        // send data to osc
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/speed", Mathf.Clamp((player.GetComponent<Rigidbody>().velocity.magnitude - minSpeed) / maxSpeed, 0, 1));
    }

    private void CollectObject(int score)
    {
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/collectable", score);
    }

    private void Impact()
    {
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/impact", "bang");
    }
}
