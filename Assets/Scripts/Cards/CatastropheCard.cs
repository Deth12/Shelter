using UnityEngine;
using UnityEngine.UI;

public class CatastropheCard : MonoBehaviour
{
    [SerializeField] private Text titleField = null;
    [SerializeField] private Text descriptionField = null;
    [SerializeField] private Text populationField = null;
    [SerializeField] private Text destructionField = null;

    public void FillCatastropheCard(CatastropheCardInfo ci)
    {
        titleField.text = ci.Title;
        descriptionField.text = ci.Description;
        populationField.text = ci.PopulationLeft.ToString("P0");
        destructionField.text = ci.DestructionPercent.ToString("P0");
    }
}

public struct CatastropheCardInfo
{
    public string Title;
    public string Description;
    public float PopulationLeft;
    public float DestructionPercent;
}
