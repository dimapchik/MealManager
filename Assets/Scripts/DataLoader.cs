using System.Collections.Generic;
using Mono.Data.SqliteClient;
using UnityEngine;

public class DataLoader : MonoBehaviour {
    private string dbName = "MealsDB.sqlite";

    public List<MealData> LoadMeals(string from, string to) {
        List<MealData> meals = new List<MealData>();
        string connectionString = "URI=file:" + dbName;

        using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();
            using (var command = connection.CreateCommand()) {
                command.CommandText = "SELECT name, calories, proteins, fats, carbs, date FROM Meals " + 
                "WHERE date >= " + from +" AND date <= " + to + ")";
                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        MealData meal = new MealData {
                            Name = reader.GetString(0),
                            Calories = reader.GetFloat(1),
                            Proteins = reader.GetFloat(2),
                            Fats = reader.GetFloat(3),
                            Carbohydrates = reader.GetFloat(4),
                            Date = reader.GetString(5)
                        };
                        meals.Add(meal);
                    }
                }
            }
        }
        return meals;
    }
}

[System.Serializable]
public class MealData {
    public string Name;
    public float Calories;
    public float Proteins;
    public float Fats;
    public float Carbohydrates;
    public string Date;
}
