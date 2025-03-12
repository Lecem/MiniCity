using TMPro;
using UnityEngine;

public class HighlightBox : MonoBehaviour
{
    private MeshRenderer lastObject; // En son etkileþime girilen obje
    public string buildingName; //binanýn adýný tutacak
    public GameObject nameUI; //world canvas UI objesini tutacak.

    private TextMeshProUGUI nameText;

    public float yHigh = 2f;

    void Start()
    {
         lastObject = GetComponent<MeshRenderer>();
        lastObject.enabled = false;   

        

    }

    private void OnMouseEnter()
    {
        Debug.Log(gameObject + "objeye gelindi");

        if (nameUI == null )
        {
            Debug.LogError(gameObject.name + "için nameUI atanmadý ki! ");
            return; //hata varsa yine de fonksiyondan çýk
        }

        nameUI.SetActive(true); //UI ý görünür yapar.
       
        nameUI.GetComponent<TextMeshProUGUI>().text = buildingName; //UI ögesine binanýn adýný koyar.


        //UI ý bina üstüne taþý
        nameUI.transform.position = transform.position + Vector3.up * yHigh;

        //UI kameraya baktýrýr
        nameUI.transform.LookAt(Camera.main.transform);
        nameUI.transform.Rotate(0, 180, 0);


        lastObject.enabled = true;

    }


    void OnMouseExit() 
    {
        Debug.Log(gameObject + "objeden çýkýldý");

        if(nameUI != null) //nameUI doluysa
        {
            nameUI.SetActive(false);
        }
        lastObject.enabled = false;
    }


}
