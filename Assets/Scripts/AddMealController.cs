using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Mono.Data.SqliteClient;
using System.IO;


public class AddMealController : MonoBehaviour {
    public InputField Name;
    public InputField Calories;
    public InputField Protein;
    public InputField Fats;

    public InputField Carbohydrates;
    public Button Save;

    private string name;
    private float calories;
    private float protein;
    private float fats;
    private float carbohydrates;

    private string dbName = "MealsDB.sqlite";

    void Start() {
        Save.onClick.AddListener(SaveMeal);
        Name.onValueChanged.AddListener(UpdateName);
        Calories.onValueChanged.AddListener(UpdateCalories);
        Protein.onValueChanged.AddListener(UpdateProtein);
        Fats.onValueChanged.AddListener(UpdateFats);
        Carbohydrates.onValueChanged.AddListener(UpdateCarbohydrates);
    }

    void UpdateName(string value) {
        name = value;
    }
    void UpdateCalories(string value) {
        if (float.TryParse(value, out float new_calories)) {
            calories = new_calories;
        }
    }
    void UpdateProtein(string value) {
        if (float.TryParse(value, out float new_protein)) {
            protein = new_protein;
        }
    }
    void UpdateFats(string value) {
        if (float.TryParse(value, out float new_fats)) {
            fats = new_fats;
        }
    }
    void UpdateCarbohydrates(string value) {
        if (float.TryParse(value, out float new_carbohydrates)) {
            carbohydrates = new_carbohydrates;
        }
    }

    void SaveMeal() {
        string dbPath = Path.Combine(Application.persistentDataPath, dbName);
        string connectionString = "URI=file:" + dbPath;
        Debug.Log(connectionString);
        using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();
            //TODO exeption with irrcorrect input;
            using (var command = connection.CreateCommand()) {
                string currentDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                command.CommandText = 
                    "INSERT INTO Meals (name, calories, proteins, fats, carbs, date) " +
                    "VALUES (" + name.ToString() + ", " + calories.ToString() + ", "
                    + protein.ToString() + ", " + fats.ToString() + ", " 
                    + carbohydrates.ToString() + ", " + currentDate + ")";
                Debug.Log(currentDate);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
