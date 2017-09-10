using UnityEngine;
using System.Diagnostics;

public class IsFinished : MonoBehaviour {


    public void RestartGame()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        Process.Start(Application.dataPath + "/../Get_Me_Out_x86.exe");
        Application.Quit();
    }

    public void ExitGame()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        Application.Quit();
    }
}
