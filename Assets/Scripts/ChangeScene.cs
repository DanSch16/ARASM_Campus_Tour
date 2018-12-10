using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void changemenu(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void exitapp()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }
}
