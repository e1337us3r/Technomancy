using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private readonly string SKELETON_TAG = "SKELETON";
    private readonly string DRAGON_TAG = "DRAGON";
    private readonly int MAX_HP = 5;
    public static readonly float MAX_MANA = 100f;
    public static float mana = 100f;

    private bool isVulnerable = true;

    public static bool gameOver = false;
    public static bool gameWon = false;
    public static int currentHp = 5;

    public static void setMana(float value) { if(value>=0&& value<=100) mana = value; }
    public static int getMana() { return (int)mana; }

    public static void incrementManaBy(float value) { if(mana+value<=100) mana+=value; }

    public static void decrementManaBy(float value) { if (mana - value >= 0) mana -= value; }

    private AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip loseClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("HuaweiARDevice").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && DragonScript.dragonDead) 
        {
            audioSource.PlayOneShot(winClip);
            gameWon = true;
            gameOver = true;
            Debug.Log("Dragon is Dead! Congrats, dude !!");
            Invoke("ResetGameState", 5f);
        }
    }

    private void CheckForDeath() 
    {
        if (!gameOver && currentHp < 1)
        {
            audioSource.PlayOneShot(loseClip);
            gameOver = true;
            Debug.Log("WE DEAD! OMFG !!!!");
            Invoke("ResetGameState", 5f);
        }
    }

    private void ToggleVulnerability() 
    {
        isVulnerable = !isVulnerable;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("got hit");

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.gameObject.tag == SKELETON_TAG)
        {
            Debug.Log("SKELETON HIT US !!!");
        }
        else if (other.gameObject.tag == DRAGON_TAG)
        {
            Debug.Log("DRAGON HIT US !!! MEDIC !!!");
        }

        TryToDamagePlayer();
    }

    public void OnTriggerStay(Collider other)
    {
        TryToDamagePlayer();
    }

    private void TryToDamagePlayer() 
    {
        if (!gameOver && isVulnerable)
        {
            ToggleVulnerability(); // => false; invulnerable
            currentHp -= 1;
            CheckForDeath();

            Invoke("ToggleVulnerability", 2f); // => true; vulnerable
        } else
        {
            Debug.LogError("CANT DAMAGE PLAYERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
        }
    }

    public void ResetGameState()
    {
        Utils.FindAndDestroyAllEnemies();
        gameOver = false;
        gameWon = false;
        currentHp = MAX_HP;
        mana = MAX_MANA;
        isVulnerable = true;
        DragonScript.Reset();
        //TODO call sam's script for recreating enemies
        GameObject.Find("HuaweiARDevice").GetComponent<EnemeySpawnerCTRL>().Restart();
    }
}
