using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    public int cardId;
    public Sprite frontImage;
    public Sprite backImage;
    private Image cardImage;
    public GameControler gameControler;
    private bool ViradoPraCima = false;
    private Button button;


    void Start()
    {

        cardImage = GetComponent<Image>();
        cardImage.sprite = frontImage;
        gameControler = FindFirstObjectByType<GameControler>();
        button = GetComponent<Button>();

        button.interactable = false;
        StartCoroutine(EsperaAntesDeVirar());
        
    }

    public void onClick()
    {
        if (!ViradoPraCima && gameControler.CanFlip())
        {
            VirarPraCima();
            gameControler.CardRevelada(this);
        }
    }

    public void VirarPraCima()
    {
        ViradoPraCima = true;
        //cardImage.sprite = frontImage;
        AnimacaoVirada(frontImage);
    }

    public void VirarPraBaixo()
    {
        ViradoPraCima = false;
        //cardImage.sprite = backImage;
        AnimacaoVirada(backImage);

    }

    public void BloquearBotao()
    {
        button.interactable = false;
    }

        private System.Collections.IEnumerator EsperaAntesDeVirar()
    {
        yield return new WaitForSeconds(3f);
        VirarPraBaixo();

        button.interactable = true;
    }

    private void AnimacaoVirada(Sprite newSprite)
    {
        LeanTween.scaleX(gameObject, 0f, 0.25f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            cardImage.sprite = newSprite;
            LeanTween.scaleX(gameObject, 1f, 0.25f).setEase(LeanTweenType.easeInOutQuad);
        });
    }

}
