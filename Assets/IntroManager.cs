using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    // can ignore the update, it's just to make the coroutines get called for example

    [SerializeField] Image image = null;
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] Slider slider = null;
    [SerializeField] TextMeshProUGUI text_percentage = null;

    private float time_loading = 6;
    private float time_current;
    private float time_start;

    void Start()
    {
        time_current = time_loading;
        time_start = Time.time;
        Set_FillAmount(0);
        StartCoroutine(FadeTextToFullAlpha(1.5f, image, text));
    }

    void Update()
    {
        Check_Loading();
    }

    public IEnumerator FadeTextToFullAlpha(float t, Image i, TextMeshProUGUI j)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        j.color = new Color(j.color.r, j.color.g, j.color.b, 0);

        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }

        while (j.color.a < 1.0f)
        {
            j.color = new Color(j.color.r, j.color.g, j.color.b, j.color.a + (Time.deltaTime / t));
            yield return null;
        }
        j.color = new Color(j.color.r, j.color.g, j.color.b, 1);
        while (j.color.a > 0.0f)
        {
            j.color = new Color(j.color.r, j.color.g, j.color.b, j.color.a - (Time.deltaTime / t));
            yield return null;
        }

    }

    private void Check_Loading()
    {
        time_current = Time.time - time_start;
        if (time_current < time_loading)
        {
            Set_FillAmount(time_current / time_loading);
        }
        else
        {
            End_Loading();
        }
    }

    private void End_Loading()
    {
        Set_FillAmount(1);
        SceneManager.LoadScene("Tab menu");
    }

    private void Set_FillAmount(float _value)
    {
        slider.value = _value;
        string txt = (_value.Equals(1) ? "Finished.. " : "Loading.. ") + (_value).ToString("P");
        text_percentage.text = txt;
    }

}