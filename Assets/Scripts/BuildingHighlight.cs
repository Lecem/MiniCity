using UnityEngine;

public class BuildingHighlight : MonoBehaviour
{
    private MeshRenderer MeshRend; //objenin meshRendererini saklayan deðiþken
    private Color originalColor; //objenin baþlangýçtaki rengini saklayan deðiþken 

    void Start()
    {
      
        MeshRend = GetComponent<MeshRenderer>(); //saklayacaðý meshrendereri burada veriyoruz. Objenin materyal rengini burada kontrol ederiz
        if (MeshRend == null)  //eðer meshrendereri yoksa Log atar
        {
            Debug.LogError(gameObject.name + " nesnesinde Meshenderer bileþeni yok!");
            return;
        }
        originalColor = MeshRend.material.color;     // meshrendereri bulursa originalcolor deðiþkenine OBjenin rengini verir.
    }

    void OnMouseEnter()// mouse objenin üzerine geldiðinde
    {
        Debug.Log(gameObject.name + " üstüne gelindi!"); // Kontrol için log
        if (MeshRend != null) //bir meshrenderer varsa 
        {
            Debug.Log("deðiþti");
            MeshRend.material.color = Color.green; // Rengini yesil yapar
        }       
    }

    void OnMouseExit() //mouse objeden ayrýldýðýnda
    {
        Debug.Log(gameObject.name + " üstünden çýkýldý!"); // Kontrol için log
        if (MeshRend != null)
        {
            MeshRend.material.color = originalColor; // Eski rengine döndür
        }
    }
}
