using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIManagerScene2 : MonoBehaviour
{
    public Button backBtn, arSceneBtn;
    public RawImage flag;
    public Text nameTxt, ctod, ctot, rtod, rtot, dtod, dtot;
    public GameObject loadingGo;
    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(() =>
        {
            ScenePersister._instance.LoadScene(0);
        });
        arSceneBtn.onClick.AddListener(() =>
        {
            ScenePersister._instance.LoadScene(2);
        });
        StartCoroutine(GetCasesDetails());
       // SetReferenceResolution(new Vector2(Screen.width, Screen.height));

    }


    IEnumerator GetCasesDetails()
    {
        yield return CoronaApiService.m_Instance.GetSingle(loadingGo, ScenePersister._instance.countryName, (success, caseDetails) =>
        {
            SetDetailUI(caseDetails);
        });
    }

    void SetDetailUI(CaseDetails caseDetails)
    {
        nameTxt.text = caseDetails.countryName;
        ctod.text = caseDetails.covidToday.ToString();
        ctot.text = caseDetails.covidNumber.ToString();
        rtod.text = caseDetails.recoveredToday.ToString();
        rtot.text = caseDetails.recoveredTotal.ToString();
        dtod.text = caseDetails.deadToday.ToString();
        dtot.text = caseDetails.deadNumber.ToString();
        flag.texture = Resources.Load<Texture>("Flags/" + caseDetails.countryName);

    }

    //void SetReferenceResolution(Vector2 referenceReso, float matchWidthOrHeight = 1f)
    //{
    //    const float klogBase = 2;
    //    float logWidth = Mathf.Log(Screen.width / referenceReso.x, klogBase);
    //    float logHeight = Mathf.Log(Screen.height / referenceReso.y, klogBase);
    //    float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, matchWidthOrHeight);
    //    canvas.scaleFactor = Mathf.Pow(klogBase, logWeightedAverage);
    //}

}
