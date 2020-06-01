using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
    // levels:
    // 1-MiestoAkcentai
    // 2/GatvėsMenas
    // 3-Architektūra
    // 4-Muziejai
    // 5-iIstorija
    // 6-Skulptūros

    string[] categoryArray = { "MiestoAkcentai", "GatvėsMenas", "Architektūra", "Muziejai", "Istorija", "Skulptūros" };

    public int getScore() //return "category" score
    {
        return PlayerPrefs.GetInt("currentScore");
    }

    public int getLevel()
    {
        return PlayerPrefs.GetInt("currentLevel");
    }

    public int getLevelStatus(string category)
    {
        return PlayerPrefs.GetInt(category);
    }

    public int getLevelScore(string category)
    {
        return PlayerPrefs.GetInt(category+"Score");
    }

    public void setLevelScore(string category, int score)
    {   
        PlayerPrefs.SetInt(category + "Score", score);
        if(score >= 60)
        {
            PlayerPrefs.SetInt(category, 2); //pazymiu, jog pereitas lygis
        }
    }

    public int getLastLvl()
    {
        return PlayerPrefs.GetInt("lastUnlockedLevel");
    }

    public int getScoreSum()
    {
        int sum = 0;
        foreach(string categ in categoryArray)
        {
            sum += getLevelScore(categ);
        }
        return sum;
    }

	// Use this for initialization
	void Start () {
        checkIfInitialised();
	}

    //patikrina, ar jau sukurti playerprefs raktai kategorijoms
    public void checkIfInitialised()
    {
        if (!PlayerPrefs.HasKey("currentScore")) PlayerPrefs.SetInt("currentScore", 0); //bendra tasku suma
        if (!PlayerPrefs.HasKey("lastUnlockedLevel")) PlayerPrefs.SetInt("lastUnlockedLevel", 1); //paskutinis atrakintas lygis

        //ar atrakinta kategorija (0-uzrakinta, 1-atrakinta, 2-done, atrakinti kita kategorija)
        if (!PlayerPrefs.HasKey("MiestoAkcentai")) PlayerPrefs.SetInt("MiestoAkcentai", 1); // miesto akcentų kategorija atrakinta (nes pirma)
        if (!PlayerPrefs.HasKey("GatvėsMenas")) PlayerPrefs.SetInt("GatvėsMenas", 0); //visos kitos kategorijos uzrakintos
        if (!PlayerPrefs.HasKey("Architektūra")) PlayerPrefs.SetInt("Architektūra", 0);
        if (!PlayerPrefs.HasKey("Muziejai")) PlayerPrefs.SetInt("Muziejai", 0);
        if (!PlayerPrefs.HasKey("Istorija")) PlayerPrefs.SetInt("Istorija", 0);
        if (!PlayerPrefs.HasKey("Skulptūros")) PlayerPrefs.SetInt("Skulptūros", 0);

        //kiekvienos kategorijos surinkti taskai atskirai
        if (!PlayerPrefs.HasKey("MiestoAkcentaiScore")) PlayerPrefs.SetInt("MiestoAkcentaiScore", 0);
        if (!PlayerPrefs.HasKey("GatvėsMenasScore")) PlayerPrefs.SetInt("GatvėsMenasScore", 0);
        if (!PlayerPrefs.HasKey("ArchitektūraScore")) PlayerPrefs.SetInt("ArchitektūraScore", 0);
        if (!PlayerPrefs.HasKey("MuziejaiScore")) PlayerPrefs.SetInt("MuziejaiScore", 0);
        if (!PlayerPrefs.HasKey("IstorijaScore")) PlayerPrefs.SetInt("IstorijaScore", 0);
        if (!PlayerPrefs.HasKey("SkulptūrosScore")) PlayerPrefs.SetInt("SkulptūrosScore", 0);
    }

    public void Destroy()
    {
        PlayerPrefs.DeleteAll();
    }

    //metodas naujai kategorijai atrakinti
    public void UnlockNewCategory()
    {
        int lvl = PlayerPrefs.GetInt("lastUnlockedLevel");
        lvl += 1;
        PlayerPrefs.SetInt("lastUnlockedLevel", lvl);
        switch (lvl)
        {
            case 1:
                PlayerPrefs.SetInt("MiestoAkcentai", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("GatvėsMenas", 1);
                break;
            case 3:
                PlayerPrefs.SetInt("Architektūra", 1);
                break;
            case 4:
                PlayerPrefs.SetInt("Muziejai", 1);
                break;
            case 5:
                PlayerPrefs.SetInt("Istorija", 1);
                break;
            case 6:
                PlayerPrefs.SetInt("Skulptūros", 1);
                break;
            default:
                Debug.Log("UnlockNewCategory() error");
                break;
        }
    }
}
