using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private Image avatar = null;
    
    [SerializeField] private Text genderAndAgeField = null;
    [SerializeField] private Text jobField = null;
    [SerializeField] private Text childField = null;
    
    // Body field
    [SerializeField] private Text heightField = null;
    [SerializeField] private Text weightField = null;
    [SerializeField] private Text bmiField = null;
    [SerializeField] private Text overallField = null;

    [SerializeField] private Text healthField = null;
    [SerializeField] private Text characterField = null;
    [SerializeField] private Text phobiaField = null;
    [SerializeField] private Text hobbyField = null;
    [SerializeField] private Text infoField = null;
    [SerializeField] private Text inventoryField = null;
    [SerializeField] private Text firstSpecialField = null;
    [SerializeField] private Text secondSpecialField = null;

    public void FillPlayerCard(PlayerCardInfo pi)
    {
        // General
        avatar.sprite = pi.Avatar;
        genderAndAgeField.text = $"{pi.Gender}, {HelpUtilities.GetYearsString(pi.Age)}";
        jobField.text = pi.Job;
        
        // Body field
        heightField.text = pi.Body.height.ToString();
        weightField.text = pi.Body.weight.ToString();
        bmiField.text = pi.Body.bmi.ToString("0.00");
        overallField.text = pi.Body.overall;

        // Additional info
        childField.text = pi.IsChildfree ? "Childfree" : "Не Childfree";
        healthField.text = pi.Health;
        characterField.text = pi.Character;
        phobiaField.text = pi.Phobia;
        hobbyField.text = pi.Hobby;
        infoField.text = pi.Info;
        inventoryField.text = pi.Inventory;
        
        // Specials
        firstSpecialField.text = pi.FirstSpecial;
        secondSpecialField.text = pi.SecondSpecial;
    }
}

public struct PlayerCardInfo
{
    public Sprite Avatar;
    public string Gender;
    public int Age;
    public string Job;

    public BodyInfo Body;
    public bool IsChildfree;
    public string Health;
    public string Character;
    public string Phobia;
    public string Hobby;
    public string Info;
    public string Inventory;
    
    public string FirstSpecial;
    public string SecondSpecial;
}

public class BodyInfo
{
    public int height;
    public int weight;
    public float bmi => weight / Mathf.Pow((height * 0.01f), 2);

    public string overall
    {
        get
        {
            if (bmi <= 16f)
                return "Сильный дефицит МТ";
            else if (bmi > 16 && bmi <= 18.5f)
                return "Недостаточная МТ";
            else if (bmi > 18.5f && bmi <= 25f)
                return "Норма";
            else if (bmi > 25 && bmi <= 30f)
                return "Избыточная МТ";
            else if (bmi > 30 && bmi <= 35)
                return "Ожирение";
            else
                return "Сильное ожирение";
        }
    }

    public BodyInfo(int height, int weight)
    {
        this.height = height;
        this.weight = weight;
    }
}
