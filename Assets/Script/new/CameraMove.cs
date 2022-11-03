using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public GameObject player;
    public float pLearp = .02f;
    public float rLearp = .01f;

    public Vector3 distance = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + distance;
        transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, rLearp);
    }
}
