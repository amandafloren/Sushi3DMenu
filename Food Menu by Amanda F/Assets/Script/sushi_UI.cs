using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sushi_UI : MonoBehaviour {

	public void NextScene (string a)
	{
		Application.LoadLevel (a);
	}

	public void PrevScene (string a)
	{
		Application.LoadLevel (a);
	}
		
}

