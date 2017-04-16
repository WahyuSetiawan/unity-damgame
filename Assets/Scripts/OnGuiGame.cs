using UnityEngine;
using System.Collections;

public class OnGuiGame : MonoBehaviour {
	

	void OnGUI(){
		GUI.Box(new Rect(10,10,100, 90), "Loader Menu");

		if (GUI.Button(new Rect(20,40,80,20), "Level 1")){

		}

		if (GUI.Button(new Rect(20,40,80,20), "Level 2")){
			
		}

	}

}
