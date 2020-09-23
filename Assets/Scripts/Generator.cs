using System.Collections.Generic;
using System.Linq;
using System.Data;
using UnityEngine;


public static class Generator
{
    public static List<Sprite> menAvatars = new List<Sprite>();
    public static List<Sprite> womenAvatars = new List<Sprite>();

    static Generator()
    {
        Sprite[] avatarsAtlas = Resources.LoadAll<Sprite>("Avatars");
        menAvatars.AddRange(avatarsAtlas.Where(s => s.name.StartsWith("men")));
        womenAvatars.AddRange(avatarsAtlas.Where(s => s.name.StartsWith("women")));
    }
    
    public static PlayerCardInfo GeneratePlayerCard()
    {
        PlayerCardInfo p = new PlayerCardInfo();
        p.Job = GenerateJob();
        p.Age = Random.Range(18, 80);
        p.Body = new BodyInfo(Random.Range(150, 200), Random.Range(40, 140));
        p.Gender = Random.Range(0, 100) > 50 ? "Женщина" : "Мужчина";
        p.Avatar = p.Gender == "Мужчина"
            ? menAvatars[Random.Range(0, menAvatars.Count)]
            : womenAvatars[Random.Range(0, womenAvatars.Count)];
        p.IsChildfree = Random.Range(0, 100) > 50 ? true : false;
        p.Health = GenerateHealth();
        p.Character = GenerateCharacter();
        p.Phobia = GeneratePhobia();
        p.Hobby = GenerateHobby();
        p.Info = GenerateInfo();
        p.Inventory = GenerateInventory();
        p.FirstSpecial = GenerateSpecial();
        p.SecondSpecial = GenerateSpecial();
        return p;
    }

    public static CatastropheCardInfo GenerateCatastrophy()
    {
        CatastropheCardInfo c = new CatastropheCardInfo();
        (string title, string description) ci = GenerateCatastrophyInfo();
        c.Title = ci.title;
        c.Description = ci.description;
        c.DestructionPercent = Random.Range(.02f, .6f);
        c.PopulationLeft = Random.Range(.05f, .3f);
        return c;
    }

    public static BunkerCardInfo GenerateBunker()
    {
        BunkerCardInfo b = new BunkerCardInfo();
        int[] areas = { 40, 75, 85, 90, 120, 145, 150, 160, 180, 200, 250};
        b.Area = areas[Random.Range(0, areas.Length)];
        b.TimeToStay = Random.Range(1, 37);
        b.Provision = GenerateProvision();
        b.SecurityStatus = GenerateSecurityStatus();
        (string f1, string f2, string f3) facilities = GenerateFacilities();
        b.FirstFacility = facilities.f1;
        b.SecondFacility = facilities.f2;
        b.ThirdFacility = facilities.f3;
        (string w1, string w2, string w3) warehouse = GenerateWarehouse();
        b.FirstItem = warehouse.w1;
        b.SecondItem = warehouse.w2;
        b.ThirdItem = warehouse.w3;
        b.Pests = GeneratePests();
        return b;
    }
    
    #region Players Generators

    private static string GenerateJob()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM job ORDER BY RANDOM() LIMIT 1");
    }
    
    private static string GenerateHealth()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM health ORDER BY RANDOM() LIMIT 1");
    }
    
    private static string GenerateCharacter()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM character ORDER BY RANDOM() LIMIT 1");
    }
    
    private static string GeneratePhobia()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM phobia ORDER BY RANDOM() LIMIT 1");
    }
    
    private static string GenerateHobby()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM hobby ORDER BY RANDOM() LIMIT 1");
    }
    
    private static string GenerateInfo()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM hobby ORDER BY RANDOM() LIMIT 1");
    }
    
    private static string GenerateInventory()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM inventory ORDER BY RANDOM() LIMIT 1");
    }
    
    private static string GenerateSpecial()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM special ORDER BY RANDOM() LIMIT 1");
    } 

    #endregion

    #region Catastrophy Generators

    private static (string, string) GenerateCatastrophyInfo()
    {
        DataTable dt = DatabaseAccess.GetTable("SELECT * FROM catastrophe ORDER BY RANDOM() LIMIT 1");
        return (dt.Rows[0]["title"].ToString(), dt.Rows[0]["description"].ToString());
    }


    #endregion

    #region Bunker Generators

    private static string GenerateProvision()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM provision ORDER BY RANDOM() LIMIT 1");
    }
    
    private static string GenerateSecurityStatus()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM security ORDER BY RANDOM() LIMIT 1");
    }

    private static (string, string, string) GenerateFacilities()
    {
        DataTable dt = DatabaseAccess.GetTable("SELECT * FROM facility ORDER BY RANDOM() LIMIT 3");
        return (dt.Rows[0][0].ToString(), dt.Rows[1][0].ToString(), dt.Rows[2][0].ToString());
    }
    
    private static (string, string, string) GenerateWarehouse()
    {
        DataTable dt = DatabaseAccess.GetTable("SELECT * FROM warehouse ORDER BY RANDOM() LIMIT 3");
        return (dt.Rows[0][0].ToString(), dt.Rows[1][0].ToString(), dt.Rows[2][0].ToString());
    }
    
    private static string GeneratePests()
    {
        return DatabaseAccess.ExecuteQueryWithAnswer("SELECT * FROM pest ORDER BY RANDOM() LIMIT 1");
    }
    

    #endregion
}