using UnityEngine;

public class AtivaDesativa : MonoBehaviour
{

public GameObject objetoAtivar;
public GameObject objetoDesativar;
public void Alterar()
    {
        if(objetoAtivar != null)
            objetoAtivar.SetActive(true);

        if(objetoDesativar != null)
            objetoDesativar.SetActive(false);
    }

}
