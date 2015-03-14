using UnityEngine;
using System.Collections;

public class WeaponDict
{
    public static float GetDamage(HeadGun.PrefabOption prefab, int level)
    {
        var result = 0f;
        var index = headGunDict[prefab];
        result = GetDamage(level, index);
        return result;
    }

    private static float GetDamage(int level, int index)
    {
        var result = 0f;
        if (0 <= level && level <= damageTable.GetUpperBound(1))
        {
            result = damageTable[index, level];
        }
        else if (level < 0)
        {
            result = damageTable[index, 0];
        }
        else
        {
            result = damageTable[index, damageTable.GetUpperBound(1)];
        }
        return result;
    }

    public static float GetDamage(EnemyModel.PrefabOption prefab, int level)
    {
        var result = 0f;
        var index = enemyModelDict[prefab];
        result = GetDamage(level, index);
        return result;
    }

    public static float GetReloadTime(HeadGun.PrefabOption prefab, int level)
    {
        var result = 0f;
        var index = headGunDict[prefab];
        result = GetReloadTime(level, index);
        return result;
    }

    private static float GetReloadTime(int level, int index)
    {
        var result = 0f;
        if (0 <= level && level <= reloadTimeTable.GetUpperBound(1))
        {
            result = reloadTimeTable[index, level];
        }
        else if (level < 0)
        {
            result = reloadTimeTable[index, 0];
        }
        else
        {
            result = reloadTimeTable[index, reloadTimeTable.GetUpperBound(1)];
        }
        return result;
    }

    public static float GetReloadTime(EnemyModel.PrefabOption prefab, int level)
    {
        var result = 0f;
        var index = enemyModelDict[prefab];
        result = GetReloadTime(level, index);
        return result;
    }

    private static readonly System.Collections.Generic.Dictionary<HeadGun.PrefabOption, int> headGunDict;
    private static readonly System.Collections.Generic.Dictionary<EnemyModel.PrefabOption, int> enemyModelDict;
    private static readonly float[,] damageTable;
    private static readonly float[,] reloadTimeTable;
    static WeaponDict()
    {
        headGunDict = new System.Collections.Generic.Dictionary<HeadGun.PrefabOption, int>();
        {
            headGunDict.Add(HeadGun.PrefabOption.HeadGun_Canon, 0);
            headGunDict.Add(HeadGun.PrefabOption.HeadGun_Minigun, 1);
            headGunDict.Add(HeadGun.PrefabOption.HeadGun_Rocket, 2);
            headGunDict.Add(HeadGun.PrefabOption.HeadGun_Lightning, 3);
            headGunDict.Add(HeadGun.PrefabOption.HeadGun_Firethrower, 4);
            headGunDict.Add(HeadGun.PrefabOption.HeadGun_ShockGun, 5);
        }

        enemyModelDict = new System.Collections.Generic.Dictionary<EnemyModel.PrefabOption, int>();
        {
            enemyModelDict.Add(EnemyModel.PrefabOption.Boss1, 0);
            enemyModelDict.Add(EnemyModel.PrefabOption.Boss2, 4);
            enemyModelDict.Add(EnemyModel.PrefabOption.Tank_Bron, 0);
            enemyModelDict.Add(EnemyModel.PrefabOption.Tank_DarkBoss, 0);
            enemyModelDict.Add(EnemyModel.PrefabOption.Tank_Easy, 0);
            enemyModelDict.Add(EnemyModel.PrefabOption.Tank_Electro, 3);
            enemyModelDict.Add(EnemyModel.PrefabOption.Tank_Fire, 4);
            enemyModelDict.Add(EnemyModel.PrefabOption.Tank_Hard, 0);
            enemyModelDict.Add(EnemyModel.PrefabOption.Tank_Normal, 0);
            enemyModelDict.Add(EnemyModel.PrefabOption.Tank_Nuc, 0);
            enemyModelDict.Add(EnemyModel.PrefabOption.Tank_Raket, 2);
            enemyModelDict.Add(EnemyModel.PrefabOption.TankRotGun, 1);
        }
        reloadTimeTable = new float[headGunDict.Count, 5];
        damageTable = new float[headGunDict.Count, 5];

        InitializeReloadTimeTable();
        InitializeDamageTable();
    }

