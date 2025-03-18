using TMPro;
using UnityEngine;

public class HighlightBox : MonoBehaviour
{
    private MeshRenderer lastObject; // En son etkile�ime girilen obje
    public string buildingName; //binan�n ad�n� tutacak
    public GameObject nameUI; //world canvas UI objesini tutacak.
    public bool isMouseOver;

    private TextMeshProUGUI nameText;

    public float yHigh = 2f;

    void Start()
    {
         lastObject = GetComponent<MeshRenderer>();
        lastObject.enabled = false;   
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
        Debug.Log(gameObject + "objeye gelindi");

        if (nameUI == null )
        {
            Debug.LogError(gameObject.name + "i�in nameUI atanmad� ki! ");
            return; //hata varsa yine de fonksiyondan ��k
        }

        nameUI.SetActive(true); //UI � g�r�n�r yapar.
       
        nameUI.GetComponent<TextMeshProUGUI>().text = buildingName; //UI �gesine binan�n ad�n� koyar.


        //UI � bina �st�ne ta��
        nameUI.transform.position = transform.position + Vector3.up * yHigh;


        lastObject.enabled = true;

    }

    private void Update()
    {
        if (isMouseOver)
        {
            //UI kameraya bakt�r�r
            nameUI.transform.LookAt(Camera.main.transform);
            nameUI.transform.Rotate(0, 180, 0);
        }
    }


    void OnMouseExit() 
    {
        isMouseOver = false;
        Debug.Log(gameObject + "objeden ��k�ld�");

        if(nameUI != null) //nameUI doluysa
        {
            nameUI.SetActive(false);
        }
        lastObject.enabled = false;
    }


}
