using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
// using System.Text.Json;
using System.IO;

namespace Cowbot_Beep_Boop.SpaceShips
{
    public class EnemyManager : MonoBehaviour
    {
        static EnemyManager _instance;
        public static EnemyManager GetInstance() => _instance;

        public EnemyManager() => _instance = this;


        public string ResourceName = "enemies";

        public List<GameObject> enemyPrototypes;
        public Dictionary<string, EnemyStats> Enemies { get; set; }
        public int EnemyCount => Enemies.Count;

        public void LoadFromJson()
        {
            TextAsset jasonFile = (TextAsset)Resources.Load(ResourceName);
            var enemyData = JsonUtility.FromJson<EnemyData>(jasonFile.text);
            var enemyList = enemyData.EnemyList;
            Enemies = enemyList.ToDictionary(item => item.Name);
            // Enemies = JsonUtility
            //         .FromJson<EnemyData>(jasonFile.text)
            //         .EnemyList
            //         .ToDictionary(item => item.Name);
        }

        public void DEBUG_CreateExampleFile()
        {
            EnemyData enemyData = new();
            // List<Stats_Prototype> list = new();
            enemyData.EnemyList = new EnemyStats[4];
            enemyData.EnemyList[0] = new EnemyStats();
            enemyData.EnemyList[1] = new EnemyStats();

            File.WriteAllText("enemies_example.json", JsonUtility.ToJson(enemyData, true));
        }
    }
}