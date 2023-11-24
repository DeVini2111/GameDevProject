using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class TestCamera : MonoBehaviour

{
    //private float zoomSpeed = 2.0f;
    private Camera ZoomCamera;

    private void Start()
    {
    }


    // Update is called once per frame
    private void Update()
    {
        Vector3 moveDir = new Vector3(0, 0, 0);
        int edgeScrollSize = 20;
        float moveSpeed = 50f;

        // AD camera movement
        if (Input.GetKey(KeyCode.A)) moveDir.z = -0.5f;
        if (Input.GetKey(KeyCode.D)) moveDir.z = +0.5f;


        // Mouse side camera movement
        if (Input.mousePosition.x < edgeScrollSize) moveDir.z = -0.5f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) moveDir.z = +0.5f;

        // Change position
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Boundries of the camera movement
        var move = transform.position;
        move.x = Mathf.Clamp(move.x, +13, +30);
        move.z = Mathf.Clamp(move.z, -18, +22);
        transform.position = move;


        // Camera Zoom
        float zoomSpeed = 10f;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(scroll * zoomSpeed, 0, 0, Space.World);
    }
}