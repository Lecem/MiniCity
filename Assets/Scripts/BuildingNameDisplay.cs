using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingNameDisplay : MonoBehaviour
{
    public string buildingName; // bina objesinin ad�n� tutacak de�i�ken
    public GameObject nameUI; // UI objesini tutacak de�i�ken
    void OnMouseEnter() //fare objenin �zerine geldi�inde tetiklenir
    {
        if (nameUI == null) // E�er nameUI bo�sa hata vermeden uyar� yazs�n
        {
            Debug.LogError(gameObject.name + " i�in nameUI atanmad�! L�tfen Inspector'dan bir UI ��esi ekleyin.");
            return; // Hata olu�ursa fonksiyondan ��k
        }

        nameUI.SetActive(true); // UI �gesini g�r�n�r yapar
        nameUI.GetComponent<TextMeshProUGUI>().text = buildingName; //  UI �gesinin text ine bina ad�n� yerle�tirir. 
    }

    void OnMouseExit() //fare objeden ayr�ld���nda tetiklenir. 
    {
        if (nameUI != null) //nameUI �u anda bo� de�ilse yani bir �ey yaz�yorsa
        {
            nameUI.SetActive(false); //UI �gesini gizler.
        }
    }
}
