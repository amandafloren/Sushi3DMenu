using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class registerController : MonoBehaviour {
	[SerializeField]
	private InputField nameField = null;
	[SerializeField]
	private InputField emailField = null;
	[SerializeField]
	private InputField unameField = null;
	[SerializeField]
	private InputField passField = null;
	// Use this for initialization

	private string url = "http://www.suzuki-jabodetabek.com/SushiResto/register.php";
	void Start () {
		
	}

	public void daftar(){

		if (nameField.text == "" || emailField.text == "" || unameField.text == "" || passField.text == "") {
			//feedbackFail ("Harap mengisi kolom data");
		} else {
			string namaUser = nameField.text;
			string emailUser = emailField.text;
			string username = unameField.text;
			string password = passField.text;

			WWW tes = new WWW(url +"?nama=" + WWW.EscapeURL(namaUser) + "&email=" + WWW.EscapeURL(emailUser) + "&username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password));
			StartCoroutine(validateRegister(tes));
		}
	}

	IEnumerator validateRegister(WWW www){
		yield return www;
		if (www.error == null) {
			//print (www.text);
			string output = www.text;
			//print (output);
			if (output.Contains("Sukses")) {
				StartCoroutine (changeScene());
			} 
		} else {
			if (www.error == "Couldn't connect to host") {
			}
		}
	}

	IEnumerator changeScene(){
		yield return new WaitForSeconds (5);
		Application.LoadLevel ("loginScene");

	}

	public void loginScene(){
		Application.LoadLevel ("loginScene");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
