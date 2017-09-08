using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loginController : MonoBehaviour {
	
	//deklarasi variabel
	[SerializeField]
	private InputField userField = null;
	[SerializeField]
	private InputField passField = null;
	[SerializeField]
	private Text feedbackmsg = null;
	[SerializeField]
	private Toggle rememberUser = null;

	private string url = "http://localhost:8080/SushiResto/loginManager.php";
	// Use this for initialization
	void Start () {
		//pengecekkan jika user ingin data loginnya disimpan ke dalam sistem
		if (PlayerPrefs.HasKey ("remember") && PlayerPrefs.GetInt ("remember") == 1) {
			userField.text = PlayerPrefs.GetString ("rememberLogin");
			passField.text = PlayerPrefs.GetString ("rememberPass");
		}

	}

	public void cekLogin(){
		// fungsi untuk melakukan pengecekkan field sudah terisi atau tidak
		if (userField.text == "" || passField.text == "") {
			feedbackFail ("Harap mengisi kolom data");
		} else {
			string username = userField.text;
			string password = passField.text;

			if (rememberUser.isOn) {
				PlayerPrefs.SetInt ("rememberUser", 1);
				PlayerPrefs.SetString ("rememberLogin", username);
				PlayerPrefs.SetString ("rememberPass", password);
			}
			//form untuk mentransfer data ke URL yang dituju
			WWW sendData = new WWW(url +"?login=" + username + "&password=" + password);
			//mengembalikan hasil ketika fungsi yield dipanggil
			StartCoroutine(validateLogin(sendData));
		}
	}

	IEnumerator validateLogin(WWW www){
		yield return www;
		if (www.error == null) {
			string output = www.text;
			//pengecekkan jika hasil data yang dikirimkan berisikan angka 1
			if (output.Contains("1")) {
				feedbackSuccess ("Login berhasil, Harap tunggu...");
				string namaUser = userField.text;
				PlayerPrefs.SetString ("namaUser", namaUser);
				StartCoroutine (changeScene());
			//pengecekkan jika output yang diberikan bukan angka 1
			} else {
				feedbackFail("Username atau Password invalid");
			}
		} else {
			if (www.error == "Tidak dapat terhubung ke host") {
				feedbackFail ("Tidak ada koneksi");
			}
		}
	}

	IEnumerator changeScene(){
		///mengembalikan hasil dari proses pada fungsi startcoroutine
		yield return new WaitForSeconds (5);
		//memanggil scene bernama sushiScene1
		Application.LoadLevel ("sushiScene1");

	}

	//fungsi untuk memberikan teks umpan balik jika user berhasil melakukan login
	void feedbackSuccess(string messages){
		feedbackmsg.CrossFadeAlpha (100f, 0f, false);
		feedbackmsg.color = Color.green;
		feedbackmsg.text = messages;
	}

	//fungsi untuk memberikan teks umpan balik jika user tidak berhasil melakukan login
	void feedbackFail(string messages){
		feedbackmsg.CrossFadeAlpha (100f, 1f, false);
		feedbackmsg.color = Color.red;
		feedbackmsg.text = messages;
		feedbackmsg.CrossFadeAlpha (0f, 2f, false);
		userField.text = "";
		passField.text = "";
	}

	//me-load scene bernama register
	public void regScene(){
		Application.LoadLevel ("register");
	}
}
