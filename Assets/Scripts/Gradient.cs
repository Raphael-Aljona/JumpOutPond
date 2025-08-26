using UnityEngine;
using UnityEngine.UI;

public class Gradient : MonoBehaviour
{
    public Slider powerSlider;
    public Image fillImage;
    public Gradient gradient;

    void Update()
    {
        if (powerSlider != null && fillImage != null)
        {
            //fillImage.color = gradient.Evaluate(powerSlider.value);
        }
    }
}
