using UnityEngine;
using TMPro;

public class Pontuacao : MonoBehaviour
{

public TextMeshProUGUI textoPontuacaoFinal;
    void Start()
    {
        int pontos = PlayerPrefs.GetInt("PontuacaoFinal", 0);
        textoPontuacaoFinal.text = " Sua Pontuação " + pontos;


        string nome = PlayerPrefs.GetString("NomeJogador", "SemNome");
        string email = PlayerPrefs.GetString("EmailJogador", "SemEmail");
        string telefone = PlayerPrefs.GetString("TelefoneJogador", "SemTelefone");

        //FindFirstObjectByType<Ranking>().CadastrarJogador(nome, email, telefone, pontos);

        Ranking rk = FindFirstObjectByType<Ranking>();
        if(rk != null)
        {
            rk.CadastrarJogador(nome, email, telefone, pontos);
        }
    }
}
