using UnityEngine;

/** Animation Bool States
 * isRunning   !!not used
 * isIdle      !!not used
 * isRunHit
 * isIdleHit
 * isWalking
 * isWalkHit
 * */



public class EnemyAIScript : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent nav;   
     
    public Transform player;//FPSController
    public Transform eyesFront;// A cone with a disabled mesh renderer is used for the front eye(Radar)
    public Transform eyesFrontLeft;// A cone with a disabled mesh renderer is used for the front left eye(Radar)
    public Transform eyesFrontRight;// A cone with a disabled mesh renderer is used for the front right eye(Radar)
    public Transform eyesBehindRight;// A cone with a disabled mesh renderer is used for the back right eye(Radar)
    public Transform eyesBehindFront;// A cone with a disabled mesh renderer is used for the back eye(Radar)

    public GameObject miniMapArrow; //for changing the color mini map arrow when player gets near enemy
    public GameObject miniMapArrowRing;//for changing the color mini map ring when player gets near enemy

    public GameObject crossHair_InRangeGreen;// for displaying a bigger crosshair when player get near the enemy
    public GameObject crossHair_InRangeBlack;// for displaying a bigger crosshair when player get near the enemy

    private PlayerStatusScript playerStatus;

    public GameObject rock_CaveDoor1;//close the cave door so player cant run back to the house
    public GameObject rock_CaveDoor2;//close the cave door so player cant enter the second cave

    public GameObject pointerToEnemy_Red;//for changing the color
    public GameObject pointerToEnemy_Yellow;//for changing the color

    private float showTime;//For Debugging. used to show how much time has passed in the console
    private string state = "chooseA_SpotToWalk";
    public static bool isOutside = false; // the enemies are activated when the player takes the weapon. This is set in WeaponInSafeScript.cs
    private float wait;//when player out runs the enemy, the enemy chases the player for some time and looses interest
    //private float standStill;// when enemy reaches path position, enemy should stay still for a certain amount of time and move after

    private float wait2;//wait a certain amount of seconds to damage the player and repeat
    private float damagePlayer = 0.05f;// 0.05 * 100 = 5 damage units


    private float nearAttack_distance;//when enemy gets near the player 
    private float inFrontAttack_distance;//when enemy is in front of player
    private float outOfSight;//when player out runs the enemy, the enemy looses interest
    private float showCrossHair_distance; // when player is 10 units from enemy, crosshair is shown

    public AudioClip[] footsounds;
    public AudioClip[] hitsounds;
    private AudioSource sound;

    private float navSpeed = 6.7f;



    void Start()
    {
        sound = GetComponent<AudioSource>();
        playerStatus = GetComponent<PlayerStatusScript>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 1.2f;     //enemy speed
        //anim.speed = 1.2f;
    }

    //footstep sounds
    public void FootStep(int _num)
    {
       sound.clip = footsounds[_num];
       sound.Play();
    }
    //punch sounds
    public void HitSound(int _num)
    {
        sound.clip = hitsounds[_num];
        sound.Play();
    }

    //check if we can see the player
    //obstacles(crystal and rocks) Layers in the Inspector are set to ignore Raycast so the enemy can see through them 

    public void CheckSight()
    {

        if (isOutside == true)
        {

            RaycastHit rayHit;
            if (Physics.Linecast(eyesFront.position, player.transform.position, out rayHit))
            {
                //print("hit" + rayHit.collider.gameObject.name); //show the name of the objects that were hit with the raycast


                if (rayHit.collider.gameObject.name == "FPSController")
                {
                    
                    rock_CaveDoor1.SetActive(true);//close the rock door to the apartment
                    rock_CaveDoor2.SetActive(true);//close the rock door to the second cave


                    // if state variable hasnt been initiated with "inPlainSight", which it hasnt, then overwrite state variable with "chase". 
                    // This means any other state which is activ will be overwritten with state = "chase" in the moment raycast collides with the player
                    if (state != "inPlainSight")
                    {
                        state = "chase";
                        nav.speed = navSpeed;
                        //anim.speed = 3.7f;
                        wait = 10f;
                        FindObjectOfType<SFX_Manager>().Play("enemyChase");
                    }
                }
            }
            RaycastHit rayHit1;
            if (Physics.Linecast(eyesFrontLeft.position, player.transform.position, out rayHit1))
            {
                //print("hit" + rayHit.collider.gameObject.name); //show the name of the objects that were hit with the raycast


                if (rayHit.collider.gameObject.name == "FPSController")
                {

                    // if state variable hasnt been initiated with "inPlainSight", which it hasnt, then overwrite state variable with "chase". 
                    // This means any other state which is activ will be overwritten with state = "chase" in the moment raycast collides with the player
                    if (state != "inPlainSight")
                    {
                        state = "chase";
                        nav.speed = navSpeed;
                        //anim.speed = 3.7f;
                        wait = 10f;
                        FindObjectOfType<SFX_Manager>().Play("enemyChase");
                    }
                }
            }
            RaycastHit rayHit2;
            if (Physics.Linecast(eyesFrontRight.position, player.transform.position, out rayHit2))
            {
                //print("hit" + rayHit.collider.gameObject.name); //show the name of the objects that were hit with the raycast


                if (rayHit.collider.gameObject.name == "FPSController")
                {

                    // if state variable hasnt been initiated with "inPlainSight", which it hasnt, then overwrite state variable with "chase". 
                    // This means any other state which is activ will be overwritten with state = "chase" in the moment raycast collides with the player
                    if (state != "inPlainSight")
                    {
                        state = "chase";
                        nav.speed = navSpeed;
                        //anim.speed = 3.7f;
                        wait = 10f;
                        FindObjectOfType<SFX_Manager>().Play("enemyChase");

                    }
                }
                RaycastHit rayHit3;
                if (Physics.Linecast(eyesBehindRight.position, player.transform.position, out rayHit3))
                {
                    //print("hit" + rayHit.collider.gameObject.name); //show the name of the objects that were hit with the raycast


                    if (rayHit.collider.gameObject.name == "FPSController")
                    {

                        // if state variable hasnt been initiated with "inPlainSight", which it hasnt, then overwrite state variable with "chase". 
                        // This means any other state which is activ will be overwritten with state = "chase" in the moment raycast collides with the player
                        if (state != "inPlainSight")
                        {
                            state = "chase";
                            nav.speed = navSpeed;
                            //anim.speed = 3.7f;
                            wait = 10f;
                            FindObjectOfType<SFX_Manager>().Play("enemyChase");
                        }
                    }
                }
                RaycastHit rayHit4;
                if (Physics.Linecast(eyesBehindFront.position, player.transform.position, out rayHit4))
                {
                    //print("hit" + rayHit.collider.gameObject.name); //show the name of the objects that were hit with the raycast


                    if (rayHit.collider.gameObject.name == "FPSController")
                    {

                        // if state variable hasnt been initiated with "inPlainSight", which it hasnt, then overwrite state variable with "chase". 
                        // This means any other state which is activ will be overwritten with state = "chase" in the moment raycast collides with the player
                        if (state != "inPlainSight")
                        {
                            state = "chase";
                            nav.speed = navSpeed;
                           // anim.speed = 3.7f;
                            wait = 10f;
                            FindObjectOfType<SFX_Manager>().Play("enemyChase");
                        }
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        Debug.DrawLine(eyesFront.position, player.transform.position, Color.red);//draws a line in the scene window to show distance between player and enemy
        if (isOutside == true)
        {

            //walks randomly
            //chooses a spot to walk to
            if (state == "chooseA_SpotToWalk")
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
                //pick a random place to walk
                Vector3 randomPos = Random.insideUnitCircle * 20f;//finding the random point in a certain radius of space
                NavMeshHit navHit;
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);//Find a random position to walk to from the starting point in a radius of 20
                nav.SetDestination(navHit.position);
                //standStill = 3f;
                state = "chooseAnotherSpot";
                
            }

            //chooses another spot to walk to 
            if (state == "chooseAnotherSpot")
            {
                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    //Debug.Log("arrived");
                   state = "chooseA_SpotToWalk";
                   anim.SetBool("isWalking", false);
                   anim.SetBool("isIdle", true);



                     /** not working properly                   

                    // when enemy reaches path position, enemy should stay still for a certain amount of time and move after
                    if (standStill > 0)
                    {
                     anim.SetBool("isWalking", false);
                     anim.SetBool("isIdle", true);
                        standStill -= Time.deltaTime;
                    }else
                    {
                        anim.SetBool("isIdle", false);
                        anim.SetBool("isWalking", true);
                        state = "chooseA_SpotToWalk";
                    }
                    **/
                }
            }

            //chase-------------------------------------------------------------------------------------------
            if (state == "chase")
            {
                MiniMap_ChangeIconColor changeArrow = miniMapArrow.GetComponent<MiniMap_ChangeIconColor>();
                MiniMap_ChangeIconColor changeRing = miniMapArrowRing.GetComponent<MiniMap_ChangeIconColor>();
                changeArrow.changeMiniMapColor = true; // change color of minimapArrow icon to red. 
                changeRing.changeMiniMapColor = true; // change color of minimapRing icon to red

                pointerToEnemy_Red.SetActive(true);
                pointerToEnemy_Yellow.SetActive(false);

                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
                nav.destination = player.transform.position;

                //when player out runs the enemy, the enemy looses interest
                //if player is(20 units away) out of the enemys field for a certain time of seconds the enemy goes back patrolling
                outOfSight = Vector3.Distance(transform.position, player.transform.position);
                if (outOfSight > 20)
                {
                    //Debug.Log("Player is out of enemies sight");
                    if (wait > 0f)
                    {
                        showTime += Time.deltaTime;
                        //Debug.Log(showTime);
                        wait -= Time.deltaTime;
                    }
                    else
                    {

                        changeArrow.changeMiniMapColor = false; // change color of minimapArrow icon back to yellow
                        changeRing.changeMiniMapColor = false; // change color of minimapRing icon back to yellow

                        pointerToEnemy_Red.SetActive(false);
                        pointerToEnemy_Yellow.SetActive(true);

                        showTime = 0;
                        //Debug.Log("Enemy has stopped chasing you");
                        nav.speed = 1.2f;
                        //anim.speed = 1.2f;
                        state = "chooseAnotherSpot";
                    }
                }

                //if player is 11 units away from the enemy show Crosshair - the range of the gun fire is 10 units
                showCrossHair_distance = Vector3.Distance(transform.position, player.transform.position);
                if (showCrossHair_distance < 11)
                {
                    crossHair_InRangeBlack.SetActive(true);
                    crossHair_InRangeGreen.SetActive(true);
                }
                else
                {
                    crossHair_InRangeBlack.SetActive(false);
                    crossHair_InRangeGreen.SetActive(false);
                }

                //(5 units away)
                //if enemy gets near the player start the walk hit animation
                nearAttack_distance = Vector3.Distance(transform.position, player.transform.position);
                if (nearAttack_distance < 5)
                {
                    anim.SetBool("isWalkHit", true);
                    anim.SetBool("isWalking", false);
                    if (nearAttack_distance < 4)// if enemy gets in front of the player
                    {
                        state = "inFrontPlayer";
                    }
                }
                else
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isWalkHit", false);
                }


            }

            //(4 units away) 
            //when enemy is in front of player start the idle hit animation and damage the player
            if (state == "inFrontPlayer")
            {
                inFrontAttack_distance = Vector3.Distance(transform.position, player.transform.position);
                if (inFrontAttack_distance < 4)
                {
                   // Debug.Log("damage");
                    anim.SetBool("isWalkHit", false);
                    anim.SetBool("isIdleHit", true);

                    //wait a certain amount of seconds to damage the player and repeat 
                    if (wait2 > 0)
                    {
                        wait2 -= Time.deltaTime;
                    }
                    else
                    {
                        playerStatus.GetDamage(damagePlayer);// damage the player  
                        wait2 = 0.5f;
                    }

                }
                else
                {
                    anim.SetBool("isWalkHit", true);
                    anim.SetBool("isIdleHit", false);
                    state = "chase";
                    
                }
            }
            // nav.SetDestination(player.transform.position);
        }
    }
}


