using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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

    // Camera Zoom De�i�kenleri
    public float zoomSpeed = 100f; //zoom h�z�
    public float minZoom = 5f; // min zoom
    public float maxZoom = 50f; //max Zoom
    private float currentZoom; // Anl�k Zoom Seviyesi


    private void Start()
    {
        moveButton.onClick.AddListener(MoveToCamera);
        currentZoom = Vector3.Distance(transform.position, target.position);
    }

    void MoveToCamera()
    {
        EventSystem.current.SetSelectedGameObject(null); // UI Focus'u kald�r

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
                     .OnComplete(() =>
                     {

                         canRotate = true;
                         currentZoom = Vector3.Distance(transform.position, newCameraPosition.position); // Ge�i� tamamland���nda d�n��� aktif et
                     });
        }
        else
        {
            //Kameray� Eski Pozisyonuna geri getir.
            transform.DOMove(savedPosition, transitionTime);
            transform.DORotate(savedRotation.eulerAngles, transitionTime)
            .OnComplete(() =>
            {
                canRotate = false;
                currentZoom = Vector3.Distance(transform.position, savedPosition); // zoomu g�ncelle

            });

        }

    }


    void Update()
    {
        CameraRotationProcess();
        CameraZoomProcess();
    }

    private void CameraZoomProcess()
    {
        float scroll = Mouse.current.scroll.ReadValue().y; //Mouse scroll yukar� a�a�� alg�lama
        

        if (scroll != 0f)
        {
            Debug.Log("Mouse Scroll Alg�land�: " + scroll);

            currentZoom -= scroll * zoomSpeed* Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            //Vector3 targetPoint = canRotate ? newCameraPosition.position : target.position;
           // Vector3 direction = (transform.position - targetPoint).normalized;
            transform.position += transform.forward * (scroll * zoomSpeed * Time.deltaTime);





            ////kameran�n anl�k uzakl���n� g�ncelle
            //currentZoom -= scroll * zoomSpeed;

            ////Zoom'u belirlenen s�n�rlar i�inde tutar
            //if (currentZoom < minZoom)
            //{
            //    currentZoom = minZoom;
            //}
            //else if (currentZoom > maxZoom)
            //{
            //    currentZoom += maxZoom;
            //}

            ////Kameran�n hangi noktaya g�re zoom yapaca��
            //Vector3 targetPoint;

            //if(canRotate) //yeni konuma ge�tiyse
            //{
            //    targetPoint = newCameraPosition.position;
            //}
            //else //ilk konumdaysa
            //{
            //    targetPoint = target.position;
            //}

            ////Kameray� yeni uzakl�kta konumland�r
            //Vector3 direction = (transform.position - targetPoint).normalized; //yon belirleme
            //transform.position = targetPoint + direction * currentZoom;





        }

    }



    private void CameraRotationProcess()
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


