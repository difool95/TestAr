[System.Serializable]
public class Case 
{
    public float covidNumber;
    public float deadNumber;
    public string countryName;
    public Case(float covidNumber, float deadNumber, string countryName)
    {
        this.covidNumber = covidNumber;
        this.deadNumber = deadNumber;
        this.countryName = countryName;
    }

    public Case()
    {

    }
}
