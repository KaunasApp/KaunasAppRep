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
    public Text progress; // kelintas dabar atsakinejamas klausimas
    public Text showScore; //rodomi surinkti taskai siame zaidime
    public Button backToMenu;
    public GameObject EndWindow;
    public GameObject EndBackground;
    public Text closingText; //kategorijos zaidimo pabaigos tekstas
    public Button endMenu; //kategorijos pabaiga, grizimas i menu
    public Button endReplay; //kategorijos pabaiga, zaisti dar karta
    public ScoreSystem scoreSystem;
    public Sprite greenBckgr;
    public Sprite redBackgr;
    public Sprite neutralBckgr;

    private int currentQuestionNr = 0; //einamo klausimo nr
    private List<Klausimas> currentQuestions; //einamos kategorijos klausimai
    private string currentCategory; //zaidziamos kategorijos pavadinimas
    private int score;

    
    public void ShowLevel(List<Klausimas> klausimai, string categoryName)
    {
        score = 0;
        currentQuestionNr = 0;
        EndWindow.SetActive(false);
        gameObject.SetActive(true);
        currentCategory = categoryName;
        currentQuestions = klausimai;
        ShowNextQuestion();
        Debug.Log(currentQuestions.Count + "klausimu count");
        Debug.Log(scoreSystem.getLevelStatus(currentCategory)+"current category status");
    }

    //Grįžti atgal į kategorijų meniu
    public void BackToCategoryMenu()
    {
        EndWindow.SetActive(false);
        menuManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
        menuManager.Updt();
    }

    //žaisti dar kartą
    public void Replay()
    {
        ShowLevel(currentQuestions, currentCategory);
    }

    //Klausimo pateikimas
    private void ShowNextQuestion()
    {
        Debug.Log(currentQuestionNr+"dabartinis klausimas");
     
        string prog = currentQuestionNr + 1 + " / " + currentQuestions.Count;
        progress.text = prog;
        showScore.text = "Taškai: " + score.ToString(); 

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
            Debug.Log(scoreSystem.getLevelStatus(currentCategory));
            if(scoreSystem.getLevelStatus(currentCategory) == 2)
            {
                if(score > scoreSystem.getLevelScore(currentCategory))
                {
                    scoreSystem.setLevelScore(currentCategory, score); //padidino score, bet nebeatrakina naujos
                    closingText.text = "Kita kategorija jau atrakinta.\n\rPagerinai rezultatą!";
                    EndBackground.GetComponent<SpriteRenderer>().sprite = neutralBckgr;
                    EndWindow.SetActive(true);
                    gameObject.SetActive(false);
                }
                else
                {
                    closingText.text = "Kita kategorija jau atrakinta";
                    EndBackground.GetComponent<SpriteRenderer>().sprite = neutralBckgr;
                    EndWindow.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
            else if(scoreSystem.getLevelStatus(currentCategory) == 1)
            {
                if(score > scoreSystem.getLevelScore(currentCategory))
                {
                    scoreSystem.setLevelScore(currentCategory, score); //issaugoju nauja score

                    if (scoreSystem.getLevelStatus(currentCategory) == 2) //atrakina nauja kategorija
                    {
                        if(currentCategory == "Skulptūros")
                        {
                            gameObject.SetActive(false);
                            closingText.text = "Įveikėte visas kategorijas!";
                            EndBackground.GetComponent<SpriteRenderer>().sprite = greenBckgr;
                            EndWindow.SetActive(true);
                            gameObject.SetActive(false);
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            closingText.text = "Atrakinta nauja kategorija!";
                            EndBackground.GetComponent<SpriteRenderer>().sprite = greenBckgr;
                            EndWindow.SetActive(true);
                            gameObject.SetActive(false);
                            scoreSystem.UnlockNewCategory();
                        }
                    }
                    else
                    {
                        //pagerintas score, bet neatrakinta nauja kategorija
                        closingText.text = "Pagerinai rezultatą!\n\rDeja, nepakanka taškų naujai kategorijai atrakinti";
                        EndBackground.GetComponent<SpriteRenderer>().sprite = neutralBckgr;
                        EndWindow.SetActive(true);
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    //neatrakinta nauja kategorija, nepagerino score
                    closingText.text = "Nepakanka taškų naujai kategorijai atrakinti.\n\rBandyk dar kartą!";
                    EndBackground.GetComponent<SpriteRenderer>().sprite = redBackgr;
                    EndWindow.SetActive(true);
                    gameObject.SetActive(false);
                }      
            }
            else
            {
                Debug.Log("Klaida, levelSatus = 0, negali būti");
            }
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
            //pasirinktas teisingas atsakymas
            Color col = btn.image.color;
            col = correctColor;
            col.a = 1f;
            btn.image.color = col;
            score += 10;
        }
        else
        {
            //pasirinktas neteisingas atsakymas
            Color col = btn.image.color;
            col = wrongColor;
            col.a = 1f;
            btn.image.color = col;
        }

        currentQuestionNr++;
        Invoke("ShowNextQuestion", 1f);

    }
}
