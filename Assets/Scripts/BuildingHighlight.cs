using UnityEngine;

public class BuildingHighlight : MonoBehaviour
{
    private MeshRenderer MeshRend; //objenin meshRendererini saklayan de�i�ken
    private Color originalColor; //objenin ba�lang��taki rengini saklayan de�i�ken 

    void Start()
    {
      
        MeshRend = GetComponent<MeshRenderer>(); //saklayaca�� meshrendereri burada veriyoruz. Objenin materyal rengini burada kontrol ederiz
        if (MeshRend == null)  //e�er meshrendereri yoksa Log atar
        {
            Debug.LogError(gameObject.name + " nesnesinde Meshenderer bile�eni yok!");
            return;
        }
        originalColor = MeshRend.material.color;     // meshrendereri bulursa originalcolor de�i�kenine OBjenin rengini verir.
    }

    void OnMouseEnter()// mouse objenin �zerine geldi�inde
    {
        Debug.Log(gameObject.name + " �st�ne gelindi!"); // Kontrol i�in log
        if (MeshRend != null) //bir meshrenderer varsa 
        {
            Debug.Log("de�i�ti");
            MeshRend.material.color = Color.green; // Rengini yesil yapar
        }       
    }

    void OnMouseExit() //mouse objeden ayr�ld���nda
    {
        Debug.Log(gameObject.name + " �st�nden ��k�ld�!"); // Kontrol i�in log
        if (MeshRend != null)
        {
            MeshRend.material.color = originalColor; // Eski rengine d�nd�r
        }
    }
}
