using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonScript : MonoBehaviour
{
    public GameObject player;
    public GameObject fireball;

    public Transform mouth;
    public float distanceFromPlayer;
    public bool shouldFollow = false;
    public float timeSinceLastAttack = 0;
    public static int MAX_HP = 5;
    public float attackInterval = 5;
    public static int hitsToDie = MAX_HP;
    Animator anim;
    public static bool dragonDead = false;
    private AudioSource audioSource;
    public AudioClip deathScreamClip;

    public static void Reset()
    {
        hitsToDie = MAX_HP;
        dragonDead = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("HuaweiARDevice").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Fly");
        //InvokeRepeating("shootFireball", 0, 3.0f);
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
        // Debug.LogError("Distance " + distanceFromPlayer);
        // Debug.LogError("Player x: " + player.transform.position.x + " y: " + player.transform.position.y + " z: " + player.transform.position.z);
        
        //TODO: make distance greater for the dragon so that it shoots fireballs from above and far.
        if (distanceFromPlayer < 15)
        {
            //Debug.LogError("HERE 38");
            //Transistion to idle float first then attack player.
            shouldFollow = false;
            anim.SetTrigger("StopFollowingPlayer");
            if (timeSinceLastAttack > attackInterval)
            {
                attackPlayer();
            }
            //attackPlayer();
            //Debug.LogError("HERE 40");
        }
        else
        {
            
            shouldFollow = true;
            //Dragon should trigger fly instead it could do other animations while flying.
            //anim.SetTrigger("Walk"); This is from skeleton dont uncomment.
            anim.SetTrigger("FlyTowardsPlayer");
        }

        timeSinceLastAttack += Time.deltaTime;
    }

    void attackPlayer()
    {
        //When its in distance the dragon needs to fire the fireballs.
        //anim.SetTrigger("Attack"); This is from skeleton dont uncomment.
        anim.SetTrigger("FireAtPlayer");
        shootFireball();
        
        timeSinceLastAttack = 0;

    }

    private void shootFireball()
    {
        mouth.LookAt(player.transform);
        Instantiate(fireball, mouth.position, mouth.rotation);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFireball")
        {
            TakeDamage();
            UpdateHealthBar();
            if (hitsToDie <= 0)
            {
                audioSource.PlayOneShot(deathScreamClip);
                Destroy(this.gameObject, 0);
                dragonDead = true;
            }
        }
    }

    private void TakeDamage()
    {
        hitsToDie -= 1;
        anim.SetTrigger("TakeHit");

    }

    private void UpdateHealthBar()
    {
        
    }

}
