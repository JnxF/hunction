using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class Coordenadas
{
    public Coordenadas(double lat = 0, double lng = 0) {
        this.lat = lat;
        this.lng = lng; 
    }
    public int getObject;
    public double lat;
    public double lng;
    public string mac;
    public string query;
}

[System.Serializable]
public class Step {
    public int order;
    public double lat;
    public double lng;
    public string placename;
    public Step(double lat=0, double lng=0, int order=1, string placename="") {
        this.order = order;
        this.lat = lat;
        this.lng = lng;
        this.placename = placename;
    }
}

public class InteractionAPI : MonoBehaviour {

    public Coordenadas coordActuales;
    public GameObject Camera;

    void Start () {
		//StartCoroutine(communicateHeroku());
    }
 
    void Update () {

    }

    public Coordenadas getCoordActuales() {
        return coordActuales;
    }
}