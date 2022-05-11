using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagment : MonoBehaviour
{
    [SerializeField] int startingSection;
    [SerializeField] GameObject[] sections;
    int previousSection;

    private void Start()
    {
        for (int i = 0; i < sections.Length; i++)
            sections[i].SetActive(false);

        SetActiveSection(startingSection);
    }

    public void SetActiveSection(int section)
    {
        sections[previousSection].SetActive(false);
        sections[section].SetActive(true);
        previousSection = section;
    }
}
