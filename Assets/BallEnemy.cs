using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Cylinder").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 LookPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(LookPos);

        transform.position += transform.forward * Time.deltaTime;
        
    }
}
