using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControler : MonoBehaviour
{
    private List<Carta> revelandoCards = new List<Carta>();
    private bool canFlip = true;
    
    public TextMeshProUGUI textoPontuação;
    private int pontos = 0;
    public AlterarLayer alterarLayer;


    void Start()
    {
        AtualizarPontos();
    }

    public void CardRevelada(Carta card)
    {
        revelandoCards.Add(card);

        if(revelandoCards.Count == 2)
        {
            canFlip = false;
            StartCoroutine(ChecarPar());
        }
    }

    public IEnumerator<System.Object> ChecarPar()
    {
        yield return new WaitForSeconds(1f);

        if(revelandoCards[0].cardId == revelandoCards[1].cardId)
        {
            pontos += 50;
            AtualizarPontos();
            revelandoCards[0].BloquearBotao();
            revelandoCards[1].BloquearBotao();
            Debug.Log("Par Encontrado");

            if(alterarLayer != null)
            {
                alterarLayer.RegistrarParEncontrado();
            }
        }
        else
        {
            pontos -= 10;
            if (pontos < 0) pontos = 0;
            AtualizarPontos();
            revelandoCards[0].VirarPraBaixo();
            revelandoCards[1].VirarPraBaixo();
            Debug.Log("Não Encontrado");
        }
        revelandoCards.Clear();
        canFlip = true;
    }
    public bool CanFlip()
    {
        return canFlip;
    }


    private void AtualizarPontos()
    {
        textoPontuação.text = "Pontos" + pontos;
        PlayerPrefs.SetInt("PontuacaoFinal", pontos);
    }
    private void ResetarPontos()
    {
        pontos = 0;
    }

}
