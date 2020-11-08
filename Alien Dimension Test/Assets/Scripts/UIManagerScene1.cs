using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerScene1 : MonoBehaviour
{
    public Button arSceneBtn;
    public GameObject cardPrefab;
    public Canvas canvas;

    private void Awake()
    {
       // layoutGroupRectTransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //create prefabs and set flags
        CreateCards();
        /////
        arSceneBtn.onClick.AddListener(() =>
        {
            CoronaApiService.m_Instance.StopAllCoroutines();
            StopAllCoroutines();
            ScenePersister._instance.LoadScene(2);
        });
       // SetScrollViewHeight();

    }

    void CreateCards()
    {
        int index = 0;
        foreach (var card in CoronaApiService.m_Instance.GetCardsNames())
        {
            GameObject cardInstantiated = Instantiate(cardPrefab);
            cardInstantiated.transform.SetParent(transform);
            cardInstantiated.transform.GetChild(0).GetComponent<RawImage>().texture = Resources.Load<Texture>("Flags/" + card.name);
            StartCoroutine(GetCases(gameObject, card.name, index));
            index++;
        }
    }

    IEnumerator GetCases(GameObject cardsGroup, string name, int index)
    {
        cardsGroup.transform.GetChild(index).GetChild(9).gameObject.SetActive(true);
        yield return CoronaApiService.m_Instance.GetList(cardsGroup, name, index, (success, newCase) =>
        {
            if (success)
            {
                cardsGroup.transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() =>
                {
                    //SEND DATA TO PERSIST TO THE OTHER SCENE
                    ScenePersister._instance.countryName = name;
                    CoronaApiService.m_Instance.StopAllCoroutines();
                    StopAllCoroutines();
                    ScenePersister._instance.LoadScene(1);
                });
                cardsGroup.transform.GetChild(index).GetChild(9).gameObject.SetActive(false);
                SetDetailUI(newCase, index);
            }
        });
    }

    void SetDetailUI(Case newCase, int index)
    {
        gameObject.transform.GetChild(index).GetChild(8).GetChild(0).GetComponent<Text>().text = newCase.countryName;
        StartCoroutine(CodeUtils.ChronoOnTextUI(newCase.covidNumber, gameObject.transform.GetChild(index).GetChild(4).GetComponent<Text>()));
        StartCoroutine(CodeUtils.ChronoOnTextUI(newCase.deadNumber, gameObject.transform.GetChild(index).GetChild(7).GetComponent<Text>()));
    }


    void SetReferenceResolution(Vector2 referenceReso, float matchWidthOrHeight = 1f)
    {
        const float klogBase = 2;
        float logWidth = Mathf.Log(Screen.width / referenceReso.x, klogBase);
        float logHeight = Mathf.Log(Screen.height / referenceReso.y, klogBase);
        float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, matchWidthOrHeight);
        canvas.scaleFactor = Mathf.Pow(klogBase, logWeightedAverage);
    }

//void SetScrollViewHeight()
//    {
//        switch (Screen.width)
//        {
//            case 800:
//                layoutGroupRectTransform.sizeDelta = new Vector2(layoutGroupRectTransform.sizeDelta.x, (450 + 181) * 38 + 200);
//                break;
//            case 1280:
//                layoutGroupRectTransform.sizeDelta = new Vector2(layoutGroupRectTransform.sizeDelta.x, (450 + 181) * 38 / 2 + 200);
//                break;
//            case 1920:
//                layoutGroupRectTransform.sizeDelta = new Vector2(layoutGroupRectTransform.sizeDelta.x, (450 + 181) * 38 / 4 + 200);
//                break;
//            case 2160:
//                layoutGroupRectTransform.sizeDelta = new Vector2(layoutGroupRectTransform.sizeDelta.x, (450 + 181) * 38 / 4 + 200);
//                break;
//            case 2560:
//                layoutGroupRectTransform.sizeDelta = new Vector2(layoutGroupRectTransform.sizeDelta.x, (450 + 181) * 38 / 5 + 200);
//                break;
//            case 2960:
//                layoutGroupRectTransform.sizeDelta = new Vector2(layoutGroupRectTransform.sizeDelta.x, (450 + 181) * 38 / 6 + 200);
//                break;

//            default:
//                layoutGroupRectTransform.sizeDelta = new Vector2(layoutGroupRectTransform.sizeDelta.x, (450 + 181) * 38  + 200);
//                break;
//        }
//    }


}
