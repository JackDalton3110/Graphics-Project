using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Change : MonoBehaviour
{

	public void change(string NewGame)
    {
        SceneManager.LoadScene(NewGame);
    }
}
