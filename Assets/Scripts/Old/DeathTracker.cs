using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTracker : MonoBehaviour
{
    public string format = "Deaths: {0}";

    public int deaths { get; private set; }

    public void Increment() => deaths++;
    public void Reset() => deaths = 0;

    private TMPro.TextMeshProUGUI _text;

    public void Start()
    {
        deaths = 0;
        _text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void Update()
    {
        _text.text = string.Format(format, deaths);
    }
}
