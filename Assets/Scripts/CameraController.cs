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
    private bool canRotate; //default býrakýnca false oluyormuþ.

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

        // Kamerayý yeni konuma ve rotasyona taþý
        transform.DOMove(newCameraPosition.position, transitionTime);
        transform.DORotate(newCameraPosition.eulerAngles, transitionTime)
                 .OnComplete(() => canRotate = true); // Geçiþ tamamlandýðýnda dönüþü aktif et
    

}


    private void StartSelfRotation()
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Sol týk basýlýyken döndürür
        {
            float h = Input.GetAxis("Mouse X") * rotationSpeed; //fareyi saða sola hareket ettirme deðerini döndürür.
           // float v = Input.GetAxis("Mouse Y") * rotationSpeed; //fareyi yukarý aþaðý hareket ettiren deðerini döndürür.
            transform.RotateAround(target.position, Vector3.up, h);   //kamerayý saða sola döndürür.
           // transform.RotateAround(target.position, transform.right, -v); //kamerayý yukarý aþaðý döndürür.
        }
    }
}


