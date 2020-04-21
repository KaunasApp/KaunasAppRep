using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Klausimas[] klausimai;
    public GameManager gameManager;
    public CategoryObj[] kategorijos;
    public Button[] categories;
    private void Start()
    {
        for(int i = 0; i < kategorijos.Length; i++)
        {
            //gameManager.gameObject.SetActive(false);
            //gameObject.SetActive(true);
            categories[i].image.sprite = kategorijos[i].categoryImage;
            categories[i].transform.GetComponentInChildren<Text>().text = kategorijos[i].categoryText;
        }      
    }

    //nurodytos kategorijos klausimu atrinkimas
    public void PickOutQuestions(CategoryEnum.Category chosenCategory)
    {    
        List<Klausimas> atrinktiKlausimai = new List<Klausimas>();

        foreach(var question in klausimai)
        {
            if (question.category == chosenCategory)
            {
                atrinktiKlausimai.Add(question);
            }
        }
        Debug.Log(atrinktiKlausimai.Count);
        gameManager.ShowLevel(atrinktiKlausimai);
        gameManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ShowQustions(Text text)
    {
        string chosenCategory = text.text;
        CategoryEnum.Category categ = new CategoryEnum.Category();
        switch (chosenCategory)
        {
            case "Miesto akcentai":
                categ = CategoryEnum.Category.MiestoAkcentai;
                PickOutQuestions(categ);
                break;
            case "Gatvės menas":
                categ = CategoryEnum.Category.GatvesMenas;
                PickOutQuestions(categ);
                break;
            case "Architektūra":
                categ = CategoryEnum.Category.Architektura;
                PickOutQuestions(categ);
                break;
            case "Muziejai":
                categ = CategoryEnum.Category.Muziejai;
                PickOutQuestions(categ);
                break;
            case "Istorija":
                categ = CategoryEnum.Category.Istorija;
                PickOutQuestions(categ);
                break;
            case "Skulptūros":
                categ = CategoryEnum.Category.Skulpturos;
                PickOutQuestions(categ);
                break;
            default:
                Debug.Log("Klaida atpazistant kategorija. Gauta: "+ chosenCategory);
                break;
        }
    }

}
