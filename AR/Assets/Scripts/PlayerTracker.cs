using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerTracker : MonoBehaviour {
    private IEnumerator coroutine;
    private int id = 0;

    [HideInInspector]
    public Coordenadas coordenadas = null;
    [HideInInspector]
 	public Coordenadas primeras = null; 
    [HideInInspector]
 	public Step[] steps = null;
 	[HideInInspector]
 	public int currentStepIdx = -1;

    public GameObject[] monstruos;

 	public Coordenadas getRelativasPrimeras() {
 		if (primeras != null && coordenadas != null) {
 			return new Coordenadas(
 				coordenadas.lat - primeras.lat,
 				coordenadas.lng - primeras.lng);
 		}
 		return null;
 	}

    void Start() {
        coroutine = WaitAndGet(30.0f);
        StartCoroutine(coroutine);
        StartCoroutine(GetSteps());
    }

    // every 2 seconds perform the print()
    private IEnumerator WaitAndGet(float waitTime) {
        while (true) {
            StartCoroutine(Get());
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator Get() {
        UnityWebRequest www = UnityWebRequest.Get(
        	"http://hunction2018.herokuapp.com/clients/bc:3d:85:23:4a:29"); //94:65:2d:62:72:eb");
    	yield return www.SendWebRequest();

    	if (www.isNetworkError) {
    		Debug.Log("Network error: " + www.error);
    	}
    	if (www.isHttpError) {
    		Debug.Log("Http error: " + www.error);
    	}

    	string sJason = www.downloadHandler.text;
    	Debug.Log(sJason);
    	coordenadas = JsonUtility.FromJson<Coordenadas>(sJason);
    	if (coordenadas != null) {
        	Debug.Log("New coords: " + coordenadas.lat + ", " + coordenadas.lng);
        	if (primeras == null) {
        		primeras = coordenadas;
        	}
        }
    }

    private IEnumerator GetSteps() {
    	UnityWebRequest www = UnityWebRequest.Get(
        	"http://hunction2018.herokuapp.com/products");
    	yield return www.SendWebRequest();

    	if (www.isNetworkError) {
    		Debug.Log("Network error: " + www.error);
    	}
    	if (www.isHttpError) {
    		Debug.Log("Http error: " + www.error);
    	}

    	string sJason = www.downloadHandler.text;
    	Debug.Log("List " + sJason);
    	var tempSteps = PlayerTracker.getJsonArray<Step>(sJason);
    	if (tempSteps != null) {
        	Debug.Log("Got " + tempSteps.Length + " steps");
        	steps = tempSteps;
        	currentStepIdx = 0;
        } else {
        	Debug.Log("tempSteps is null");
        }
    }

    public static T[] getJsonArray<T>(string json) {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
        return wrapper.array;
    }
 
    [System.Serializable]
    private class Wrapper<T> {
        public T[] array;
    }
}
