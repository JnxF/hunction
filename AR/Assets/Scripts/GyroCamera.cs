using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GyroCamera : MonoBehaviour {
	public const float SCALE = 20000;

    public GameObject cubo;
    public GameObject world;
    public GameObject youWon;

    private Cubo setaCubo;
	private PlayerTracker playerTracker;
    private float initialYAngle = 0f;
    private float appliedGyroYAngle = 0f;
    private float calibrationYAngle = 0f;

    public Button resetButton;
    
    public Text displayCoords;
 	private float lastTime;

    private enum States {WaitingForSteps, WaitingToArrive, Fighting, Finished};
    private States state = States.WaitingForSteps;

    void Start () {
    	playerTracker = this.GetComponent<PlayerTracker>();
    	resetButton.onClick.AddListener(ResetGyro);
    	Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        initialYAngle = transform.eulerAngles.y;
        displayCoords.text = "aqui iran las coords";
        lastTime = Time.time;
        setaCubo = world.GetComponent<Cubo>();
    }
 
    void Update() {
        ApplyGyroRotation();
        ApplyCalibration();
        if (playerTracker.coordenadas != null) {
        	displayCoords.text = playerTracker.coordenadas.lat + ", " + playerTracker.coordenadas.lng;
        }
        switch(state) {
            case States.Fighting:
                UpdateFighting();
                break;
            case States.WaitingToArrive:
                UpdateWaitingToArrive();
                break;
            case States.WaitingForSteps:
                UpdateWaitingForSteps();
                break;
        }
    }

    void UpdateWaitingForSteps() {
        if (playerTracker.currentStepIdx >= 0) {
            this.state = States.WaitingToArrive;
            Debug.Log("WaitingForSteps -> WaitingToArrive");
        }
    }

    void UpdateWaitingToArrive() {
        if (playerTracker.currentStepIdx < playerTracker.steps.Length) {
            var currentStep = playerTracker.steps[playerTracker.currentStepIdx];
            var currentPosition = playerTracker.coordenadas;
            if (currentStep != null && currentPosition != null) {
                // TODO: Calcular distancia entre ambas y ver si has llegado!
                var meters = distance(
                    currentStep.lat,
                    currentStep.lng,
                    currentPosition.lat,
                    currentPosition.lng,
                    'K') * 1000.0;
                Debug.Log("Distance: " + meters);
                if ( meters < 1) {
                    SpawnNewCube();
                    this.state = States.Fighting;
                    Debug.Log("WaitingToArrive -> Fighting");
                }
            }
        } else {
            Debug.Log(playerTracker.steps.Length + " take " + playerTracker.currentStepIdx);
        }
    }

    void SpawnNewCube() {
        //cube.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.z);
        // camera.GetComponent<GyroCamera>().spawnSeta();
        cubo.transform.RotateAround(Vector3.zero, Vector3.up, 180);
        setaCubo.health = 100;
        world.SetActive(true);
    }

    void UpdateFighting() {
        if (!cubo.activeInHierarchy) {
            playerTracker.currentStepIdx++;
            if (playerTracker.currentStepIdx < playerTracker.steps.Length) {
                this.state = States.WaitingToArrive;
            } else {
                this.state = States.Finished;
                youWon.SetActive(true);
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