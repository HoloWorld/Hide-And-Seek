//allows us to go from one scene to another

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent (typeof (OptionScript))]
public class GoToScene_OptionAction : OptionAction_WithButton {

	public string SceneName = "";

	// Update is called once per frame
	void Update () 
	{
		try
		{

			if (gameObject.GetComponent<OptionScript>().Focus  && Input.GetButtonDown(Button) )
			{
				SceneManager.LoadScene(SceneName);

				GetComponent<AudioSource>().GetComponent<AudioSource>().PlayOneShot(ActionAudio); 
				//this audio will only be heard if this GameObject has a Object.DontDestroyOnLoad on it.
			}
		}
		catch(UnityException)
		{
			print ("your 'Button' string value is not correct. Go to Edit -> Project Settings -> Input, then change the value to a correct one");
		}
	}
}
