using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GyroCamera : MonoBehaviour {
	public const float SCALE = 20000;

    public GameObject cubo;
    public GameObject world;
    public GameObject youWon;
    public Text distanceText;
    public Text stateText;
    public Text texts;
    public GameObject textsPanel;
    public Material setaOriginalMaterial;

    private float timeHideText;

    private Cubo setaCubo;
	private PlayerTracker playerTracker;
    private float initialYAngle = 0f;
    private float appliedGyroYAngle = 0f;
    private float calibrationYAngle = 0f;
    private float timeToDie;

    public Button resetButton;
    
    public Text displayCoords;
 	private float lastTime;

    public enum States {WaitingForSteps, WaitingToArrive, Fighting, Dying, Finished};
    private States state = States.WaitingForSteps;

    public States GetState() {
        return state;
    }

    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    	playerTracker = this.GetComponent<PlayerTracker>();
    	resetButton.onClick.AddListener(ResetGyro);
    	Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        initialYAngle = transform.eulerAngles.y;
        displayCoords.text = "aqui iran las coords";
        lastTime = Time.time;
        setaCubo = world.GetComponent<Cubo>();
    }

    public void Skip() {
        if (state == States.WaitingToArrive) {
            SpawnNewCube();
            this.state = States.Fighting;
            distanceText.gameObject.SetActive(false);
            ShowText("Fight!");
            Debug.Log("WaitingToArrive -> Fighting");
        }
    }

    public void Restart() {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ShowText(string text) {
        texts.text = text;
        timeHideText = Time.time + 2.0f;
        textsPanel.SetActive(true);
        texts.gameObject.SetActive(true);
    }
 
    void Update() {
        ApplyGyroRotation();
        ApplyCalibration();

        //Debug.Log("THT . T " + timeHideText + " - " + Time.time);
        if (Time.time > timeHideText && texts.gameObject.activeInHierarchy) {
            texts.gameObject.SetActive(false);
            textsPanel.SetActive(false);
        }

        if (playerTracker.coordenadas != null) {
        	displayCoords.text = playerTracker.coordenadas.lat + ", " + playerTracker.coordenadas.lng;
        }

        switch(state) {
            default:
                stateText.text = "Ended";
                break;
            case States.Dying:
                stateText.text = "Dying";
                UpdateDying();
                break;
            case States.Fighting:
                stateText.text = "Fighting";
                UpdateFighting();
                break;
            case States.WaitingToArrive:
                stateText.text = "WTA";
                UpdateWaitingToArrive();
                break;
            case States.WaitingForSteps:
                stateText.text = "WFS";
                UpdateWaitingForSteps();
                break;
        }
    }

    void UpdateWaitingForSteps() {
        if (playerTracker.currentStepIdx >= 0) {
            this.state = States.WaitingToArrive;
            distanceText.gameObject.SetActive(true);
            if (playerTracker.currentStepIdx < playerTracker.steps.Length) {
                var currentStep = playerTracker.steps[playerTracker.currentStepIdx];
                ShowText("Next monster in: " + currentStep.placename);
            }
            Debug.Log("WaitingForSteps -> WaitingToArrive");
        }
    }

    void UpdateWaitingToArrive() {
        if (playerTracker.currentStepIdx < playerTracker.steps.Length) {
            var currentStep = playerTracker.steps[playerTracker.currentStepIdx];
            var currentPosition = playerTracker.coordenadas;
            if (currentStep != null && currentPosition != null) {
                var meters = distance(
                    currentStep.lat,
                    currentStep.lng,
                    currentPosition.lat,
                    currentPosition.lng,
                    'K') * 1000.0;
                distanceText.text = "Distance: " + meters;
                if ( meters < 2 ) {
                    SpawnNewCube();
                    this.state = States.Fighting;
                    distanceText.gameObject.SetActive(false);
                    ShowText("Fight!");
                    Debug.Log("WaitingToArrive -> Fighting");
                }
            }
        } else {
            Debug.Log(playerTracker.steps.Length + " take " + playerTracker.currentStepIdx);
        }
    }

    void SpawnNewCube() {
        cubo.transform.RotateAround(Vector3.zero, Vector3.up, 180);
        cubo.GetComponentInChildren<SkinnedMeshRenderer>().material = setaOriginalMaterial;
        setaCubo.health = 100;
        world.SetActive(true);
    }

    void UpdateFighting() {
        if (setaCubo.health <= 0) {
            timeToDie = Time.time + 3f;
            this.state = States.Dying;
            Debug.Log("Fighting -> Dying");
        }
    }

    void UpdateDying() {
        if (Time.time >= timeToDie) {
            playerTracker.currentStepIdx++;
            world.SetActive(false);
            if (playerTracker.currentStepIdx < playerTracker.steps.Length) {
                distanceText.gameObject.SetActive(true);
                this.state = States.WaitingToArrive;
                if (playerTracker.currentStepIdx < playerTracker.steps.Length) {
                    var nextStep = playerTracker.steps[playerTracker.currentStepIdx];
                    ShowText("Next monster in: " + nextStep.placename);
                }
                Debug.Log("Dying -> WaitingToArrive");
            } else {
                this.state = States.Finished;
                youWon.SetActive(true);
                Debug.Log("Dying -> FInished");
            }
        }
    }
 
    void ResetGyro() {
        calibrationYAngle = appliedGyroYAngle - initialYAngle; // Offsets the y angle in case it wasn't 0 at edit time.
    }

    public void hideGyro() {
        initialYAngle = transform.eulerAngles.y;
        //transform.Rotate( 90f, 180f, 0f, Space.World);
    	//calibrationYAngle = appliedGyroYAngle - initialYAngle;
    }
   
    void ApplyGyroRotation()  {
        transform.rotation = Input.gyro.attitude;
        transform.Rotate( 0f, 0f, 180f, Space.Self ); // Swap "handedness" of quaternion from gyro.
        transform.Rotate( 90f, 180f, 0f, Space.World ); // Rotate to make sense as a camera pointing out the back of your device.
        appliedGyroYAngle = transform.eulerAngles.y; // Save the angle around y axis for use in calibration.
    }
 
    void ApplyCalibration() {
        transform.Rotate( 0f, -calibrationYAngle, 0f, Space.World ); // Rotates y angle back however much it deviated when calibrationYAngle was saved.
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //:::                                                                         :::
    //:::  This routine calculates the distance between two points (given the     :::
    //:::  latitude/longitude of those points). It is being used to calculate     :::
    //:::  the distance between two locations using GeoDataSource(TM) products    :::
    //:::                                                                         :::
    //:::  Definitions:                                                           :::
    //:::    South latitudes are negative, east longitudes are positive           :::
    //:::                                                                         :::
    //:::  Passed to function:                                                    :::
    //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
    //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
    //:::    unit = the unit you desire for results                               :::
    //:::           where: 'M' is statute miles (default)                         :::
    //:::                  'K' is kilometers                                      :::
    //:::                  'N' is nautical miles                                  :::
    //:::                                                                         :::
    //:::  Worldwide cities and other features databases with latitude longitude  :::
    //:::  are available at https://www.geodatasource.com                         :::
    //:::                                                                         :::
    //:::  For enquiries, please contact sales@geodatasource.com                  :::
    //:::                                                                         :::
    //:::  Official Web site: https://www.geodatasource.com                       :::
    //:::                                                                         :::
    //:::           GeoDataSource.com (C) All Rights Reserved 2018                :::
    //:::                                                                         :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private double distance(double lat1, double lon1, double lat2, double lon2, char unit) {
      if ((lat1 == lat2) && (lon1 == lon2)) {
        return 0;
      }
      else {
        double theta = lon1 - lon2;
        double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515;
        if (unit == 'K') {
          dist = dist * 1.609344;
        } else if (unit == 'N') {
          dist = dist * 0.8684;
        }
        return (dist);
      }
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts decimal degrees to radians             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double deg2rad(double deg) {
      return (deg * Math.PI / 180.0);
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts radians to decimal degrees             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double rad2deg(double rad) {
      return (rad / Math.PI * 180.0);
    }
}