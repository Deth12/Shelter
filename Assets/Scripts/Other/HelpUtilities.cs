
public static class HelpUtilities
{
    public static string GetYearsString(int years)
    {
        if (years <= 10 || years >= 15)
            switch (years % 10)
            {
                case 1:
                    return years + " год";
                case 2:
                case 3:
                case 4:
                    return years + " года";
                default:
                    return years + " лет";
            }
        return years + " лет";
    }

    public static string GetMonthsString(int months)
    {
        switch (months)
        {
            case 1:
                return months + " месяц";
            case 3:
                return months + " месяца";
            default:
                return months + " месяцев";
        }
    }

    public static string GetYearMonthsString(int months)
    {
        int y = months / 12;
        int m = months % 12;
        return 
            y != 0 ? 
                m != 0 ? 
                    $"{GetYearsString(y)}, {GetMonthsString(m)}" 
                    : $"{GetYearsString(y)}"
                : $"{GetMonthsString(m)}";
    }
}
