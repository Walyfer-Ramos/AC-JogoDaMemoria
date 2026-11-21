using UnityEngine;
using System.Collections.Generic;
// using UnityEngine.UI;
using System.IO;
using System.Linq;
using TMPro;

public class Ranking : MonoBehaviour
{

    [System.Serializable]
    public class Jogador
    {
        public string nome;
        public string email;
        public string telefone;
        public int pontuacao;

        public Jogador(string nome, string email, string telefone, int pontuacao)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.pontuacao = pontuacao;
        }
    }

    public TextMeshProUGUI rankingText;
    private List<Jogador> jogadores = new List<Jogador>();

    private string caminhoCSV;

    void Awake()
    {
        caminhoCSV = Path.Combine(Application.persistentDataPath, "jogadores.csv");

        if (!File.Exists(caminhoCSV))
        {
            File.WriteAllText(caminhoCSV, "Nome, E-mail, Telefone, Pontuacao\n");
        }
    }

    public void CadastrarJogador(string nome, string email, string telefone, int pontuacao)
    {
        Jogador novo = new Jogador(nome, email, telefone, pontuacao);
        jogadores.Add(novo);

        string linha = $"{nome},{email},{telefone},{pontuacao}\n";
        File.AppendAllText(caminhoCSV, linha);
    }

    public void MostrarRank()
    {
        string[] linhas = File.ReadAllLines(caminhoCSV).Skip(1).ToArray();
        jogadores.Clear();

        foreach(string linha in linhas)
        {

            if (string.IsNullOrWhiteSpace(linha)) continue;
            string[] dados = linha.Split(',');
            if(dados.Length == 4)
            {
                string nome = dados[0];
                string email = dados[1];
                string telefone = dados[2];
                int pontuacao = int.Parse(dados[3]);

                jogadores.Add(new Jogador(nome, email, telefone, pontuacao));
            }
        }

        var rankingOrdenado = jogadores.OrderByDescending(j => j.pontuacao).ToList();
        rankingText.text = "Ranking: \n";
        for(int i = 0; i< rankingOrdenado.Count; i++)
        {
            rankingText.text += $"{i + 1}. {rankingOrdenado[i].nome} - {rankingOrdenado[i].pontuacao}\n";

        }
    }
}
