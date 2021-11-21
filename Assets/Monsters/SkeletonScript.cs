using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public GameObject player;
    public float distanceFromPlayer;
    public bool shouldFollow = true;
    public int hitsToDie = 2;
    Animator anim;
    private AudioSource audioSource;
    public AudioClip swordSwingClip;
    public AudioClip deathScreamClip;
    private long lastShotTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("HuaweiARDevice").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("SOUL");
        //Debug.LogError("HERE 22");
        Vector3 LookPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //Debug.LogError("HERE 24");
        gameObject.transform.LookAt(LookPos);

        if (shouldFollow)
        {
            //Debug.LogError("HERE 29");
            transform.position += transform.forward * Time.deltaTime * 0.5f;
            //Debug.LogError("HERE 31");
        }

        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        // Debug.LogError("Distance "+distanceFromPlayer);
        // Debug.LogError("Player x: " + player.transform.position.x + " y: "+ player.transform.position.y +" z: "+ player.transform.position.z);
        if (distanceFromPlayer < 2)
        {
            //Debug.LogError("HERE 38");
            attackPlayer();
            //Debug.LogError("HERE 40");
        }
        else {
            shouldFollow = true;
            anim.SetTrigger("Walk");
        }
    }

    void attackPlayer()
    {
        var now = System.DateTimeOffset.Now.ToUnixTimeSeconds();
        if (now - lastShotTime >= 1)
        {
            lastShotTime = now;
            anim.SetTrigger("Attack");
            audioSource.PlayOneShot(swordSwingClip);

        }
        shouldFollow = false;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFireball")
        {
            hitsToDie -= 1;
            if (hitsToDie <= 0)
            {
                audioSource.PlayOneShot(deathScreamClip);
                Destroy(this.gameObject, 0);
            }
        }
    }

    private void OnDestroy()
    {
        EnemeySpawnerCTRL.decrementNumOfSkeletons();
    }
}