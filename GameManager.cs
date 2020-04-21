using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public MenuManager menuManager;
    public Button[] optionButtons;
    public Text question;
    public Image questionImage;
    public Color correctColor, wrongColor, normalCol;
    public Text progress;
    public Button atgal;

    private int currentQuestionNr = 0; //einamo klausimo nr
    private List<Klausimas> currentQuestions; //einamos kategorijos klausimai

    
    public void ShowLevel(List<Klausimas> klausimai)
    {
        currentQuestions = klausimai;
        ShowNextQuestion();
    }

    //Grįžti atgal į kategorijų meniu
    public void BackToCategoryMenu()
    {
        menuManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    //Klausimo pateikimas
    private void ShowNextQuestion()
    {
        string prog = currentQuestionNr + 1 + " / " + currentQuestions.Count;
        progress.text = prog;

        //jei dar nepasibaige klausimai
        if(currentQuestionNr < currentQuestions.Count)
        {
            //jei klausimas turi paveiksleli
            if (currentQuestions[currentQuestionNr].questionImage != null)
            {
                questionImage.gameObject.SetActive(true);
                questionImage.sprite = currentQuestions[currentQuestionNr].questionImage;
            }
            //jei neturi
            else
            {
                questionImage.gameObject.SetActive(false);
            }

            Button[] shuffledOpt = optionButtons;
            Shuffle(shuffledOpt);
            //atsakymo mygtuku setup
            for (int i = 0; i < shuffledOpt.Length; i++)
            {
                shuffledOpt[i].GetComponentInChildren<Text>().text = currentQuestions[currentQuestionNr].options[i];
                Color col = shuffledOpt[i].image.color;
                col = normalCol;
                col.a = 1f;
                shuffledOpt[i].image.color = col;
            }

            // klausimo tekstas
            question.text = currentQuestions[currentQuestionNr].questionInfo;
        }
        //atsakyti visi klausimai
        else
        {
            Debug.Log("baigta");
        }
    }

    //Atsakymo variantu maisymas
    public void Shuffle(Button[] btns)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < btns.Length; t++)
        {
            Button tmp = btns[t];
            int r = Random.Range(t, btns.Length);
            btns[t] = btns[r];
            btns[r] = tmp;
        }
    }

    //tikrina ar pasirinktas teisingas ats
    public void PickQuestion(Button btn)
    {

        if (btn.GetComponentInChildren<Text>().text == currentQuestions[currentQuestionNr].correctAns)
        {
            Debug.Log("MLDC");
            Color col = btn.image.color;
            col = correctColor;
            col.a = 1f;
            btn.image.color = col;

        }
        else
        {
            Debug.Log("YOU SUCK");
            Color col = btn.image.color;
            col = wrongColor;
            col.a = 1f;
            btn.image.color = col;
        }

        currentQuestionNr++;
        Invoke("ShowNextQuestion", 1f);

    }
}
