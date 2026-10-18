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

    void Start()
    {
        // Define o caminho do arquivo CSV
        caminhoCSV = Path.Combine(
            Application.persistentDataPath,
            "jogadores.csv"
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

        // Verifica se todos os campos estão preenchidos
        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(telefone))
        {
            Debug.LogWarning("Preencha todos os campos!");
            return;
        }

        // Mantém os dados salvos no PlayerPrefs
        PlayerPrefs.SetString("NomeJogador", nome);
        PlayerPrefs.SetString("EmailJogador", email);
        PlayerPrefs.SetString("TelefoneJogador", telefone);
        PlayerPrefs.Save();

        // Salva os dados no arquivo CSV
        SalvarNoCSV(nome, email, telefone);

        Debug.Log("Cadastro salvo: " + nome + " / " + email + " / " + telefone);
    }

    void SalvarNoCSV(string nome, string email, string telefone)
    {
        try
        {
            // Cria a pasta, caso seja necessário
            string pasta = Path.GetDirectoryName(caminhoCSV);
            Directory.CreateDirectory(pasta);

            // Cria o cabeçalho se o arquivo ainda não existir
            if (!File.Exists(caminhoCSV))
            {
                string cabecalho = "Nome;Email;Telefone" + System.Environment.NewLine;
                File.WriteAllText(caminhoCSV, cabecalho, Encoding.UTF8);
            }

            // Prepara os campos para evitar problemas com ;, aspas e quebras de linha
            string linha = FormatarCSV(nome) + ";" +
                           FormatarCSV(email) + ";" +
                           FormatarCSV(telefone) +
                           System.Environment.NewLine;

            // Acrescenta o novo cadastro sem apagar os anteriores
            File.AppendAllText(caminhoCSV, linha, Encoding.UTF8);

            Debug.Log("Cadastro adicionado ao CSV: " + caminhoCSV);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao salvar CSV: " + e.Message);
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