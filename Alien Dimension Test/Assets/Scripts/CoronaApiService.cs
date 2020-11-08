using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CoronaApiService : MonoBehaviour
{
    string path = "JsonData/countries.json";
    string jsonstring;
    CountryInfoList countryInfoList;
    public static CoronaApiService m_Instance;


    public string JsonFileToString(string path)
    {
        string loadJsonfile = path.Replace(".json", "");
        TextAsset loadedfile = Resources.Load<TextAsset>(loadJsonfile);
        jsonstring = loadedfile.text;
        return jsonstring;
    }

    public void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        //getting local jsonFile
        jsonstring = JsonFileToString(path);
        countryInfoList = JsonUtility.FromJson<CountryInfoList>(jsonstring);
    }


    public  List<CountryName> GetCardsNames()
    {
        return countryInfoList.items;
    }

    public  IEnumerator GetListCase(GameObject cardsGroup, string countryName,int cardIndexIngroup, System.Action<bool, Case> aCallback)
    {
        string path = "https://api.covid19tracking.narrativa.com/api/" + CodeUtils.GetCurrentDate() + "/country/" + countryName;
        UnityWebRequest cardInfoRequest = UnityWebRequest.Get(path);
        var operation = cardInfoRequest.SendWebRequest();
        yield return CodeUtils.ProgressBar(operation);
        // yield return coroutine;
        if (cardInfoRequest.isNetworkError || cardInfoRequest.isHttpError)
        {
            aCallback(false, new Case());
            Debug.Log(cardInfoRequest.error);
            yield break;
        }
        JSONNode cardInfo = JSON.Parse(cardInfoRequest.downloadHandler.text);
        int deaths = cardInfo["dates"][CodeUtils.GetCurrentDate()]["countries"][CodeUtils.UppercaseFirst(countryName)]["today_deaths"];
        int positives = cardInfo["dates"][CodeUtils.GetCurrentDate()]["countries"][CodeUtils.UppercaseFirst(countryName)]["today_confirmed"];
        Case newCase = new Case(positives, deaths, countryName);
        aCallback(true, newCase);
    }

    public Coroutine GetList(GameObject cardsGroup, string countryName,int index, System.Action<bool, Case> aCallback)
    {
        return m_Instance.StartCoroutine(GetListCase(cardsGroup, countryName,index, aCallback));
    }

    public IEnumerator GetSingleCase(GameObject loadingGo, string countryName, System.Action<bool, CaseDetails> aCallback)
    {
        string path = "https://api.covid19tracking.narrativa.com/api/" + CodeUtils.GetCurrentDate() + "/country/" + countryName;
        UnityWebRequest cardInfoRequest = UnityWebRequest.Get(path);
        var operation = cardInfoRequest.SendWebRequest();
        loadingGo.SetActive(true);
        yield return CodeUtils.ProgressBar(operation);
        if (cardInfoRequest.isNetworkError || cardInfoRequest.isHttpError)
        {
            aCallback(true, new CaseDetails());
            Debug.Log(cardInfoRequest.error);
            yield break;
        }
        loadingGo.SetActive(false);
        JSONNode cardInfo = JSON.Parse(cardInfoRequest.downloadHandler.text);
        int deadTotal = cardInfo["dates"][CodeUtils.GetCurrentDate()]["countries"][CodeUtils.UppercaseFirst(countryName)]["today_deaths"];
        int covidTotal = cardInfo["dates"][CodeUtils.GetCurrentDate()]["countries"][CodeUtils.UppercaseFirst(countryName)]["today_confirmed"];
        int covidToday = cardInfo["dates"][CodeUtils.GetCurrentDate()]["countries"][CodeUtils.UppercaseFirst(countryName)]["today_new_confirmed"];
        int deadToday = cardInfo["dates"][CodeUtils.GetCurrentDate()]["countries"][CodeUtils.UppercaseFirst(countryName)]["today_new_deaths"];
        int recoveredToday = cardInfo["dates"][CodeUtils.GetCurrentDate()]["countries"][CodeUtils.UppercaseFirst(countryName)]["today_new_recovered"];
        int recoveredTotal = cardInfo["dates"][CodeUtils.GetCurrentDate()]["countries"][CodeUtils.UppercaseFirst(countryName)]["today_recovered"];
        CaseDetails caseDetail = new CaseDetails(covidToday, deadToday, recoveredTotal, recoveredToday, covidTotal, deadTotal, countryName);
        aCallback(false, caseDetail);
    }

    public Coroutine GetSingle(GameObject loadingGo, string countryName, System.Action<bool, CaseDetails> aCallback)
    {
        return m_Instance.StartCoroutine(GetSingleCase(loadingGo, countryName, aCallback));
    }

    public IEnumerator GetInfoByItalyRegion(string region, System.Action<bool, Case> aCallback)
    {
        string path = "https://api.covid19tracking.narrativa.com/api/" + CodeUtils.GetCurrentDate() + "/country/italy/region/" + region;
        UnityWebRequest regionInfoRequest = UnityWebRequest.Get(path);
        var operation = regionInfoRequest.SendWebRequest();
        //wait loading
        yield return CodeUtils.ProgressBar(operation);
        if(regionInfoRequest.isNetworkError || regionInfoRequest.isHttpError)
        {
            aCallback(false, new CaseDetails());
            Debug.Log(regionInfoRequest.error);
            yield break;
        }
        //Stop loading
        JSONNode regionInfoText = JSON.Parse(regionInfoRequest.downloadHandler.text);
        int deaths = regionInfoText["dates"][CodeUtils.GetCurrentDate()]["countries"]["Italy"]["regions"][0]["today_deaths"];
        int positives = regionInfoText["dates"][CodeUtils.GetCurrentDate()]["countries"]["Italy"]["regions"][0]["today_confirmed"];
        Case regionCase = new Case( positives, deaths, "italy");
        aCallback(true, regionCase);

    }


    public void ResetCouroutines()
    {
        StopAllCoroutines();
    }

}

[System.Serializable]
public class CountryName
{
   public string name;
}

[System.Serializable]
public class CountryInfoList
{
    public List<CountryName> items;

}
