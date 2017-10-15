using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Book_BinaryScript : MonoBehaviour {

  //public Image bookBinary;
    public GameObject bookBinary;
    public Camera fpsCam;


    // if raycast hits the open book collider
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "Book_binary")
            {
                //bookBinary.enabled = true;
                bookBinary.SetActive(true);
            }
            if (hit.collider.gameObject.tag == "Game_Laptop")// in case raycast hits the laptop then deactivate the laptop panel
            {
                //bookBinary.enabled = false;
                bookBinary.SetActive(false);
            }
        }
        else
        {
            //bookBinary.enabled=false;
            bookBinary.SetActive(false);
        }
    }
}
