using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    public float hunger;
    public float happiness;    

    public float maxHunger;
    public float maxHappiness;

    public Text petName;
    public string petNameStr;
    public string petType;
    public int petTypeInt;

    public bool interactable;

    public float timer;
    //time it takes for the pet to gain hunger and lose happiness
    public float timeTillDecay;
    public Player player;

    public void Start()
    {        
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeTillDecay)
        {
            UpdatePets();            
            
        }
    }

    public void UpdatePets()
    {   
        hunger += 1;
        happiness -= 1;        
    }

    private void OnMouseDown()
    {
        player.selectedPet = this;
        player.UpdatePetStats();
    }

    //if pet dislikes food or the interaction they lose two happiness and gain two hunger
    public void Dislike()
    {
        hunger += 2;
        happiness -= 2;        
    }

    //function to check if pets happiness or hunger are at the threshold for interaction
    public void CheckIfInteractable()
    {
        if (hunger >= maxHunger || happiness <= 0)
        {
            interactable = false;
        }
        else
        {
            interactable = true;
        }
    }


    public void Petting()
    {
        hunger += 1;
        happiness += 1;
        player.UpdatePetStats();
    }

    

}

