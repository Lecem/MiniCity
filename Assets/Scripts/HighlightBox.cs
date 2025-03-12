using TMPro;
using UnityEngine;

public class HighlightBox : MonoBehaviour
{
    private MeshRenderer lastObject; // En son etkile�ime girilen obje
    public string buildingName; //binan�n ad�n� tutacak
    public GameObject nameUI; //UI objesini tutacak.

    void Start()
    {
         lastObject = GetComponent<MeshRenderer>();
        lastObject.enabled = false;   

    }

    private void OnMouseEnter()
    {
        Debug.Log(gameObject + "objeye gelindi");

        if (nameUI == null)
        {
            Debug.LogError(gameObject.name + "i�in nameUI atanmad� ki! ");
            return; //hata varsa yine de fonksiyondan ��k
        }
        nameUI.SetActive(true); //UI � g�r�n�r yapar.
        nameUI.GetComponent<TextMeshProUGUI>().text = buildingName; //UI �gesine binan�n ad�n� koyar.
         lastObject.enabled = true;

    }


    void OnMouseExit() 
    {
        Debug.Log(gameObject + "objeden ��k�ld�");

        if(nameUI != null) //nameUI doluysa
        {
            nameUI.SetActive(false);
        }
        lastObject.enabled = false;
    }


}
