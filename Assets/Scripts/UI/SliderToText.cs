using UnityEngine;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private Text targetText = null;

    private void Start()
    {
        if(slider == null)
            slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(TextUpdate);
    }

    private void TextUpdate(float value)
    {
        targetText.text = value.ToString();
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveAllListeners();
    }
}
