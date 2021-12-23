using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject[] pets;
    public GameObject[] petPrefabs;
    public int selectedPet;

    public GameObject petNamer;
    public void NextPet()
    {
        pets[selectedPet].SetActive(false);
        selectedPet += 1;
        if(selectedPet >= pets.Length)
        {
            selectedPet = 0;
        }
        pets[selectedPet].SetActive(true);
    }
    public void PreviousPet()
    {
        pets[selectedPet].SetActive(false);
        selectedPet -= 1;
        if (selectedPet < 0)
        {
            selectedPet = pets.Length - 1;
        }
        pets[selectedPet].SetActive(true);
    }
}
