using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InteractionAPI : MonoBehaviour {

    [System.Serializable]
    public class Coordenadas
    {
        public int getObject;
        public float lat;
        public float lng;
        public string mac;
        public string query;
    }

    void Start () {
		//StartCoroutine(communicateHeroku());
    }
 
    void Update () {

    }

    public void sendData(string option) {
        if (option == "coords") {
            StartCoroutine(getCoords());
        }
    }

    IEnumerator getCoords() {
        UnityWebRequest www = UnityWebRequest.Get("http://hunction2018.herokuapp.com/clients/94:65:2d:62:72:eb");
        yield return www.SendWebRequest ();
        string sJason = www.downloadHandler.text;
        Coordenadas coords = JsonUtility.FromJson<Coordenadas>(sJason);
        Debug.Log(coords.lat);
    }

    IEnumerator communicateHeroku(int option) {
		Debug.Log ("qewqeqeqw");
		UnityWebRequest www = UnityWebRequest.Get("http://hunction2018.herokuapp.com/");
		Debug.Log ("Vamos a enviar");
		yield return www.SendWebRequest ();
		Debug.Log ("Fin");
		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Form upload complete!");
		}
		Debug.Log(www.downloadHandler.text);
    }
}