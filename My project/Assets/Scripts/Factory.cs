using UnityEngine;

[System.Serializable]
public class Factory
{
    // 
    public string factoryName;
    public int level;

    public Vector3 position;  

    // Productivity
    public float productivityPerSecond;
    public float productivityPerClick;

    // Price
    public float purchasePrice;
    public float upgradePrice;

    // Bonus
    public string[] bonuses;



    public Factory(string name, float price, float upgradePrice, float productivityPerSecond, float productivityPerClick, Vector3 position)
    {
        this.upgradePrice = upgradePrice;
        factoryName = name;
        purchasePrice = price;
        this.productivityPerSecond = productivityPerSecond;
        this.productivityPerClick = productivityPerClick;
        level = 1;
        this.position = position;
    }

    public void UpgradeFactory()
    {
        if (CanUpgrade())
        {
            level++;
            productivityPerSecond *= 1.2f;
            productivityPerClick *= 1.2f;
            purchasePrice *= 1.5f;
            upgradePrice *= 1.5f;
            AddBonus("Bonus level " + level);
        }
    }

    public bool CanUpgrade()
    {
        return true;
    }

    public void AddBonus(string bonus)
    {
        string[] newBonuses = new string[bonuses.Length + 1];
        for (int i = 0; i < bonuses.Length; i++)
        {
            newBonuses[i] = bonuses[i];
        }
        newBonuses[bonuses.Length] = bonus;
        bonuses = newBonuses;
    }
}
