using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    private Camera ZoomCamera;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 moveDir = Vector3.zero;
        int edgeScrollSize = 20;
        float moveSpeed = 50f;
        float cameraSpeed = 0.5f;

        // AD camera movement
        if (Input.GetKey(KeyCode.A)) moveDir.z = -cameraSpeed;
        if (Input.GetKey(KeyCode.D)) moveDir.z = +cameraSpeed;
        if (Input.GetKey(KeyCode.W)) moveDir.x = -cameraSpeed;
        if (Input.GetKey(KeyCode.S)) moveDir.x = +cameraSpeed;

        if (Input.GetKey(KeyCode.LeftArrow)) moveDir.z = -cameraSpeed;
        if (Input.GetKey(KeyCode.RightArrow)) moveDir.z = +cameraSpeed;
        if (Input.GetKey(KeyCode.UpArrow)) moveDir.x = -cameraSpeed;
        if (Input.GetKey(KeyCode.DownArrow)) moveDir.x = +cameraSpeed;

        // Mouse side camera movement
        if (Input.mousePosition.x < edgeScrollSize) moveDir.z = -cameraSpeed;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) moveDir.z = +cameraSpeed;

        // Smoothly interpolate between current position and target position
        transform.position = Vector3.Lerp(transform.position, transform.position + moveDir, Time.deltaTime * moveSpeed);

        // Camera Zoom
        float zoomSpeed = 10f;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(-(scroll * zoomSpeed), 0, 0, Space.World);

        // Boundaries of the camera movement
        var clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, +13, +30);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -18, +23);
        transform.position = clampedPosition;
    }
}
