using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getSushiData : MonoBehaviour {
	public Text NamaSushi;
	public Text HargaSushi;
	public Text Informasi;
	public Button upBtn;
	public Button downBtn;
	public Button Order;
	public Button ClosePanel;
	public Text jumlah;
	public int jumlahOrder = 1;
	public GameObject canvasPanel;
	public Button cekOrder;
	public string sushiName;
	// Use this for initialization
	string CreateUserURL = "localhost:8080/SushiResto/insertOrderTemp.php";
	IEnumerator Start () {
		string jenisSushi = sushiName;
		WWW itemsData = new WWW ("localhost:8080/SushiResto/getSushi.php?kodeSushi=" + jenisSushi);
		yield return itemsData;
		string itemsDataString = itemsData.text;
		print (itemsDataString);

		NamaSushi.text = GetDataValue(itemsDataString,"NamaSushi:");
		HargaSushi.text = "Rp " + GetDataValue(itemsDataString,"HargaSushi:");
		Informasi.text = GetDataValue(itemsDataString,"Info:");
		print (GetDataValue(itemsDataString,"Nama:"));
		upBtn.onClick.AddListener(() => addQty());
		downBtn.onClick.AddListener (() => subQty ());
		Order.onClick.AddListener (() => orderSushi ());
		ClosePanel.onClick.AddListener(() => exitPanel());
	//	cekOrder.onClick.AddListener (() => lihatOrder ());
	}

	string GetDataValue(string data, string index){
		string value =	data.Substring (data.IndexOf (index) + index.Length);
		if(value.Contains("|"))value = value.Remove(value.IndexOf("|"));
		return value;
	}

	public void Update(){
		//upOrder ();
	}

	void addQty(){
		jumlahOrder += 1;
		jumlah.text = jumlahOrder.ToString();
	}

	void subQty(){
		if (jumlahOrder == 1) {
			jumlahOrder = 1;
		} else {
			jumlahOrder -= 1;
		}
		jumlah.text = jumlahOrder.ToString();
	}
		

	void orderSushi(){
		string username = PlayerPrefs.GetString ("namaUser"); 
		WWWForm form = new WWWForm ();
		form.AddField("usernamePost", username);
		form.AddField ("pesananPost", NamaSushi.text);
		form.AddField ("jumlahSushiPost", jumlahOrder);
		WWW www = new WWW (CreateUserURL, form);
		canvasPanel.gameObject.SetActive(false);
		cekOrder.gameObject.SetActive (true);

	}

	void exitPanel(){
		Debug.Log("Button Triggered");
		canvasPanel.gameObject.SetActive(false);
		cekOrder.gameObject.SetActive(true);
	}


}
