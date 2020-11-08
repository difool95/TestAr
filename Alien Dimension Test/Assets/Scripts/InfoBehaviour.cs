using System.Collections;
using TMPro;
using UnityEngine;

public class InfoBehaviour : MonoBehaviour
{
    float speed = 6f;
    private Transform sectionInfo;
    Vector3 desiredScale;
    Vector3 desiredPosition;
    string regionName;


    private void Awake()
    {
        regionName = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshPro>().text;
        desiredPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        desiredScale = new Vector3(0.003f, 0.003f, 0.003f);
        sectionInfo = transform.GetChild(0);
    }
    private void Start()
    {
        StartCoroutine(SetInfos(regionName));
    }

    IEnumerator SetInfos(string region)
    {
        yield return CoronaApiService.m_Instance.GetInfoByItalyRegion(region, (success, regionCase) =>
        {
            if (success)
            {
                transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<TextMeshPro>().text = regionCase.deadNumber.ToString();
                transform.GetChild(0).GetChild(0).GetChild(5).GetComponent<TextMeshPro>().text = regionCase.covidNumber.ToString();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        sectionInfo.localScale = Vector3.Lerp(sectionInfo.localScale, desiredScale, Time.deltaTime * speed);
        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition, Time.deltaTime * speed);
    }
    public void OpenInfo()
    {
        desiredScale = new Vector3(0.01666666f, 0.01666666f, 0.01666666f);
        desiredPosition = new Vector3(transform.localPosition.x, -1, transform.localPosition.z);
    }

    public void CloseInfo()
    {
        desiredScale = new Vector3(0.003f, 0.003f, 0.003f);
        desiredPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
    }

}
