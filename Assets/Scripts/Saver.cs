using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saver : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        SaveSystem.Init();
        DontDestroyOnLoad(gameObject);
        player = FindObjectOfType<Player>();
        
    }


    public void Save()
    {
        player.UpdateSaveData();

        //converts dictionarys into json file seperated by "n" so that it can be split later
        string json = JsonConvert.SerializeObject(player.saveData, Formatting.Indented);
            
        SaveSystem.Save(json);
        //debug line if you need to view saved json file
        Debug.Log(json);
    }

    public void LoadSave()
    {
        string saveString = SaveSystem.Load();

        //seperate dictionary strings and convert them back into dictionaries
        List<string> saveData = JsonConvert.DeserializeObject<List<string>>(saveString);        
                
        //take dictionaries from save data and put them into game data
        player.saveData = saveData;
        player.LoadSaveData();
    }

    public void DeleteSaves()
    {

        SaveSystem.DeleteSaves();
        Debug.Log("Deleted Saves");
    }



}
