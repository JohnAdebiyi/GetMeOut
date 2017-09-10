using UnityEngine;


public class MiniMap_Zoom : MonoBehaviour {
    //used couch wall as box collider for zooming in the apartment!
    //used corridor_tunnelRight as box collider for zooming in the glas tunnel
    //script is on couch wall, corridor_tunnelRight and miniMapArrowGreen GameObject!


    public Camera miniMap;// for zooming in the camera
    public GameObject miniMapArrow_Player;//for making the arrow smaller when zoomed in

    /**
    public float enter_x = 1f;
    public float enter_y = 1f;
    public float enter_z = 1f;

    public float exit_x = 1f;
    public float exit_y = 1f;
    public float exit_z = 1f;
**/


    void OnTriggerEnter(Collider other)
    {
        miniMap.orthographicSize = 15; // to zoom in
        miniMapArrow_Player.transform.localScale = new Vector3(150,150,150);// make arrow smaller
    }
    void OnTriggerExit(Collider other)
    {
        miniMap.orthographicSize = 70;
        miniMapArrow_Player.transform.localScale = new Vector3(450, 450, 450);
    }
}
