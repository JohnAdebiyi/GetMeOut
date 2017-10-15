using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthScript : MonoBehaviour {

    public float maxHealth = 15f; // health of the enemy
    private float currentHealth = 0f;

    public static int enemiesDead = 0;
    public static int enemiesDeadCounter = 5;

    public Text enemiesCounterText;
    public GameObject enemiesCounterPanel;

    public GameObject destroyedVersion; //using Prefab MonsterNormalAnimFall (Animation) or MonsterNormalNoAnimFall Rigidbody(physics)
    public GameObject displayItem;// for displaying the cross health or paper//prefab

    public GameObject crossHair_InRangeGreen;// for deactivating the cross hair when enemy dies
    public GameObject crossHair_InRangeBlack;// for deactivating the cross hair when enemy dies

    public GameObject pointerToEnemy_Red;//for destroying the pointer
    public GameObject pointerToEnemy_Yellow;//for destroying the pointer

    public GameObject pointerToKeycardTerminal;

    public Image bar;

    public ParticleSystem houseCaveEntranceEff;


    void Start()
    {
        //Image bar - full bar at start
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar();
        if (currentHealth <= 0f)
        {
            Die();
            crossHair_InRangeBlack.SetActive(false);
            crossHair_InRangeGreen.SetActive(false);
            FindObjectOfType<SFX_Manager>().Play("stoneDrop");
        }
    }

    //Image bar
    void healthBar()
    {
        bar.fillAmount = currentHealth / maxHealth; // procent value from 0 - 1. example -  60/100 = 0,6% on the bar      
    }


    void Die()
    {
        Instantiate(displayItem, transform.position, transform.rotation);// display cross health or paper
        Instantiate(destroyedVersion, transform.position, transform.rotation);//display the enemy falling to the ground
        Destroy(gameObject);// destroy the enemy
        pointerToEnemy_Red.SetActive(false);
        pointerToEnemy_Yellow.SetActive(false);
        pointerToEnemy_Red = null;
        pointerToEnemy_Yellow = null;

        enemiesDead += 1;
        Debug.Log("enemies dead: " + enemiesDead);
        //if all enemies are dead, open the rock doors
        if (enemiesDead == 5)
        {
            Debug.Log("dead");
            Destroy(GameObject.FindWithTag("caveDoor1"));
            Destroy(GameObject.FindWithTag("caveDoor2"));

            enemiesCounterPanel.SetActive(false);
            pointerToKeycardTerminal.SetActive(true);

            houseCaveEntranceEff.Play();

            Keypad_BedRoom_SAFE_Script.enemiesAreDead = true;
        }


        enemiesDeadCounter -= 1;
        enemiesCounterText.text = "Enemies: " + enemiesDeadCounter.ToString();//health text

    }
}
