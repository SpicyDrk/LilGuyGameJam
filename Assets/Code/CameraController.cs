using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 startOffset;
    public Transform target;
    public float smoothSpeed = 0.01f;
     
    private Vector3 mousePosition;
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        startOffset = new Vector3(-6.18f, 6.7f, -7.5f);
        transform.position = player.position + startOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            mousePosition = hit.point;
        }
        else
        {
            Debug.Log("No hit");
        }
        
        Vector3 desiredPosition = target.position + startOffset;
        //change desired position to move the camera in the direction of the mouse
        Vector3 mouseOffset = (mousePosition - desiredPosition).normalized * 5f;
        
        desiredPosition += mouseOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}
