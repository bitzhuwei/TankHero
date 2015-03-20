using UnityEngine;
using System.Collections;

public class WorkshopConfig 
{
    public int money;
    public HeadGun.PrefabOption currentWeapon;
    public System.Collections.Generic.List<HeadGun.PrefabOption> boughtWeapons = new System.Collections.Generic.List<HeadGun.PrefabOption>();
    public int boughtArmor;
    public int boughtMovementSpeed;
    public int boughtReloadTime;
    public int boughtProjectileSpeed;
    public int boughtDamage;
    public System.Collections.Generic.Dictionary<HeadGun.PrefabOption, int> weaponPriceDict = new System.Collections.Generic.Dictionary<HeadGun.PrefabOption, int>();
    public int[] armorPrices = new int[4];
    public int[] movementSpeedPrices = new int[4];
    public int[] reloadTimePrices = new int[4];
    public int[] projectileSpeedPrices = new int[4];
    public int[] damagePrices = new int[4];

    public override string ToString()
    {
        return string.Format("money: {0}", money);
        //return base.ToString();
    }

    public static WorkshopConfig Parse(string content)
    {
        var result = new WorkshopConfig();
        var array = System.Text.Encoding.ASCII.GetBytes(content);
        using (var mem = new System.IO.MemoryStream(array))
        {
            ParseWorkshopConfig(mem, result);
        }

        return result;
    }

    static readonly char[] separator = new char[] { ':', ';' };
    static readonly char[] comma = new char[] { ',' };
    private static void ParseWorkshopConfig(System.IO.MemoryStream mem, WorkshopConfig result)
    {
        using (var reader = new System.IO.StreamReader(mem))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line.StartsWith(@"//"))
                { continue; }
                var index = line.IndexOf("//");
                if (index > 0)
                { line = line.Substring(0, index); }
                var parts = line.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
                switch (parts[0])
                {
                    case "money":
                        result.money = int.Parse(parts[1]);
                        break;
                    case "currentWeapon":
                        result.currentWeapon = (HeadGun.PrefabOption)System.Enum.Parse(typeof(HeadGun.PrefabOption), parts[1]);
                        break;
                    case "boughtWeapon":
                        var weapons = parts[1].Split(comma, System.StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in weapons)
                        {
                            var weapon = (HeadGun.PrefabOption)System.Enum.Parse(typeof(HeadGun.PrefabOption), item);
                            result.boughtWeapons.Add(weapon);
                        }
                        break;
                    case "boughtArmor":
                        result.boughtArmor = int.Parse(parts[1]);
                        break;
                    case "boughtMovementSpeed":
                        result.boughtMovementSpeed = int.Parse(parts[1]);
                        break;
                    case "boughtReloadTime":
                        result.boughtReloadTime = int.Parse(parts[1]);
                        break;
                    case "boughtProjectileSpeed":
                        result.boughtProjectileSpeed = int.Parse(parts[1]);
                        break;
                    case "boughtDamage":
                        result.boughtDamage = int.Parse(parts[1]);
                        break;
                    case "weaponPrice":
                        {
                            var prices = parts[1].Split(comma, System.StringSplitOptions.RemoveEmptyEntries);
                            result.weaponPriceDict.Add(HeadGun.PrefabOption.HeadGun_Canon, int.Parse(prices[0]));
                            result.weaponPriceDict.Add(HeadGun.PrefabOption.HeadGun_Minigun, int.Parse(prices[1]));
                            result.weaponPriceDict.Add(HeadGun.PrefabOption.HeadGun_Rocket, int.Parse(prices[2]));
                            result.weaponPriceDict.Add(HeadGun.PrefabOption.HeadGun_Lightning, int.Parse(prices[3]));
                            result.weaponPriceDict.Add(HeadGun.PrefabOption.HeadGun_Firethrower, int.Parse(prices[4]));
                            result.weaponPriceDict.Add(HeadGun.PrefabOption.HeadGun_ShockGun, int.Parse(prices[5]));
                        }
                        break;
                    case "armorPrice":
                        {
                            var prices = parts[1].Split(comma, System.StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < result.armorPrices.Length; i++)
                            {
                                result.armorPrices[i] = int.Parse(prices[i]);
                            }
                        }
                        break;
                    case "movementSpeedPrice":
                        {
                            var prices = parts[1].Split(comma, System.StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < result.movementSpeedPrices.Length; i++)
                            {
                                result.movementSpeedPrices[i] = int.Parse(prices[i]);
                            }
                        } break;
                    case "reloadTimePrice":
                        {
                            var prices = parts[1].Split(comma, System.StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < result.reloadTimePrices.Length; i++)
                            {
                                result.reloadTimePrices[i] = int.Parse(prices[i]);
                            }
                        } break;
                    case "projectileSpeedPrice":
                        {
                            var prices = parts[1].Split(comma, System.StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < result.projectileSpeedPrices.Length; i++)
                            {
                                result.projectileSpeedPrices[i] = int.Parse(prices[i]);
                            }
                        } break;
                    case "damagePrice":
                        {
                            var prices = parts[1].Split(comma, System.StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < result.damagePrices.Length; i++)
                            {
                                result.damagePrices[i] = int.Parse(prices[i]);
                            }
                        } break;
                    default:
                        break;
                }
            }
        }
    }

    internal void Save(string relativeFile)
    {
        var builder = new System.Text.StringBuilder();
        builder.AppendLine(string.Format("money:{0};", this.money));
        builder.AppendLine(string.Format("currentWeapon:{0};", this.currentWeapon));
        builder.Append("boughtWeapon:");
        for (int i = 0; i < this.boughtWeapons.Count - 1; i++)
        {
            builder.Append(this.boughtWeapons[i]);
            builder.Append(',');
        }
        builder.Append(this.boughtWeapons[this.boughtWeapons.Count - 1]);
        builder.AppendLine(";");
        builder.AppendLine(string.Format("boughtArmor:{0}", this.boughtArmor));
        builder.AppendLine(string.Format("boughtMovementSpeed:{0}", this.boughtMovementSpeed));
        builder.AppendLine(string.Format("boughtReloadTime:{0}", this.boughtReloadTime));
        builder.AppendLine(string.Format("boughtProjectileSpeed:{0}", this.boughtProjectileSpeed));
        builder.AppendLine(string.Format("boughtDamage:{0}", this.boughtDamage));
        builder.AppendLine("weaponPrice:100,200,300,400,500,600;");
        builder.AppendLine("armorPrice:100,200,400,800;");
        builder.AppendLine("movementSpeedPrice:100,200,400,800;");
        builder.AppendLine("reloadTimePrice:100,200,400,800;");
        builder.AppendLine("projectileSpeedPrice:100,200,400,800;");
        builder.AppendLine("damagePrice:100,200,400,800;");

        FileHelper.Write(relativeFile, builder.ToString());
    }
}
