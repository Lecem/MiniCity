using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target; //hangi objenin etrafýnda döneceðimiz.
    public float rotationSpeed = 5.0f; // dönüþ hýzý 
    public Button moveButton;


    public Transform newCameraPosition; // Kamera geçiþi için hedef konum
    public float transitionTime = 2.0f; // Geçiþ süresi
    public float selfRotationSpeed = 10f; // Kendi etrafýnda dönme hýzý
    private bool canRotate; //default býrakýnca false oluyormuþ.Yeni konuma geçtiyse true oluyor.

    private Vector3 savedPosition;
    private Quaternion savedRotation;

    // Camera Zoom Deðiþkenleri
    public float zoomSpeed = 100f; //zoom hýzý
    public float minZoom = 5f; // min zoom
    public float maxZoom = 50f; //max Zoom
    private float currentZoom; // Anlýk Zoom Seviyesi


    private void Start()
    {
        moveButton.onClick.AddListener(MoveToCamera);
        currentZoom = Vector3.Distance(transform.position, target.position);
    }

    void MoveToCamera()
    {
        EventSystem.current.SetSelectedGameObject(null); // UI Focus'u kaldýr

        //    // Kamerayý yeni konuma ve rotasyona taþý
        //    transform.DOMove(newCameraPosition.position, transitionTime);
        //    transform.DORotate(newCameraPosition.eulerAngles, transitionTime)
        //             .OnComplete(StartSelfRotation); // Geçiþ bitince kendi etrafýnda dönmeye baþla
        //
        if (!canRotate)
        {
            savedPosition = transform.position;
            savedRotation = transform.rotation;


            // Kamerayý yeni konuma ve rotasyona taþý
            transform.DOMove(newCameraPosition.position, transitionTime);
            transform.DORotate(newCameraPosition.eulerAngles, transitionTime)
                     .OnComplete(() =>
                     {

                         canRotate = true;
                         currentZoom = Vector3.Distance(transform.position, newCameraPosition.position); // Geçiþ tamamlandýðýnda dönüþü aktif et
                     });
        }
        else
        {
            //Kamerayý Eski Pozisyonuna geri getir.
            transform.DOMove(savedPosition, transitionTime);
            transform.DORotate(savedRotation.eulerAngles, transitionTime)
            .OnComplete(() =>
            {
                canRotate = false;
                currentZoom = Vector3.Distance(transform.position, savedPosition); // zoomu güncelle

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
        float scroll = Mouse.current.scroll.ReadValue().y; //Mouse scroll yukarý aþaðý algýlama
        

        if (scroll != 0f)
        {
            Debug.Log("Mouse Scroll Algýlandý: " + scroll);

            currentZoom -= scroll * zoomSpeed* Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            //Vector3 targetPoint = canRotate ? newCameraPosition.position : target.position;
           // Vector3 direction = (transform.position - targetPoint).normalized;
            transform.position += transform.forward * (scroll * zoomSpeed * Time.deltaTime);





            ////kameranýn anlýk uzaklýðýný güncelle
            //currentZoom -= scroll * zoomSpeed;

            ////Zoom'u belirlenen sýnýrlar içinde tutar
            //if (currentZoom < minZoom)
            //{
            //    currentZoom = minZoom;
            //}
            //else if (currentZoom > maxZoom)
            //{
            //    currentZoom += maxZoom;
            //}

            ////Kameranýn hangi noktaya göre zoom yapacaðý
            //Vector3 targetPoint;

            //if(canRotate) //yeni konuma geçtiyse
            //{
            //    targetPoint = newCameraPosition.position;
            //}
            //else //ilk konumdaysa
            //{
            //    targetPoint = target.position;
            //}

            ////Kamerayý yeni uzaklýkta konumlandýr
            //Vector3 direction = (transform.position - targetPoint).normalized; //yon belirleme
            //transform.position = targetPoint + direction * currentZoom;





        }

    }



    private void CameraRotationProcess()
    {
        if (Input.GetMouseButton(0)) // Sol týk basýlýyken döndürür
        {
            float h = Input.GetAxis("Mouse X") * rotationSpeed; //fareyi saða sola hareket ettirme deðerini döndürür.

            if (!canRotate)
            {
                // Eðer yeni konuma geçmediyse hedef obje etrafýnda dön
                transform.RotateAround(target.position, Vector3.up, h);   //kamerayý saða sola döndürür.
            }
            else
            {
                // Eðer yeni konuma geçtiyse kendi etrafýnda dön
                transform.Rotate(Vector3.up, h, Space.World);
            }
        }
    }
}


