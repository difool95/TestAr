public class CaseDetails : Case
{
    public int covidToday;
    public int deadToday;
    public int recoveredTotal;
    public int recoveredToday;

    public CaseDetails(int covidNumberToday, int deadNumberToday, int recoveredNumberTotal, int recoveredNumberToday,
        int covidNumber, int deadNumber, string countryName)
        :base(covidNumber, deadNumber,countryName)
    {
        covidToday = covidNumberToday;
        deadToday = deadNumberToday;
        recoveredTotal = recoveredNumberTotal;
        recoveredToday = recoveredNumberToday;
    }

    public CaseDetails() : base()
    {

    }
}
