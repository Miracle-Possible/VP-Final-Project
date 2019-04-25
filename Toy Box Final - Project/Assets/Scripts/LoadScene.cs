using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void PlayMode()
    {
        SceneManager.LoadScene("Game View");
    }

    public void Editor()
    {
        try
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));

        }
        catch (System.Exception)
        {

            throw;
        }
        SceneManager.LoadScene("Editor");
    }
}
