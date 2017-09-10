using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
public class DeactivateColorCorrectionCurve : MonoBehaviour {

    public Camera fpsCam;// for putting off color correction curve when player leaves the house


    //using couch wall.fbx as box collider
    void OnTriggerEnter(Collider other)
    {
        fpsCam.GetComponent<ColorCorrectionCurves>().enabled = true;
    }
    void OnTriggerExit(Collider other)
    {
        fpsCam.GetComponent<ColorCorrectionCurves>().enabled = false;
    }
}
