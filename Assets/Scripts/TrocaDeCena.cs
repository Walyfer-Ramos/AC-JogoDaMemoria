using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeCena : MonoBehaviour
{
    public string cena = "";

    public void TrocarCena()
    {
        SceneManager.LoadScene(cena);
    }
}
