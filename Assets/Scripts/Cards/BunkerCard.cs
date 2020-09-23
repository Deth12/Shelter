using UnityEngine;
using UnityEngine.UI;

public class BunkerCard : MonoBehaviour
{
    // General Info
    [SerializeField] private Text areaField = null;
    [SerializeField] private Text timeToStayField = null;
    [SerializeField] private Text provisionField = null;
    [SerializeField] private Text securityStatusField = null;
    
    // Facilities
    [SerializeField] private Text firstFacilityField = null;
    [SerializeField] private Text secondFacilityField = null;
    [SerializeField] private Text thirdFacilityField = null;

    // Warehouse
    [SerializeField] private Text firstItemField = null;
    [SerializeField] private Text secondItemField = null;
    [SerializeField] private Text thirdItemField = null;
    
    // Other
    [SerializeField] private Text pestsStatus = null;

    public void FillBunkerCard(BunkerCardInfo bi)
    {
        areaField.text = $"{bi.Area} квадратных метра";
        timeToStayField.text = HelpUtilities.GetYearMonthsString(bi.TimeToStay);
        /*
        timeToStayField.text = $"{HelpUtilities.GetYearsString(bi.TimeToStay / 12)} " +
                               $"{HelpUtilities.GetMonthsString(bi.TimeToStay % 12)}";
        */
        provisionField.text = bi.Provision;
        securityStatusField.text = bi.SecurityStatus;

        firstFacilityField.text = bi.FirstFacility;
        secondFacilityField.text = bi.SecondFacility;
        thirdFacilityField.text = bi.ThirdFacility;

        firstItemField.text = bi.FirstItem;
        secondItemField.text = bi.SecondItem;
        thirdItemField.text = bi.ThirdItem;

        pestsStatus.text = bi.Pests;
    }
}

public struct BunkerCardInfo
{
    public float Area;
    public int TimeToStay;
    public string Provision;
    public string SecurityStatus;
    
    public string FirstFacility;
    public string SecondFacility;
    public string ThirdFacility;
    
    public string FirstItem;
    public string SecondItem;
    public string ThirdItem;

    public string Pests;
}
