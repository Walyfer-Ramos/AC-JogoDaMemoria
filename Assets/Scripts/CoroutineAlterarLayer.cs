using UnityEngine;
using System.Collections;
// using UnityEngine.UI;

public class CoroutineAlterarLayer : MonoBehaviour
{
    public GameObject imagemUI;

    void Start()
    {
        if(imagemUI != null)
        {
            imagemUI.SetActive(true);
            StartCoroutine(EsconderImagemCortina(6f));
        }
    }

    IEnumerator EsconderImagemCortina(float tempo)
    {
        if(imagemUI != null)
        {
           yield return new WaitForSeconds(tempo);
           imagemUI.SetActive(false); 
        }
    }
}
