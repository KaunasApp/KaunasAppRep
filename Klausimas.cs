using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Klausimas : ScriptableObject {

    public string questionInfo;
    public CategoryEnum.Category category;
    public Sprite questionImage;
    public List<string> options;
    public string correctAns;
}
