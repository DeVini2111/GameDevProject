using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class TestCamera : MonoBehaviour

{
    private float zoomSpeed = 2.0f;
    private Camera ZoomCamera;

    private void Start(){
    }


    // Update is called once per frame
    private void Update(){
        Vector3 moveDir = new Vector3(-15.0500002f,2.83999991f,-6.65999985f);
        int edgeScrollSize = 20;
        float moveSpeed = 50f;

        // AD camera movement
        if(Input.GetKey(KeyCode.A)) moveDir.x = -0.5f;
        if(Input.GetKey(KeyCode.D)) moveDir.x = +0.5f;


        // Mouse side camera movement
        if(Input.mousePosition.x < edgeScrollSize) moveDir.x = -0.5f;
        if(Input.mousePosition.x > Screen.width - edgeScrollSize) moveDir.x = +0.5f;

        // Change position
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Boundries of the camera movement
        var move = transform.position;
        move.x = Mathf.Clamp(move.x, +3, +30);
        move.z = Mathf.Clamp(move.z, -18, -12);
        transform.position = move;


        // Camera Zoom
        float zoomSpeed = 10f;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, scroll * zoomSpeed, Space.World);                                    
    }
    
}
