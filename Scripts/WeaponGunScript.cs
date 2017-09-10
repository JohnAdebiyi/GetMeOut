using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


using System;
using System.Text;
using UnityEngine.Windows.Speech;
//get rid of this error if occurs: UnityEditor.AsyncHTTPClient:Done(State, Int32)
//Edit -> Preferences -> Show Asset Store Search Hits -> disable checkbox

public class WeaponGunScript : MonoBehaviour {


    public float damage = 1f; // damage inflicted to the enemy
    public float range = 10f; // distance that can be fired to
    public float impactForce = 30f;

    [Space]
    [Space]

    public float maxAmmo = 10f;
    public float currentAmmo;
    public float declineAmmo = 1f;
    public float reloadTime = 3f;
    private bool isReloading = false;

    [Space]
    [Space]

    [SerializeField]
    private string[] m_Keywords;
    private KeywordRecognizer m_Recognizer;

    [Space]
    [Space]

    public Text ammoText;
    public Text reloadingText;//reloading text

    [Space]
    [Space]

    public ParticleSystem projectile;//particle effect projectile /lasers or afterburners or etc....
    public GameObject impactEffect;//enemy has to have a box collider// impact effect(particle effect Shockwave) after enemy is hit with a raycast 
    [Space]
    [Space]

    public Camera fpsCam;// used to shoot a Raycast from the camera
    public Image bar;

    private Animator _animator;
    private Animator weaponGun_Bob;

    public GameObject isDead_Panel;

    public GameObject gunReloading;
    public Renderer rendGun;

    //enable speech and weapon functions only when weapon gets activated
    void Awake()
    {
        m_Recognizer = new KeywordRecognizer(m_Keywords);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();

        _animator = GetComponent<Animator>();
        weaponGun_Bob = GetComponent<Animator>();

        currentAmmo = maxAmmo;
        ammoText.text = "Ammo: " + maxAmmo.ToString();
        reloadingText.enabled = false;

        rendGun = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //-------------------------------------------------Weapon Bob
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            weaponGun_Bob.Play("WeaponGun_BobVertical");
            weaponGun_Bob.speed = 1f;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))//onrelease
        {
            weaponGun_Bob.speed = 0f;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            weaponGun_Bob.Play("WeaponGun_BobHorizontal");
            weaponGun_Bob.speed = 1f;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))//onrelease
        {
            weaponGun_Bob.speed = 0f;
        }
        //------------------------------------------------------


        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            ammoBar();
            ammoText.text = "Ammo: " + currentAmmo.ToString();

            // if is reloading then stop other actions
            if (isReloading)
                return;
            //if ammo is zero or below then stop other actions and reload
            if (currentAmmo <= 0)
            {

                StartCoroutine(Reload());
                return;
            }

            if (Input.GetButtonDown("Fire1"))//left mouse
            {
                shoot();
                _animator.Play("WeaponGun_moveBack", -1, 0f);
                FindObjectOfType<SFX_Manager>().Play("laserSound");

            }
            if (Input.GetMouseButtonDown(2) && (currentAmmo != maxAmmo))//middle mouse
            {
                Debug.Log("Pressed middle click.");
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator Reload()
    {

        isReloading = true; //set isReloading to true so other actions are stopped
        ammoText.enabled = false;//disable the ammotext so the reloading text can be seen
        reloadingText.enabled = true; // show reloading text
        FindObjectOfType<SFX_Manager>().Play("reloading");

        //_animator.Play("WeaponGun_ReloadingStart", -1, 0f);
        gunReloading.SetActive(true);
        rendGun.enabled = false;

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;// fill ammo
        
        rendGun.enabled = true;
        gunReloading.SetActive(false);
        //_animator.Play("WeaponGun_ReloadingStop", -1, 0f);

        FindObjectOfType<SFX_Manager>().Stop("reloading");
        reloadingText.enabled = false;// disable reloading text
        ammoText.enabled = true;// enable the ammotext
        isReloading = false;//set isReloading to false so other actions can be done
    }


    void shoot()
    {

            currentAmmo -= declineAmmo;
            int mask = (9 << LayerMask.NameToLayer("enemy")); // hit only this layer(object) with the raycast
            projectile.Play();
            RaycastHit hit; //store information about what we hit with out ray // used for the enemy
            if (isDead_Panel.gameObject.activeInHierarchy == true)
            {
                mask = ~1 << 9;// if the dead panel is activ dont let the raycast hit the enemy..ignoring layer 9
            }
            //to shoot out a ray
            //fpsCam.transform.position -> shoot out a ray starting at the position of our camera
            //fpsCam.transform.position -> shoot the ray in the direction we are facing
            //out hit -> gather information of what we hit and put it in the RaycastHit hit variable
            //range -> if objects are further away than the range unit, we arent able to hit them
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, mask))// this physics works only the enemy because of the mask
            {
                //show the name of the object that has been hit in the console
                // Debug.Log(hit.transform.name);

                // subtract amount from the enemys health
                EnemyHealthScript enemy = hit.transform.GetComponent<EnemyHealthScript>();
                if (hit.collider.tag == "enemy")
                //if(enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                //if enemy is hit, start the chase
                EnemyAIScript _enemy = hit.transform.GetComponent<EnemyAIScript>();
                if (_enemy != null)
                {
                    _enemy.CheckSight();
                }

                //push an object with a rigid body backwards when hit
                //-hit.normal -> push backwards
                if (hit.rigidbody != null)//if an object has a rigid body 
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                //if an object is hit, show the shockwave
                //hit.point -> point of impact
                //Quaternion.LookRotation(hit.normal) -> dont understand
                GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;

                //destroy every instanitaed object after 2 seconds. This is done so that the unity hierarchy  
                //doesnt get full with instantiated objects and also to make sure memory does not get full
                Destroy(impactGameObject, 2f);


                Shielded_EnemyScript shieldedEnemy = hit.transform.GetComponent<Shielded_EnemyScript>();
                if (hit.collider.tag == "shielded_enemy")
                {
                    shieldedEnemy.shield(); // if this specific enemy is hit, activate his shield
                    shieldedEnemy.TakeDamageShield(damage);// damage his shield
                }
            }
            //if raycast hits other objects like walls, stones, etc show impact effect.  ...impactEffect_Shockwave play On Awake checkbox has to be activ
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
                Destroy(impactGameObject, 2f);
            }
        
    }


    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (args.text == m_Keywords[0]) // keyword "shoot" to fire
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
            builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
            builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
            Debug.Log(builder.ToString());


            // if is reloading then stop other actions
            if (isReloading)
                return;
            //if ammo is zero or below then stop other actions and reload
            if (currentAmmo <= 0)
            {

                StartCoroutine(Reload());
                return;
            }

            shoot();
            _animator.Play("WeaponGun_moveBack", -1, 0f);
            FindObjectOfType<SFX_Manager>().Play("laserSound");

        }

        if (args.text == m_Keywords[1]) //keyword "charge" to reload ammo
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
            builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
            builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
            Debug.Log(builder.ToString());

            StartCoroutine(Reload());
        }
    }



    //Image bar
    void ammoBar()
    {
        bar.fillAmount = currentAmmo / maxAmmo; // procent value from 0 - 1. example -  60/100 = 0,6% on the bar      
    }

}
