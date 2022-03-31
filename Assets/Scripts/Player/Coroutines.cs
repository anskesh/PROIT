using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coroutines: MonoBehaviour
{
    public IEnumerator ShowText(string text)
    {
        var info = GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>();
        info.text = "Требуется " + text;
        yield return new WaitForSecondsRealtime(1f);
        info.text = "";
    }
}
