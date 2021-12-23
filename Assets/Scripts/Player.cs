using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public List<GameObject> ownedPets;    
    public Pet selectedPet;
    private Store store;
    public GameObject storeMenu;
    public GameObject collectionMenu;
    public GameObject collection;

    public GameObject interactions;

    public InputField inputField;

    public GameObject foods;

    public Text petName;
    public Text petHunger;
    public Text petHappiness;

    public List<string> saveData;
    //gameobject array of all possible pets you can own in order to load them back in
    public GameObject[] petPrefabs;


    //saving data in a list of strings
    public void UpdateSaveData()
    {
        saveData.Clear();
        foreach(GameObject pet in ownedPets)
        {
                saveData.Add(pet.GetComponent<Pet>().petTypeInt.ToString() + "," +
                pet.GetComponent<Pet>().petNameStr + "," +
                pet.GetComponent<Pet>().happiness.ToString() + "," +
                pet.GetComponent<Pet>().hunger.ToString());
        }
    }

    

    //loading sava data from string
    // 0 = pet type
    /* 1 = pet name
     * 2 = happiness
     * 3 = hunger
     * 
     */
    public void LoadSaveData()
    {
        for(int i = 0; i <= saveData.Count - 1; i++)
        {
            string[] splitData = saveData[i].Split(',');

            //adding pet into collection from load
            collectionMenu.SetActive(true);
            storeMenu.SetActive(false);
            GameObject clonePet = Instantiate(petPrefabs[int.Parse(splitData[0])]);
            ownedPets.Add(clonePet);            
            ownedPets[ownedPets.Count - 1].gameObject.transform.SetParent(collection.transform, false);

            
            //assigning data to loaded pet
            ownedPets[i].GetComponent<Pet>().petNameStr = splitData[1];            
            ownedPets[i].GetComponent<Pet>().happiness = int.Parse(splitData[2]);
            ownedPets[i].GetComponent<Pet>().hunger = int.Parse(splitData[3]);
            ownedPets[i].GetComponent<Pet>().petName.text = ownedPets[i].GetComponent<Pet>().petNameStr;
            

        }
        interactions.SetActive(true);
        
    }
    
    public void OpenStore()
    {
        storeMenu.SetActive(true);
        collectionMenu.SetActive(false);
    }

    public void OpenCollection()
    {
        storeMenu.SetActive(false);
        collectionMenu.SetActive(true);
        if(ownedPets.Count == 0)
        {
            interactions.SetActive(false);
            return;
        }
        interactions.SetActive(true);        
        
    }    

    public void BuyPet()
    {        
        if(store == null)
        {
            store = FindObjectOfType<Store>();
        }
        store.petNamer.SetActive(true);
    }


    //function to buy pet, first create clone from prefab of pet then add the clone into the list
    
    public void AddPetToCollection(InputField petName)
    {
        GameObject clonePet = Instantiate(store.petPrefabs[store.selectedPet]);
        ownedPets.Add(clonePet);        
        ownedPets[ownedPets.Count - 1].GetComponent<Pet>().petNameStr = petName.text;        
        collectionMenu.SetActive(true);
        ownedPets[ownedPets.Count - 1].gameObject.transform.SetParent(collection.transform,false);     
        
        collectionMenu.SetActive(false);
        store.petNamer.SetActive(false);
    }

   
    public void OpenFoodMenu()
    {
        if (foods.activeInHierarchy)
        {
            foods.SetActive(false);
        }
        else if (!foods.activeInHierarchy)
        {
            foods.SetActive(true);
        }
    }
    
    public void UpdatePetStats()
    {        
        petName.text = selectedPet.petNameStr;
        petHappiness.text = selectedPet.happiness.ToString();
        petHunger.text = selectedPet.hunger.ToString();
    }

    public void Petting()
    {
        selectedPet.Petting();
    }

    

    

    
}
