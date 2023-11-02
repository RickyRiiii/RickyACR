namespace Ricky;

public class RickyOptions
{
    public static RickyOptions Instance = new();

    public void Reset()
    {
        //Instance = new();
        HealthPercentOf30Reduce = 80;
        HealthPercentOfArmsLength = 80;
        HealthPercentOfLowBlow = 100;
        HealthPercentOfRampart = 80;
        HealthPercentOfReprisal = 80;
        HealthPercentOfInvencible = 15;

        HealthPercentOfBulwark = 80;
        HealthPercentOfDivineVeil = 95;
        HealthPercentOfCover = 30;
        HealthPercentOfSheltron = 90;
}

    public bool UseAOE = true;
    public bool Useyuanc = false;
    public bool UseBrotherhood = true;
    public bool UseBisha = true;
    public bool UseOnslaught = true;
    public bool UseInfuriate = true;
    public bool UseWind = true;
    public bool UseTrueNorth = true;
    public bool FirstTime1 = true;
    public bool FirstTime2 = true;
    public bool UseBloodBath = true;
    public bool UseSecondWind = true;
    public bool Hongzhan = false;
    public bool FirstMHBL = false;
    public bool QuickBrotherhood = false;
    public bool UsePosui = true;
    public bool MustLunar = false;
    public bool UseDqz = true;
    public bool UseDouqi = true;
    public bool FirstDongluan = false;
    public int BrustMode = 0;
    public int SolarMode = 0;
    public int WarStart = 1;
    public int WarBurst = 1;
    public int LunarTime = 0;
    public int MonkMod = 0;
    public int MonkStep = 0;

    public int HealthPercentOf30Reduce = 80;
    public int HealthPercentOfArmsLength = 80;
    public int HealthPercentOfLowBlow = 100;
    public int HealthPercentOfRampart = 80;
    public int HealthPercentOfReprisal = 80;
    public int HealthPercentOfInvencible = 15;

    public int HealthPercentOfBulwark = 80;
    public int HealthPercentOfDivineVeil = 95;
    public int HealthPercentOfCover = 30;
    public int HealthPercentOfSheltron = 90;

    public bool FirstFire = false;
}