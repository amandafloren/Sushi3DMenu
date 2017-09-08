using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void resetOrder(){
		string username = PlayerPrefs.GetString ("namaUser"); 
		string CreateUserURL = "http://192.168.0.14:8080/sushiresto/deleteorder.php?usernamePost=" + username;
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		WWW www = new WWW (CreateUserURL, form);

		Debug.Log ("Triggered Tes");
		Application.LoadLevel ("sushiScene1");
	}

	public void closePanel(){
		Application.LoadLevel ("sushiScene1");
		//sushiRaycast.gameObject.SetActive(true);
		Debug.Log ("Triggered Close");
	}
}
