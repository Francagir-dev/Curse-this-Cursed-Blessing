using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Absolutamente todo esto es temporal y mal hecho
[CreateAssetMenu(fileName = "New Answers", menuName = "Answers")]
public class Answers : ScriptableObject
{
    public List<string> answersHigh = new List<string>();
    public List<string> answersLow = new List<string>();
    public List<string> answersNone = new List<string>();

    public void RandomPhrases(ChoiceController contrl)
    {
        string[] answers = new string[4];
        answers[0] = answersHigh[Random.Range(0, answersHigh.Count)];
        answers[1] = answersLow[Random.Range(0, answersLow.Count)];
        answers[2] = answersNone[Random.Range(0, answersNone.Count)];
        answers[3] = answersNone[Random.Range(0, answersNone.Count)];

        int[] seq = contrl.Sequence();

        contrl.SetAnswer(seq[0], answers[0], 2);
        contrl.SetAnswer(seq[1], answers[1], 1);
        contrl.SetAnswer(seq[2], answers[2], 0);
        contrl.SetAnswer(seq[3], answers[3], 0);
    }
}
