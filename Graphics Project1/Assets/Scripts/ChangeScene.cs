using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *Author: Jack Dalton
 * Date: 2/12/2017
 * function: changes scenes for button presses.
 */

public class ChangeScene : MonoBehaviour {

	public void changeScene(string NewGame)
	{
		//Load new scene
		SceneManager.LoadScene(NewGame);
	}
}