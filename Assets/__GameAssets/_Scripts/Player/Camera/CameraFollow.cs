using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakterin transformu
    public float smoothSpeed = 0.125f;  // Kamera hareketinin yumu�akl���
    public Vector3 offset;    // Karakter ile kamera aras�ndaki ofset (uzakl�k)

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
    }
}
