using UnityEngine;


//Enemy healthbar should always position itself to the fps main camera. 
//Goes to EnemyCanvas
public class CameraFacingBillboardScript : MonoBehaviour
{
    public Camera fpsCam;
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - fpsCam.transform.position);
    }
}


