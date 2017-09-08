using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class cekOrder : MonoBehaviour {
	public string json;
	public Text display;
	public GameObject orderListPrefab;
	public GameObject parentPanel;
	public Button ExitPanel2;
	public Button resetBtn;
	public Button confirmBtn;
	public InputField noSeat;

	IEnumerator Start()
	{
		string uname = PlayerPrefs.GetString ("namaUser"); 
		WWW w = new WWW ("http://localhost:8080/SushiResto/getUserOrder.php?nama="+ uname);
		yield return w;
		json = w.text;
		getList();
		ExitPanel2.onClick.AddListener(() => closePanel());
		resetBtn.onClick.AddListener(() => resetOrder ());
		confirmBtn.onClick.AddListener (() => validOrder ());
		Debug.Log (uname);
	}

	[System.Serializable]
	public struct UserOrderList
	{
		public string pesanan;
		public string jumlah;
		public string harga;
	}

	[System.Serializable]
	public class orderData{
		public List<UserOrderList> orderList;

	}

	public void getList () {
		orderData data = JsonUtility.FromJson<orderData>(json);

		while (this.transform.childCount > 0) {
			Transform c = this.transform.GetChild(0);
			c.SetParent (null);
			Destroy (c.gameObject);
		}

		if (json.Contains("KOSONG")) {
			return;
		} else {
			//ini adalah kode yang men-generate prefab pada setiap array yang dimiliki json
			foreach (UserOrderList getData in data.orderList) {
				GameObject listPrefab = (GameObject)Instantiate (orderListPrefab);
				listPrefab.transform.SetParent (parentPanel.transform, false);
				listPrefab.transform.SetParent (this.transform);
				listPrefab.transform.Find ("Nama Sushi").GetComponent<Text> ().text = getData.pesanan;
				listPrefab.transform.Find ("Harga").GetComponent<Text> ().text = getData.harga;
				listPrefab.transform.Find ("Jumlah").GetComponent<Text> ().text = getData.jumlah;
				print (getData.pesanan + " " + getData.jumlah + " " + getData.harga);
			}
		}
	}

	void resetOrder(){
		string username = PlayerPrefs.GetString ("namaUser"); 
		string CreateUserURL1 = "http://localhost:8080/SushiResto/deleteOrder.php?usernamePost=" + username;
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		WWW www = new WWW (CreateUserURL1, form);

		Debug.Log ("Triggered Tes");
		Application.LoadLevel ("checkOrder");
	}

	void closePanel(){
		Application.LoadLevel ("sushiScene1");
		//sushiRaycast.gameObject.SetActive(true);
		Debug.Log ("Triggered Close");
	}

	void validOrder(){
		string user = PlayerPrefs.GetString ("namaUser");
		string containerSeat = noSeat.text;
		string CreateUserURL2 = "http://localhost:8080/SushiResto/insertOrderValid.php?nama=" + user + "&seat=" + containerSeat;
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", user);
		WWW www = new WWW (CreateUserURL2, form);

		Debug.Log ("trigger valid");
		Application.LoadLevel ("sushiScene1");
		
	}


}
