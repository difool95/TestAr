using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class CodeUtils : MonoBehaviour
{
    public static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static string GetCurrentDate()
    {
        string day = System.DateTime.Now.ToString("yyyy-MM-dd");
        return day;
    }

    //public static IEnumerator ProgressBar(UnityWebRequestAsyncOperation operation, Image loadingsprite)
    //{
    //    while (!operation.isDone)
    //    {
    //      //  loadingsprite.fillAmount += Time.deltaTime * 0.8f;
    //        yield return null;
    //    }
    //}

    public static IEnumerator ProgressBar(UnityWebRequestAsyncOperation operation)
    {
        while (!operation.isDone)
        {
            //  loadingsprite.fillAmount += Time.deltaTime * 0.8f;
            yield return null;
        }
    }

    public static IEnumerator ChronoOnTextUI(float FinalNumber, Text numberText)
    {
        float currentNumber = 0;
        while(currentNumber < FinalNumber)
        {
            currentNumber += 1000;
            yield return null;
            if (currentNumber > FinalNumber)
                currentNumber = FinalNumber;
            numberText.text = currentNumber.ToString();
        }
    }

   public static void CountRows(GridLayoutGroup gridlayoutGroup, out int m_NumOfRow)
    {
        Vector3 firstElementPosition = gridlayoutGroup.transform.GetChild(0).position;
        m_NumOfRow = 1;
        for(int i = 0; i<gridlayoutGroup.transform.childCount; i ++)
        {
            if (!Mathf.Approximately(firstElementPosition.y, gridlayoutGroup.transform.GetChild(i).localPosition.y))
            {
                m_NumOfRow++;
                firstElementPosition = gridlayoutGroup.transform.GetChild(i).localPosition;
            }
        }
    }

}
