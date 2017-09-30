using UnityEngine;
using System.Collections;

public class WeaponGun_isReloadingBob : MonoBehaviour
{

    private Animator weaponGun_Bob;

    void Start()
    {
        weaponGun_Bob = GetComponent<Animator>();
    }


    void Update()
    {
        {
            //-------------------------------------------------Weapon Bob
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                weaponGun_Bob.Play("WeaponGunOleinnick_BobVertical");
                weaponGun_Bob.speed = 0.7f;
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))//onrelease
            {
                weaponGun_Bob.speed = 0f;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                weaponGun_Bob.Play("WeaponGunOleinnick_BobHorizontal");
                weaponGun_Bob.speed = 0.7f;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))//onrelease
            {
                weaponGun_Bob.speed = 0f;
            }
            //------------------------------------------------------
        }
    }
}