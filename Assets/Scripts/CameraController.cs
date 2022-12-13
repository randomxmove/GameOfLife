using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController _instance;

    private Vector3 lastDragPosition;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
            lastDragPosition = Input.mousePosition;
        if (Input.GetMouseButton(2))
        {
            var delta = lastDragPosition - Input.mousePosition;
            transform.Translate(delta * Time.deltaTime * 100f);
            lastDragPosition = Input.mousePosition;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            Camera.main.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * 500;
        }

    }

    public void UpdateCameraPosition(Transform target, int width, int height, float cellSize)
    {
        // ADJUST CAMERA POSITION TO GRID CENTER + OFFSET
        float offset = 100;
        float something = 200; // UPDATE THIS FOR BETTER CAMERA POSITIONING
        Camera.main.transform.position = new Vector3(target.position.x - offset, target.position.y, target.position.z);

        // ADJUST CAMERA DISTANCE THROUGH ORTHOGRAPHIC SIZE
        float orthoSize = (width * height) / (something / cellSize);
        Camera.main.orthographicSize = orthoSize < something ? something : orthoSize;

    }


}
