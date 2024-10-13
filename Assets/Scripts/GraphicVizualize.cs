public class GraphVisualizer : MonoBehaviour {
    public DataLoader dataLoader;
    public GraphChart chart;
    void Start() {
        List<MealData> meals = dataLoader.LoadMeals();
        PrepareData(meals);
    }

    private void PrepareData(List<MealData> meals) {
        List<float> calories = new List<float>();
        List<string> dates = new List<string>();

        foreach (var meal in meals) {
            calories.Add(meal.Calories);
            dates.Add(meal.Date);
        }

        PlotGraph(dates.ToArray(), calories.ToArray());
    }

    private void PlotGraph(string[] dates, float[] calories) {
        chart.ClearData()
        chart.AddDataSeries("Calories", calories);
        chart.SetXAxisLabels(dates);
    }
}
