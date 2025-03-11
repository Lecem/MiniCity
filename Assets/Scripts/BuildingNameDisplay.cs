using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingNameDisplay : MonoBehaviour
{
    public string buildingName; // bina objesinin adýný tutacak deðiþken
    public GameObject nameUI; // UI objesini tutacak deðiþken
    void OnMouseEnter() //fare objenin üzerine geldiðinde tetiklenir
    {
        if (nameUI == null) // Eðer nameUI boþsa hata vermeden uyarý yazsýn
        {
            Debug.LogError(gameObject.name + " için nameUI atanmadý! Lütfen Inspector'dan bir UI öðesi ekleyin.");
            return; // Hata oluþursa fonksiyondan çýk
        }

        nameUI.SetActive(true); // UI ögesini görünür yapar
        nameUI.GetComponent<TextMeshProUGUI>().text = buildingName; //  UI ögesinin text ine bina adýný yerleþtirir. 
    }

    void OnMouseExit() //fare objeden ayrýldýðýnda tetiklenir. 
    {
        if (nameUI != null) //nameUI þu anda boþ deðilse yani bir þey yazýyorsa
        {
            nameUI.SetActive(false); //UI ögesini gizler.
        }
    }
}
