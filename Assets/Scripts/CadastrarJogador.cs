
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CadastrarJogador : MonoBehaviour
{
    public TMP_InputField inputNome;
    public TMP_InputField inputEmail;
    public TMP_InputField inputTelefone;
    public Button botaoContinue;

    private string caminhoCSV;
    private string caminhoTXT;
    private string caminhoBackupTXT;

    void Start()
    {
        caminhoCSV = Path.Combine(
            Application.persistentDataPath,
            "jogadores.csv"
        );

        caminhoTXT = Path.Combine(
            Application.persistentDataPath,
            "jogadores.txt"
        );

        botaoContinue.interactable = false;
        inputNome.ActivateInputField();

        inputNome.onValueChanged.AddListener(delegate { VerificarCampo(); });
        inputEmail.onValueChanged.AddListener(delegate { VerificarCampo(); });
        inputTelefone.onValueChanged.AddListener(delegate { VerificarCampo(); });
    }

    public void SalvarCadastro()
    {
        string nome = inputNome.text.Trim();
        string email = inputEmail.text.Trim();
        string telefone = inputTelefone.text.Trim();

        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(telefone))
        {
            Debug.LogWarning("Preencha todos os campos!");
            return;
        }

        PlayerPrefs.SetString("NomeJogador", nome);
        PlayerPrefs.SetString("EmailJogador", email);
        PlayerPrefs.SetString("TelefoneJogador", telefone);
        PlayerPrefs.Save();

        SalvarNoCSV(nome, email, telefone);
        SalvarNoTXT(nome, email, telefone);

        Debug.Log("Cadastro salvo nos dois documentos!");

    }



    void SalvarNoCSV(string nome, string email, string telefone)
    {
        try
        {
            Directory.CreateDirectory(Application.persistentDataPath);

            if (!File.Exists(caminhoCSV))
            {
                string cabecalho = "Nome;Email;Telefone" +
                    System.Environment.NewLine;

                File.WriteAllText(caminhoCSV, cabecalho, Encoding.UTF8);
            }

            string linha = FormatarCSV(nome) + ";" +
                           FormatarCSV(email) + ";" +
                           FormatarCSV(telefone) +
                           System.Environment.NewLine;

            File.AppendAllText(caminhoCSV, linha, Encoding.UTF8);

            Debug.Log("CSV salvo: " + caminhoCSV);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao salvar CSV: " + e.Message);
        }
    }



    void SalvarNoTXT(string nome, string email, string telefone)
    {
        try
        {
            string registro =
                "============================\n" +
                "CADASTRO DE JOGADOR\n" +
                "============================\n" +
                "Nome: " + nome + "\n" +
                "Email: " + email + "\n" +
                "Telefone: " + telefone + "\n\n";

            File.AppendAllText(caminhoTXT, registro, Encoding.UTF8);

            Debug.Log("TXT salvo: " + caminhoTXT);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao salvar TXT: " + e.Message);
        }
    }




    string FormatarCSV(string valor)
    {
        valor = valor.Replace("\"", "\"\"");

        return "\"" + valor.Replace("\r\n", "\n")
                           .Replace("\r", "\n") + "\"";
    }


    void VerificarCampo()
    {
        bool preenchido =
            !string.IsNullOrWhiteSpace(inputNome.text) &&
            !string.IsNullOrWhiteSpace(inputEmail.text) &&
            !string.IsNullOrWhiteSpace(inputTelefone.text);

        botaoContinue.interactable = preenchido;
    }
}
