using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Mono.Data.SqliteClient;
using System.IO;


public class MainMenuController : MonoBehaviour
{
    public Button Add;
    public Button Graphic;
    private string dbName = "MealsDB.sqlite"; 
    private string dbPath;

    void Start() {
        Add.onClick.AddListener(GoToAdd);
        Graphic.onClick.AddListener(GoToGraphic);
        Debug.Log("Start!");
        dbPath = Path.Combine(Application.persistentDataPath, dbName);
        CreateDatabase();
    }

    void GoToAdd() {
        SceneManager.LoadScene("AddMeal");
    }

    void GoToGraphic() {
        SceneManager.LoadScene("Graphic");
    }

    private void CreateDatabase() {
        string connectionString = "URI=file:" + dbPath;
        using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = 
                    "CREATE TABLE IF NOT EXISTS Meals (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "name TEXT NOT NULL, " +
                    "calories REAL, " +
                    "proteins REAL, " +
                    "fats REAL, " +
                    "carbs REAL, " +
                    "date STRING)";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
