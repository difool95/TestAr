using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenePersister : MonoBehaviour
{
    public static ScenePersister _instance;
    [HideInInspector]
    public string countryName;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
