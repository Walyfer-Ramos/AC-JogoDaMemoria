using UnityEngine;
using System.Collections.Generic;
// using UnityEditor.Rendering;
// using Unity.VisualScripting;

public class Tabuleiro : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform ParesAleatorios;
    public List<Sprite> cardImages;
    public GameControler gameControler;

    public Carta carta;



    private List<Carta> cartas = new List<Carta>();

    void Start()
    {
        GerarTabuleiro();        
    }

    public void GerarTabuleiro()
    {

        //Lista de Pares
        List<Sprite> imagensParaRodada = new List<Sprite>();
        foreach(var img in cardImages)
        {
            imagensParaRodada.Add(img);
            imagensParaRodada.Add(img);
        }

        // Embaralha
        for(int i = 0; i< imagensParaRodada.Count; i++)
        {
            Sprite temp = imagensParaRodada[i];
            int rand = Random.Range(i, imagensParaRodada.Count);
            imagensParaRodada[i] = imagensParaRodada[rand];
            imagensParaRodada[rand] = temp;
        }
        
        //Instancia Cartas
        for(int i = 0; i< imagensParaRodada.Count; i++)
        {
            GameObject cartaObj = Instantiate(cardPrefab, ParesAleatorios);
            Carta carta = cartaObj.GetComponent<Carta>();
            carta.cardId = cardImages.IndexOf(imagensParaRodada[i]);
            carta.frontImage = imagensParaRodada[i];
            carta.gameControler = gameControler;  
            cartas.Add(carta);      
        }
    }
}
