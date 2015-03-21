using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OriginalMap
{
    public List<Transform> respawnList = new List<Transform>();

    public int width;
    public int height;
    public int signedTankCount;
    public List<string> tankList = new List<string>();
    /// <summary>
    /// type [6] [Terr_6] [NWF]
    /// </summary>
    public Dictionary<int, TerrObstacleEntry> terrPrefabDict = new Dictionary<int, TerrObstacleEntry>();
    public List<string> wallList = new List<string>();
    /// <summary>
    /// [6] [N125]
    /// </summary>
    public TerrEntry[,] terrTable;
    /// <summary>
    /// [6] [N125]
    /// </summary>
    public TerrEntry[,] terr2Table;
    /// <summary>
    /// texture [6] "[TERR]" LoadFile "...[Terr_Forest_Pac].jpg" 
    /// </summary>
    public Dictionary<string, TextureEntry> textureDict = new Dictionary<string, TextureEntry>();
    public List<EventObjEntry> eventObjList = new List<EventObjEntry>();

    public List<GameObject> Build()
    {
        var result = new List<GameObject>();
        BuildTerrTable(terrTable, "TERR", result);
        BuildTerrTable(terr2Table, "TERR2", result);
        BuildEventObjList(this.eventObjList, result);
        BuildFlagObj(this.flag, result);
        BuildWall(this.wallList, result);
        BuildRespawnList(this.respawnList, result);
        return result;
    }

    private void BuildRespawnList(List<Transform> list, List<GameObject> result)
    {
        foreach (var item in list)
        {
            var gameObj = new GameObject(item.name);// ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "item_airBomb");
            result.Add(gameObj);
            gameObj.transform.position = item.position;
        }
    }

    private void BuildWall(List<string> list, List<GameObject> result)
    {
        var manager = new BrickMaterialManager();
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[i].Length; j++)
            {
                var c = list[i][j];
                if (c == ' ') { continue; }
                //Debug.LogError(string.Format("brick type: {0}", c));
                var mat = string.Empty;
                switch (c)
                {
                    case '#':
                        mat = textureDict["WALLB0"].textureName;
                        break;
                    case '&':
                        mat = textureDict["WALLB0"].textureName;
                        break;
                    case '%':
                        mat = textureDict["WALLSTONE"].textureName;
                        break;
                    case '4':
                        mat = textureDict["WALLB1"].textureName;
                        break;
                    case '5':
                        mat = textureDict["WALLB1"].textureName;
                        break;
                    case '6':
                        mat = textureDict["WALLB2"].textureName;
                        break;
                    case '7':
                        mat = textureDict["WALLB2"].textureName;
                        break;
                    case '9':
                        //Debug.LogError(string.Format("9 in {0}", filename));
                        mat = textureDict["WALLB3"].textureName;
                        break;
                    default:
                        break;
                }
                var position = new Vector3((0.0f + j) / 2 - width / 2 - 0.25f, 0, height / 2 - (0.0f + i) / 2 + 0.25f);
                var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Brick1");
                result.Add(gameObj);
                if (mat != string.Empty)
                {
                    IConfig config = gameObj.GetComponent<AssemblyConfig>();
                    config.SetMaterial(mat);
                    var brick1Health = gameObj.GetComponentInChildren<Brick1Health>();
                    brick1Health.health = manager.GetHealth(mat);
                }
                gameObj.transform.position = position;
            }
        }
    }

    private void BuildFlagObj(FlagEntry flagEntry, List<GameObject> list)
    {
        if (flag != null && flagExists)
        {
            var position = new Vector3(flagEntry.x - width / 2, 0, flagEntry.z - (height - 1) / 2);
            var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Flag");
            list.Add(gameObj);
            IConfig config = gameObj.GetComponent<AssemblyConfig>();
            config.SetPrefab(flagEntry.prefabName);
            gameObj.transform.position = position;
        }
    }

    private void BuildEventObjList(List<EventObjEntry> list, List<GameObject> result)
    {
        foreach (var item in list)
        {
            var position = new Vector3(item.x - width / 2, 0, item.z - (height - 1) / 2);
            string prefabName = string.Empty;
            if (eventObjDict.TryGetValue(item.index, out prefabName))
            {
                var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "EventBuilding");
                result.Add(gameObj);
                IConfig config = gameObj.GetComponent<AssemblyConfig>();
                config.SetPrefab(prefabName);
                gameObj.transform.position = position;
            }
            else
            {
                var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "item_airBomb");
                result.Add(gameObj);
                gameObj.transform.position = position;
                Debug.LogError(string.Format("{0} from {1} is not in dict.", item.index, this.filename));
            }
        }
    }


    private void BuildTerrTable(TerrEntry[,] terrTable, string matKey, List<GameObject> terrList)
    {
        for (int row = 0; row < this.height; row++)
        {
            for (int col = 0; col < this.width; col++)
            {
                var terr = terrTable[row, col];
                var objs = terr.Build(matKey, row, col);
                if (objs != null)
                {
                    terrList.AddRange(objs);
                }
            }
        }
    }

    protected OriginalMap() { }

    private static Dictionary<int, string> eventObjDict = new Dictionary<int, string>();
    public static OriginalMap GetOriginalMap(string filename)
    {
        var map = new OriginalMap();
        map.filename = filename;
        var obj = Resources.Load(filename);
        var array = System.Text.Encoding.ASCII.GetBytes(obj.ToString());
        using (var mem = new System.IO.MemoryStream(array))
        {
            ParseMap(mem, map);
        }

        return map;
    }

    private static void ParseMap(System.IO.MemoryStream mem, OriginalMap map)
    {
        //using (var reader = new System.IO.StreamReader(path + @"/" + filename))
        using (var reader = new System.IO.StreamReader(mem))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                Console.WriteLine(line);
                if (line.StartsWith("respawn"))
                {
                    GetRespawnList(map, reader);
                }
                else if (line.StartsWith("items"))
                {
                    //Debug.LogError(string.Format("{0}:{1}", filename, line));
                }
                else if (line.StartsWith("gametype"))
                {
                    //Debug.LogError(string.Format("{0}:{1}", filename, line));
                    GetGameType(map, line);
                }
                else if (line.StartsWith("flag"))
                {
                    //Debug.LogError(string.Format("{0}:{1}", filename, line));
                    GetFlagObj(map, line);
                }
                else if (line.StartsWith("eventobj"))
                {
                    GetEventObj(map, reader);
                }
                else if (line.StartsWith("texscript"))
                {
                    GetTextureScript(map, line);//, path);
                }
                else if (line.StartsWith("tank"))
                {
                    GetTanksInFirstLine(map, line);
                    GetTanks(map, reader);
                }
                else if (line.StartsWith("terrtype"))
                {
                    GetTerrType(map, reader);
                }
                else if (line.StartsWith("wall"))
                {
                    GetWall(map, reader);
                }
                else if (line.StartsWith("terr2"))
                {
                    GetTerr2(map, reader);
                }
                else if (line.StartsWith("terr"))
                {
                    GetTerr(map, reader);
                }
            }
        }
    }

    private static void GetRespawnList(OriginalMap map, System.IO.StreamReader reader)
    {
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line.Contains("endrespawn"))
            {
                break;
            }

            {
                var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
                var index = int.Parse(parts[1]);
                var row = int.Parse(parts[2]);
                var col = int.Parse(parts[3]);
                var gameObj = new GameObject("respawn(" + parts[1] + ")");
                gameObj.transform.position = new Vector3(row - map.width / 2, 0, col - (map.height - 1) / 2);
                map.respawnList.Add(gameObj.transform);
            }
        }
    }

    private static void GetGameType(OriginalMap map, string line)
    {
        //gametype  BFH
        var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
        if (parts[1].Contains("BF"))
        { map.flagExists = true; }
        else
        { map.flagExists = false; }
    }

    private static void GetFlagObj(OriginalMap map, string line)
    {
        //flag 10 4 FLAGI FLAG123 FLAG FLAG 
        var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 5) { return; }
        var prefabName = "Flag_" + parts[3].Substring("FLAG".Length);
        var x = int.Parse(parts[1]);
        var z = int.Parse(parts[2]);
        map.flag = new FlagEntry(prefabName, x, z);
    }
    public class FlagEntry
    {
        public string prefabName;
        public int x;
        public int z;

        public FlagEntry(string prefabName, int x, int z)
        {
            this.prefabName = prefabName;
            this.x = x;
            this.z = z;
        }

        public override string ToString()
        {
            return string.Format("{0}({1},0,{2}", prefabName, x, z);
            //return base.ToString();
        }
    }
    private static void GetEventObj(OriginalMap map, System.IO.StreamReader reader)
    {
        if (eventObjDict.Count == 0)
        { InitilizeEventObjDict(); }
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line.Contains("endeventobj"))
            {
                break;
            }

            {
                var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
                var index = int.Parse(parts[1]);
                var x = int.Parse(parts[2]);
                var z = int.Parse(parts[3]);
                map.eventObjList.Add(new EventObjEntry(index, x, z, map));
            }
        }
    }

    private static void InitilizeEventObjDict()
    {
        //eventObjDict.Add(0, "item_airBomb");
        eventObjDict.Add(1, "Event_Dzot");
        //eventObjDict.Add(2, "item_airBomb");
        eventObjDict.Add(3, "Event_Host");
        eventObjDict.Add(4, "EventP");
        eventObjDict.Add(5, "EventObj_Mirror2");
        eventObjDict.Add(6, "EventObj_Mirror");
        eventObjDict.Add(7, "Event_Bomb");
        eventObjDict.Add(8, "Event_Rec");
        eventObjDict.Add(13, "None");
        eventObjDict.Add(14, "Event_Ship");
    }

    private static void GetTextureScript(OriginalMap map, string line)//, string path)
    {
        var srcName = string.Empty;
        {
            var seperator = line.LastIndexOf('\\');
            var dot = line.LastIndexOf('.');
            srcName = line.Substring(seperator + 1, dot - 1 - seperator);
        }

        var obj = Resources.Load(@"textureScript/" + srcName);
        var array = System.Text.Encoding.ASCII.GetBytes(obj.ToString());
        using (var mem = new System.IO.MemoryStream(array))
        {
            ParseTexture(mem, map);
        }

    }

    private static void ParseTexture(System.IO.MemoryStream mem, OriginalMap map)
    {
        using (var reader = new System.IO.StreamReader(mem))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line.StartsWith("texture"))
                {
                    var index = 0;
                    var name = string.Empty;
                    var textureFile = string.Empty;
                    {
                        var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
                        index = int.Parse(parts[1]);
                        name = parts[2].Substring(1, parts[2].Length - 2);
                    }
                    {
                        line = reader.ReadLine();
                        var sign = @"Texture\\";
                        var seperator = line.LastIndexOf(sign);
                        var dot = line.IndexOf('.', seperator);
                        textureFile = line.Substring(seperator + sign.Length, dot - sign.Length - seperator);
                    }
                    map.textureDict.Add(name, new TextureEntry(index, name, textureFile));//, map));
                }
            }
        }
    }
    private static void GetTerr2(OriginalMap map, System.IO.StreamReader reader)
    {
        for (int row = 0; row < map.height; row++)
        {
            var line = reader.ReadLine();
            var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
            for (int col = 0; col < map.width; col++)
            {
                var index = int.Parse(parts[col * 2 + 1]);
                var rotation = parts[col * 2 + 2];
                map.terr2Table[row, col] = new TerrEntry(index, rotation, map);
            }
        }
    }
    private static void GetTerr(OriginalMap map, System.IO.StreamReader reader)
    {
        for (int row = 0; row < map.height; row++)
        {
            var line = reader.ReadLine();
            var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(parts.Length);
            Console.WriteLine(parts.Length);
            for (int col = 0; col < map.width; col++)
            {
                var index = int.Parse(parts[col * 2 + 1]);
                var rotation = parts[col * 2 + 2];
                map.terrTable[row, col] = new TerrEntry(index, rotation, map);
            }
        }
    }

    private static void GetWall(OriginalMap map, System.IO.StreamReader reader)
    {
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line.Contains("endwall"))
            { break; }

            var start = line.IndexOf('"');
            var end = line.LastIndexOf('"');
            map.wallList.Add(line.Substring(start + 1, end - 1 - start));
        }
    }

    private static void GetTerrType(OriginalMap map, System.IO.StreamReader reader)
    {
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line.Contains("endterr"))
            { break; }

            var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
            var index = int.Parse(parts[1]);
            var prefabName = GetPrefabName(parts[2]);
            var obstacleInfo = parts[3];
            map.terrPrefabDict.Add(index, new TerrObstacleEntry(prefabName, obstacleInfo));
        }
    }

    private static string GetPrefabName(string terrTypeInMap)
    {
        //nameInMap: TERR15
        var result = PrefabName.GetMatchedTerrPrefab(terrTypeInMap);
        return result;
    }

    private static void GetTanks(OriginalMap map, System.IO.StreamReader reader)
    {
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line.StartsWith("size"))
            {
                var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
                map.width = int.Parse(parts[1]);
                map.height = int.Parse(parts[2]);
                map.terrTable = new TerrEntry[map.height, map.width];
                map.terr2Table = new TerrEntry[map.height, map.width];
                break;
            }

            {
                var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                {
                    map.tankList.Add(parts[i]);
                    //if (parts[i].Contains("I"))
                    //{ Debug.Log(string.Format("{0} has I", map.filename)); }
                }
            }
        }
    }

    private static void GetTanksInFirstLine(OriginalMap map, string line)
    {
        var parts = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
        map.signedTankCount = int.Parse(parts[1]);
        for (int i = 2; i < parts.Length; i++)
        {
            map.tankList.Add(parts[i]);
            //if(parts[i].Contains("I"))
            //{ Debug.Log(string.Format("{0} has I", map.filename)); }
        }
    }
    static readonly char[] emptyChar = new char[] { ' ', '\t' };
    public string filename;
    public FlagEntry flag;
    public bool flagExists;

    public class TerrEntry
    {
        public int index;
        public string rotationNxx;
        public OriginalMap container;
        public TerrEntry(int index, string rotationNxx, OriginalMap container)
        {
            this.index = index;
            this.rotationNxx = rotationNxx;
            this.container = container;
        }
        public override string ToString()
        {
            if (container.terrPrefabDict.ContainsKey(index))
            {
                return string.Format("{0}({1}),{2}", index, container.terrPrefabDict[index].prefabName, rotationNxx);
            }
            else
            {
                return string.Format("{0}({1}),{2}", index, "<null>", rotationNxx);
            }
            //return base.ToString();
        }

        public IEnumerable<GameObject> Build(string matKey, int row, int col)
        {
            var prefabName = GetTerrPrefabName();
            if (prefabName == null) { return null; }
            var positionY = GetTerrPosition();
            var rotationY = GetTerrRotation();
            //var position = new Vector3(row - this.container.width / 2, positionY, col - this.container.height / 2);
            //var position = new Vector3(row - this.container.width / 2, positionY, this.container.height / 2 - col);
            //var position = new Vector3(this.container.width / 2 - row, positionY, col - this.container.height / 2);
            //var position = new Vector3(this.container.width / 2 - row, positionY, this.container.height / 2 - col);

            //var position = new Vector3(col - this.container.height / 2, positionY, row - this.container.width / 2);
            var position = new Vector3(col - this.container.height / 2, positionY, this.container.width / 2 - row);//good
            //var position = new Vector3(this.container.height / 2 - col, positionY, row - this.container.width / 2);
            //var position = new Vector3(this.container.height / 2 - col, positionY, this.container.width / 2 - row);
            var rotation = Quaternion.Euler(0, rotationY, 0);

            var objs = new List<GameObject>();
            {
                {
                    var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Terr");
                    objs.Add(gameObj);
                    IConfig config = gameObj.GetComponent<AssemblyConfig>();
                    config.SetPrefab(prefabName);
                    config.SetMaterial(this.container.textureDict[matKey].textureName);
                    gameObj.transform.position = position;
                    gameObj.transform.rotation = rotation;
                }
                //if (prefabName == PrefabName.strTerr_11
                //    || prefabName == PrefabName.strTerr_70
                //    || prefabName == PrefabName.strTerr_107)
                //{
                //    //var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Bullet_MapShow");
                //    //gameObj.GetComponentInChildren<MeshRenderer>().enabled = false;
                //    var gameObj = new GameObject(prefabName);
                //    gameObj.transform.position = position;
                //    gameObj.transform.rotation = rotation;
                //    if (prefabName == PrefabName.strTerr_107)
                //    { gameObj.transform.Translate(Vector3.left); }
                //    else
                //    { gameObj.transform.Translate(Vector3.forward); }
                //    objs.Add(gameObj);
                //    this.container.respawnList.Add(gameObj.transform); 
                //}
            }

            if (IsDoorWay())
            {
                var delta = new int[] { -1, 1 };
                foreach (var item in delta)
                {
                    var pos = position;
                    pos.x += item;
                    var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Obstacle");
                    objs.Add(gameObj);
                    gameObj.transform.position = pos;
                    gameObj.transform.rotation = rotation;
                }
            }
            else if (IsPyramid())
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var pos = position;
                        pos.x -= i; pos.z += j;
                        var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Obstacle");
                        objs.Add(gameObj);
                        gameObj.transform.position = pos;
                        gameObj.transform.rotation = rotation;
                    }
                }
            }
            else if (IsObstacle())
            {
                var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Obstacle");
                objs.Add(gameObj);
                gameObj.transform.position = position;
                gameObj.transform.rotation = rotation;
            }
            else if (IsLowObstacle())
            {
                position.y = -0.9f;
                var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Obstacle");
                objs.Add(gameObj);
                gameObj.transform.position = position;
                gameObj.transform.rotation = rotation;
            }

            return objs;
        }

        private bool IsPyramid()
        {
            var result = false;
            TerrObstacleEntry entry;
            if (this.container.terrPrefabDict.TryGetValue(this.index, out entry))
            {
                var terrPostfix = entry.prefabName.Substring("Terr_".Length);
                var terrNumber = int.Parse(terrPostfix);
                if (terrNumber == 83)
                {
                    result = true;
                    //var prefabName = this.container.terrPrefabDict[this.index];
                    //result = (prefabName.obstacleState == "NW"); //string.Format("Terr_{0}", this.Item1);
                }
            }
            return result;
        }

        private bool IsDoorWay()
        {
            var result = false;
            TerrObstacleEntry entry;
            if (this.container.terrPrefabDict.TryGetValue(this.index, out entry))
            {
                var terrPostfix = entry.prefabName.Substring("Terr_".Length);
                var terrNumber = int.Parse(terrPostfix);
                if (terrNumber == 86)
                {
                    result = true;
                    //var prefabName = this.container.terrPrefabDict[this.index];
                    //result = (prefabName.obstacleState == "NW"); //string.Format("Terr_{0}", this.Item1);
                }
            }
            return result;
        }

        private bool IsLowObstacle()
        {
            var result = false;
            TerrObstacleEntry entry;
            if (this.container.terrPrefabDict.TryGetValue(this.index, out entry))
            {
                var terrPostfix = entry.prefabName.Substring("Terr_".Length);
                var terrNumber = int.Parse(terrPostfix);
                if (terrNumber != 5)
                {
                    var prefabName = this.container.terrPrefabDict[this.index];
                    result = (prefabName.obstacleState == "NW"); //string.Format("Terr_{0}", this.Item1);
                }
            }
            return result;
        }

        private bool IsObstacle()
        {
            var result = false;
            TerrObstacleEntry entry;
            if (this.container.terrPrefabDict.TryGetValue(this.index, out entry))
            {
                var terrPostfix = entry.prefabName.Substring("Terr_".Length);
                var terrNumber = int.Parse(terrPostfix);
                if (terrNumber == 88)
                { result = false; }
                else if (terrNumber != 5)
                {
                    var prefabName = this.container.terrPrefabDict[this.index];
                    result = (prefabName.obstacleState == "NWF"
                        || prefabName.obstacleState == "NF");
                }
            }
            return result;
        }

        private float GetTerrRotation()
        {
            var angle = -1;

            if (this.rotationNxx == "N" || this.rotationNxx == "N3" || this.rotationNxx == "N34" || this.rotationNxx == "N5")
            { angle = 0; }
            else if (this.rotationNxx == "N1" || this.rotationNxx == "N13" || this.rotationNxx == "N134" || this.rotationNxx == "N15")
            { angle = 90; }
            else if (this.rotationNxx == "N2" || this.rotationNxx == "N23" || this.rotationNxx == "N234" || this.rotationNxx == "N25")
            { angle = 180; }
            else if (this.rotationNxx == "N12" || this.rotationNxx == "N123" || this.rotationNxx == "N1234" || this.rotationNxx == "N1235" || this.rotationNxx == "N125")
            { angle = 270; }
            //else if (this.rotationNxx == "N34")
            //{ angle = 360 - 0; }
            //else if (this.rotationNxx == "N134")
            //{ angle = 360 - 90; }
            //else if (this.rotationNxx == "N234")
            //{ angle = 360 - 180; }
            //else if (this.rotationNxx == "N1234")
            //{ angle = 360 - 270; }
            //else if (this.rotationNxx == "N3")
            //{ angle = 0; }

            if (angle == -1)
            {
                Debug.LogError(string.Format("{0} is not a valid rotation param.(terrtype: {1})(file: {2})",
                    this.rotationNxx, this.index, this.container.filename));
                angle = 0;
            }

            TerrObstacleEntry entry;
            if (this.container.terrPrefabDict.TryGetValue(this.index, out entry))
            {
                var terrPostfix = entry.prefabName.Substring("Terr_".Length);
                var terrNumber = int.Parse(terrPostfix);
                Console.WriteLine(terrNumber);
                //if(terrNumber==95||terrNumber==83)
                //{
                //    Debug.LogError(terr+" " + filename);
                //}
                if (terrNumber == 0 || terrNumber == 2 || terrNumber == 4 || terrNumber == 7 || terrNumber == 28 || terrNumber == 44 || /*terrNumber == 50 ||*/ terrNumber == 53 || terrNumber == 65 || terrNumber == 67 || terrNumber == 91)
                { angle += 90; }
                else if (terrNumber == 8 || terrNumber == 14 || terrNumber == 20 || terrNumber == 29 || terrNumber == 43 || terrNumber == 46 || terrNumber == 50 || terrNumber == 64 || terrNumber == 66 || terrNumber == 71 || terrNumber == 77 || terrNumber == 90 || terrNumber == 101 || terrNumber == 104 || terrNumber == 100 || terrNumber == 106 || terrNumber == 107)
                { angle += 180; }
                else if (terrNumber == 9 || terrNumber == 35 || terrNumber == 47 || terrNumber == 48 ||/* */ terrNumber == 68 || terrNumber == 78 || terrNumber == 79 || terrNumber == 96 || terrNumber == 103 || terrNumber == 105 || terrNumber == 108 || terrNumber == 109)
                { angle -= 90; }
            }

            return angle;
            //throw new Exception(string.Format("{0} is not a valid rotation param.", this.Item2));
        }

        private float GetTerrPosition()
        {
            var y = 0.0f;
            if (this.rotationNxx.EndsWith("5") || this.rotationNxx.EndsWith("34"))
            { y += 0.5f; }

            return y;
        }

        private string GetTerrPrefabName()
        {
            if (this.container.terrPrefabDict.ContainsKey(this.index))
            {
                var result = this.container.terrPrefabDict[this.index];
                return result.prefabName; //string.Format("Terr_{0}", this.Item1);
            }
            else
            {
                return null;
            }
        }
    }
    public class TerrObstacleEntry
    {
        public string prefabName;
        public string obstacleState;
        public TerrObstacleEntry(string prefabName, string obstacleState)
        {
            this.prefabName = prefabName;
            this.obstacleState = obstacleState;
        }
        public override string ToString()
        {
            return string.Format("{0},{1}", prefabName, obstacleState);
            //return base.ToString();
        }
    }
    public class TextureEntry
    {
        public int index;
        public string entryName;
        public string textureName;
        //public OriginalMap container;
        public TextureEntry(int index, string entryName, string textureName)//, OriginalMap container)
        {
            this.index = index;
            this.entryName = entryName;
            this.textureName = textureName;
            //this.container = container;
        }
        public override string ToString()
        {
            return string.Format("{0},{1},{2}", index, entryName, textureName);
            //return base.ToString();
        }
    }
    public class EventObjEntry
    {
        public int index;
        public OriginalMap container;
        public int x;
        public int z;
        public EventObjEntry(int index, int x, int z, OriginalMap container)
        {
            this.index = index;
            this.x = x;
            this.z = z;
            this.container = container;
        }
        public override string ToString()
        {
            return string.Format("{0}({1}, 0, {2})", index, x, z);
            //return base.ToString();
        }
    }
}
