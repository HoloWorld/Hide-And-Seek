//this is the Grid_OptionScript and it inherits from OptionScript

using UnityEngine;
using System.Collections;

public class SelectMapSpotlight : MonoBehaviour{

	private ControlCameraScript CCS; //the Script attached to the camera
	private Light light;

	//on start find the ControlCameraScript and detect other options
	void Start()
	{
		CCS =  GameObject.Find("Main Camera").GetComponent<ControlCameraScript>();
		light = transform.Find("Spotlight").GetComponent<Light>();
	}

	void Update()
	{
		/*
		if (Focus) //this will only be done if the this option is in focus
			LightEnable ();
		else 
			LightDisable ();
		*/
	}

	void LightEnable() {
		light.enabled = true;
	}

	void LightDisable() {
		light.enabled = false;
	}
}