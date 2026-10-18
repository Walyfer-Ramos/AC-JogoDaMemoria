using UnityEngine;
using TMPro;
// using UnityEngine.UI;

public class TecladoVirtual : MonoBehaviour
{

    public TMP_InputField InputNome;
    public TMP_InputField InputEmail;
    public TMP_InputField InputTelefone;
    public TMP_InputField inputField;


    void Start()
    {
        inputField = null;

        InputNome.onSelect.AddListener(delegate{SetCampoAtivo(InputNome);});
        InputEmail.onSelect.AddListener(delegate{SetCampoAtivo(InputEmail);});
        InputTelefone.onSelect.AddListener(delegate{SetCampoAtivo(InputTelefone);});

    }

    void SetCampoAtivo(TMP_InputField campo)
    {
        inputField = campo;
    }
    // add as letras, numeros etc... no teclado
    public void AddBotao(string botao)
    {
        if(inputField != null)
            inputField.text += botao;
    }

    public void AddEspaco()
    {
        if(inputField != null)
        inputField.text += " ";
    }

    public void Apagar()
    {
        if(inputField != null && inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void Confirmar()
    {
        Debug.Log("Nome: " + InputNome.text);
        Debug.Log("Email" + InputEmail.text);
        Debug.Log("Telefone" + InputTelefone.text);
    }
}
