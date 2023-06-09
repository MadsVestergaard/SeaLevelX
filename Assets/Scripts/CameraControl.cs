using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
 private float rotationSpeed = 500.0f;
 public Vector3 mouseWorldPosStart;
 public float zoomScale = 500.0f;
 public float zoomMin = 10f;
 public float zoomMax = 8000.0f;

 // Start is called before the first frame update
 //void Start(){}


 // Update is called once per frame
 void Update()
 {
 if (EventSystem.current.currentSelectedGameObject == null)
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Mouse0))
        {
            CamOrbit();
        }

        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            mouseWorldPosStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            Pan();
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
 }


 private void CamOrbit()
 {
 if(Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
 {
 float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
 float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
 transform.Rotate(Vector3.right, -verticalInput);
 transform.Rotate(Vector3.forward, -horizontalInput, Space.World);
 }
 }

 private void Pan()
 {
 if(Input.GetAxis("Mouse Y") !=0 || Input.GetAxis("Mouse X") !=0)
 {
 Vector3 mouseWorldPosDiff = mouseWorldPosStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
 transform.position += mouseWorldPosDiff;
 }
 }

 private void Zoom(float zoomDiff)
 {
 if(zoomDiff != 0)
 {
 mouseWorldPosStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
 Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomDiff * zoomScale, zoomMin, zoomMax);
 Vector3 mouseWorldPosDiff = mouseWorldPosStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
 transform.position += mouseWorldPosDiff;
 }
 }
}