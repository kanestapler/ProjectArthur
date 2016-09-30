using UnityEngine;
using System.Collections;


public class LevelManager : MonoBehaviour {


	public void LoadMenu(string name)
    {
		UnityEngine.SceneManagement.SceneManager.LoadScene(name) ;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

	public void QuitGame()
	{
		Application.Quit ();
	}
}
