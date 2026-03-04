using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform muzzleTransform;
    public Camera cam;
    private Vector3 mousePos;
    private Vector3 gunDirection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        gunDirection = (mousePos - transform.position).normalized;
        float angle=Mathf.Atan2(gunDirection.y,gunDirection.x)*Mathf.Rad2Deg;
        if (angle <= 90 && angle >= -90)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else if (angle > 90 || angle < -90)
        {
            transform.eulerAngles = new Vector3(-180, 0, -angle);
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("鼠标左键点击");
            Instantiate(bullet, muzzleTransform.position, Quaternion.Euler(transform.eulerAngles));
        }
    }
}
