using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		R.user = "_X_";
		R.current_level = 1;
		R.sound = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMenu()
	{

		Application.LoadLevel ("menu");
	}
}