    private static void InitializeDamageTable()
    {
        var headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Canon];
        damageTable[headGunIndex, 0] = 20;
        damageTable[headGunIndex, 1] = 24;
        damageTable[headGunIndex, 2] = 28;
        damageTable[headGunIndex, 3] = 32;
        damageTable[headGunIndex, 4] = 34;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Minigun];
        damageTable[headGunIndex, 0] = 15;
        damageTable[headGunIndex, 1] = 18;
        damageTable[headGunIndex, 2] = 21;
        damageTable[headGunIndex, 3] = 24;
        damageTable[headGunIndex, 4] = 25;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Rocket];
        damageTable[headGunIndex, 0] = 08;
        damageTable[headGunIndex, 1] = 09;
        damageTable[headGunIndex, 2] = 11;
        damageTable[headGunIndex, 3] = 12;
        damageTable[headGunIndex, 4] = 13;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Lightning];
        damageTable[headGunIndex, 0] = 11;
        damageTable[headGunIndex, 1] = 13;
        damageTable[headGunIndex, 2] = 15;
        damageTable[headGunIndex, 3] = 17;
        damageTable[headGunIndex, 4] = 18;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Firethrower];
        damageTable[headGunIndex, 0] = 09;
        damageTable[headGunIndex, 1] = 10;
        damageTable[headGunIndex, 2] = 12;
        damageTable[headGunIndex, 3] = 14;
        damageTable[headGunIndex, 4] = 15;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_ShockGun];
        damageTable[headGunIndex, 0] = 15;
        damageTable[headGunIndex, 1] = 18;
        damageTable[headGunIndex, 2] = 21;
        damageTable[headGunIndex, 3] = 24;
        damageTable[headGunIndex, 4] = 25;
    }

    private static void InitializeReloadTimeTable()
    {
        var headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Canon];
        reloadTimeTable[headGunIndex, 0] = 0.75f;
        reloadTimeTable[headGunIndex, 1] = 0.65f;
        reloadTimeTable[headGunIndex, 2] = 0.59f;
        reloadTimeTable[headGunIndex, 3] = 0.5f;
        reloadTimeTable[headGunIndex, 4] = 0.43f;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Minigun];
        reloadTimeTable[headGunIndex, 0] = 0.25f;
        reloadTimeTable[headGunIndex, 1] = 0.21f;
        reloadTimeTable[headGunIndex, 2] = 0.18f;
        reloadTimeTable[headGunIndex, 3] = 0.15f;
        reloadTimeTable[headGunIndex, 4] = 0.12f;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Rocket];
        reloadTimeTable[headGunIndex, 0] = 0.56f;
        reloadTimeTable[headGunIndex, 1] = 0.50f;
        reloadTimeTable[headGunIndex, 2] = 0.43f;
        reloadTimeTable[headGunIndex, 3] = 0.37f;
        reloadTimeTable[headGunIndex, 4] = 0.31f;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Lightning];
        reloadTimeTable[headGunIndex, 0] = 1.25f;
        reloadTimeTable[headGunIndex, 1] = 1.12f;
        reloadTimeTable[headGunIndex, 2] = 1.00f;
        reloadTimeTable[headGunIndex, 3] = 0.87f;
        reloadTimeTable[headGunIndex, 4] = 0.75f;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_Firethrower];
        reloadTimeTable[headGunIndex, 0] = 0.12f;
        reloadTimeTable[headGunIndex, 1] = 0.09f;
        reloadTimeTable[headGunIndex, 2] = 0.09f;
        reloadTimeTable[headGunIndex, 3] = 0.06f;
        reloadTimeTable[headGunIndex, 4] = 0.06f;
        headGunIndex = headGunDict[HeadGun.PrefabOption.HeadGun_ShockGun];
        reloadTimeTable[headGunIndex, 0] = 0.56f;
        reloadTimeTable[headGunIndex, 1] = 0.50f;
        reloadTimeTable[headGunIndex, 2] = 0.43f;
        reloadTimeTable[headGunIndex, 3] = 0.37f;
        reloadTimeTable[headGunIndex, 4] = 0.31f;
    }


}