using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class login : MonoBehaviour {

	public Text inputUsername;
	public Text inputPassword;
	public Button loginBtn;
	string LoginURL = "192.168.0.14:8080/sushiresto/login.php";

	//void OnGUI(){
		
	//}
	// Use this for initialization
void Start () {
		loginBtn.onClick.AddListener (() => StartCoroutine(LoginDB(inputUsername.ToString(), inputPassword.ToString())));
	}
	
	// Update is called once per frame
	//void Update () {
		//if (Input.GetKeyDown (KeyCode.L))
	//	if (loginBtn.onClick == 1)
		//	StartCoroutine(LoginDB(inputUsername.ToString(), inputPassword.ToString()));
	//}

	IEnumerator LoginDB (string username, string password)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("unamePost", username);
		form.AddField ("pwdPost", password);

		WWW www = new WWW (LoginURL, form);
		yield return www;
		Debug.Log (www.text);
	}
		
}
