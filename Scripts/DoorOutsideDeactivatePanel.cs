using UnityEngine;


public class DoorOutsideDeactivatePanel : MonoBehaviour
{

    //Hierarchy --> corridor_wall_goOutside
    //if player enter the trigger
    public GameObject outsideDoorIsOpened;


    //collision with the box collider
    void OnTriggerEnter(Collider other)
    {

        outsideDoorIsOpened.SetActive(false); // deactivates the panel "door to go outside is opened" which was activated in weaponScript.cs => OutsideDoorIsOpened.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
