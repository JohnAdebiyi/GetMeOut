using UnityEngine;


public class PointerArrows : MonoBehaviour {



    public Transform target;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
    }
}
