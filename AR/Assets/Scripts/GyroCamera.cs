using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GyroCamera : MonoBehaviour {
	public const float SCALE = 20000;

	private PlayerTracker playerTracker;
    private float initialYAngle = 0f;
    private float appliedGyroYAngle = 0f;
    private float calibrationYAngle = 0f;

    public Button resetButton;
    
    public Text displayCoords;
 	private float lastTime;
 	public GameObject monsterPrefab;

    void Start () {
    	playerTracker = this.GetComponent<PlayerTracker>();
    	resetButton.onClick.AddListener(ResetGyro);
    	Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        initialYAngle = transform.eulerAngles.y;
        displayCoords.text = "aqui iran las coords";
        lastTime = Time.time;
    }
 
    void Update() {
        ApplyGyroRotation();
        ApplyCalibration();
        // UpdatePositionPlayer();
        if (playerTracker.coordenadas != null) {
        	displayCoords.text = playerTracker.coordenadas.lat + ", " + playerTracker.coordenadas.lng;
        }
    }

    void UpdatePositionPlayer() {
    	var crds = playerTracker.getRelativasPrimeras();
    	if (crds != null) {
    		transform.position = new Vector3(
    			(float) crds.lat * SCALE,
    			(float) crds.lng * SCALE,
    			transform.position.z);
    	} else {
    		Debug.Log("null");
    	}
    }
 
    void ResetGyro() {
        calibrationYAngle = appliedGyroYAngle - initialYAngle; // Offsets the y angle in case it wasn't 0 at edit time.
    }

    public void hideGyro() {
    	transform.Rotate( 0f, 0f, 180f, Space.Self );
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
}