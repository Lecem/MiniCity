using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CameraController : MonoBehaviour
{
    public Transform target; //hangi objenin etraf�nda d�nece�imiz.
    public float rotationSpeed = 5.0f; // d�n�� h�z� 
    public Button moveButton; 

    
    public Transform newCameraPosition; // Kamera ge�i�i i�in hedef konum
    public float transitionTime = 2.0f; // Ge�i� s�resi
    public float selfRotationSpeed = 10f; // Kendi etraf�nda d�nme h�z�
    private bool canRotate; //default b�rak�nca false oluyormu�.Yeni konuma ge�tiyse true oluyor.

    private Vector3 savedPosition;
    private Quaternion savedRotation;

    private void Start()
    {
        moveButton.onClick.AddListener(MoveToCamera);
    }

    void MoveToCamera()
    {
        //    // Kameray� yeni konuma ve rotasyona ta��
        //    transform.DOMove(newCameraPosition.position, transitionTime);
        //    transform.DORotate(newCameraPosition.eulerAngles, transitionTime)
        //             .OnComplete(StartSelfRotation); // Ge�i� bitince kendi etraf�nda d�nmeye ba�la
        //
        if (!canRotate)
        {
            savedPosition = transform.position;
            savedRotation = transform.rotation;


            // Kameray� yeni konuma ve rotasyona ta��
            transform.DOMove(newCameraPosition.position, transitionTime);
            transform.DORotate(newCameraPosition.eulerAngles, transitionTime)
                     .OnComplete(() => canRotate = true); // Ge�i� tamamland���nda d�n��� aktif et

        }
        else
        {
            //Kameray� Eski Pozisyonuna geri getir.
            transform.DOMove(savedPosition, transitionTime);
                transform.DORotate(savedRotation.eulerAngles, transitionTime)
                .OnComplete(() => canRotate = false);

        }

    }


   

    void Update()
    {
        if (Input.GetMouseButton(0)) // Sol t�k bas�l�yken d�nd�r�r
        {
            float h = Input.GetAxis("Mouse X") * rotationSpeed; //fareyi sa�a sola hareket ettirme de�erini d�nd�r�r.

            if (!canRotate)
            {
                // E�er yeni konuma ge�mediyse hedef obje etraf�nda d�n
                transform.RotateAround(target.position, Vector3.up, h);   //kameray� sa�a sola d�nd�r�r.
            }
            else
            {
                // E�er yeni konuma ge�tiyse kendi etraf�nda d�n
                transform.Rotate(Vector3.up, h, Space.World);
            }
        }
    }
}


