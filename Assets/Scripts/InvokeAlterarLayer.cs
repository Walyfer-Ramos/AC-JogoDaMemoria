using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlterarLayer : MonoBehaviour
{
  
    public float tempoLimite = 10f;
    private float tempoRestante;
    public TextMeshProUGUI textoTempo;
    private int paresEncontrados = 0;
    private int totalPares = 6;

    private bool jogoIniciado = false;

    public GameObject Tempo;

    void Start()
    {
        tempoRestante = tempoLimite;
        StartCoroutine(EsperarAntesDeComecar());

    }

    void Update()
    {
        
        if(!jogoIniciado) return;
        tempoRestante -= Time.deltaTime;
        if(tempoRestante <= 0)
        {
            FimDaPartida();
        }

        if(textoTempo != null)
        {
            textoTempo.text = "Tempo\n" + Mathf.CeilToInt(tempoRestante).ToString();
        }
    }

    void FimDaPartida()
    {
        int pontos = PlayerPrefs.GetInt("PontuacaoFinal", 0);

        int bonusTempo = Mathf.CeilToInt(tempoRestante);
        int pontuacaoFinal = pontos + bonusTempo;

        PlayerPrefs.SetInt("PontuacaoFinal", pontuacaoFinal);


        SceneManager.LoadScene("PontosRank");
    }

    public void RegistrarParEncontrado()
    {
        paresEncontrados++;
        if(paresEncontrados >= totalPares)
        {
            FimDaPartida();
        }
    }

    public void IniciarJogo()
    {
        jogoIniciado = true;
    }
    private System.Collections.IEnumerator EsperarAntesDeComecar()
    {
        yield return new WaitForSeconds(3);
        IniciarJogo();
        Tempo.SetActive(true);
    }


}
