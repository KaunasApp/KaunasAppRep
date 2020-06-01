using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Klausimas[] klausimai;
    public GameManager gameManager;
    public ScoreSystem scoreSystem;
    public CategoryObj[] kategorijos;
    public Button[] categories;
    public Text currentScore;

    private void Start()
    {
        //scoreSystem.Destroy(); //ISTRINA VISUS PLAYERPREFS PRIES ZAIDIMA
        for(int i = 0; i < kategorijos.Length; i++)
        {
            gameManager.gameObject.SetActive(false);
            gameObject.SetActive(true);
            categories[i].image.sprite = kategorijos[i].categoryImage;
            categories[i].transform.GetComponentInChildren<Text>().text = kategorijos[i].categoryText;
        }
        ScorePresentation();
        LevelPresentation();
    }

    public void Updt()
    {
        ScorePresentation();
        LevelPresentation();
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
        gameManager.ShowLevel(atrinktiKlausimai, chosenCategory.ToString());
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
                categ = CategoryEnum.Category.GatvėsMenas;
                PickOutQuestions(categ);
                break;
            case "Architektūra":
                categ = CategoryEnum.Category.Architektūra;
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
                categ = CategoryEnum.Category.Skulptūros;
                PickOutQuestions(categ);
                break;
            default:
                Debug.Log("Klaida atpazistant kategorija. Gauta: "+ chosenCategory);
                break;
        }
    }

    public void ScorePresentation()
    {
        scoreSystem.checkIfInitialised();
        int score = scoreSystem.getScoreSum();
        currentScore.text = "Taškai: " + score.ToString();
    }

    public void LevelPresentation()
    {
        string[] categoryArray = { "MiestoAkcentai", "GatvėsMenas", "Architektūra", "Muziejai", "Istorija", "Skulptūros" };
        int i = 0;
        foreach(string name in categoryArray)
        {
            if(scoreSystem.getLevelStatus(name) > 0)
            {
                categories[i].interactable = true;
            }
            else if(scoreSystem.getLevelStatus(name) == 0) categories[i].interactable = false;
            i++;
        }
    }

}
