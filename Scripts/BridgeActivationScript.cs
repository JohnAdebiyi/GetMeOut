using UnityEngine;


public class BridgeActivationScript : MonoBehaviour {

    public GameObject bridge;// activate the bridge
    public GameObject bridgeDoor1;// deactivate the invincible bridge door
    public GameObject bridgeDoor2;// deactivate the invincible bridge door
    public GameObject bridgeDoor3;// deactivate the invincible bridge door
    public GameObject bridgeDoor_4;// deactivate the invincible bridge door
    public GameObject bridgeDoor_5;// deactivate the invincible bridge door
    public GameObject bridgeDoor_6;// deactivate the invincible bridge door
    public GameObject bridgeDoor_7;// deactivate the invincible bridge door

    public GameObject pointerToBridge;
    public GameObject pointerToCave;

    public static bool bridgeIsActiv = false;

	// Update is called once per frame
	void Update ()
    {
	    if (bridgeIsActiv)
        {
            bridge.SetActive(true);
            bridgeDoor1.SetActive(false);
            bridgeDoor2.SetActive(false);
            bridgeDoor3.SetActive(false);
            bridgeDoor_4.SetActive(false);
            bridgeDoor_5.SetActive(false);
            bridgeDoor_6.SetActive(false);
            bridgeDoor_7.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        pointerToBridge.SetActive(false);
        pointerToCave.SetActive(true);
    }
    


}
