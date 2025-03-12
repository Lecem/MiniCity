using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

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

    private void Start()
    {
        moveButton.onClick.AddListener(MoveToCamera);
    }

    void MoveToCamera()
    {
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
                     .OnComplete(() => canRotate = true); // Geçiþ tamamlandýðýnda dönüþü aktif et

        }
        else
        {
            //Kamerayý Eski Pozisyonuna geri getir.
            transform.DOMove(savedPosition, transitionTime);
                transform.DORotate(savedRotation.eulerAngles, transitionTime)
                .OnComplete(() => canRotate = false);

        }

    }


   

    void Update()
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


