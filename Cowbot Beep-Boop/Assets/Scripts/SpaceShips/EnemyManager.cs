using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
// using System.Text.Json;
using System.IO;
using Cowbot_Beep_Boop.Helpful;

namespace Cowbot_Beep_Boop.SpaceShips
{
    public class EnemyManager : MonoBehaviour
    {
        public const float SpawnRadius = 20f;

        static EnemyManager _instance;
        public static EnemyManager GetInstance() => _instance;

        public EnemyManager() => _instance = this;


        // public string ResourceName = "enemies";

        public List<EnemySpaceShip> enemyPrototypes;
        // public Dictionary<string, EnemyStats> Enemies { get; set; }
        // public int EnemyCount => Enemies.Count;

        void Start()
        {
            // DEBUG_CreateExampleFile();
            // LoadFromJson();
            PrepareEnemies();
            SpawnEnemies(2);
        }

        public void PrepareEnemies()
        {
            enemyPrototypes = transform.GetComponentsInChildrenWithDepthOne<EnemySpaceShip>().ToList();
            enemyPrototypes.Sort((left, right) => left.difficulty.CompareTo(right.difficulty));
        }

        public void SpawnEnemies(int sumDifficulty)
        {
            int topIndex = enemyPrototypes.Count - 1;
            System.Random rng = new();
            while(sumDifficulty > 0)
            {
                int enemyIndex = rng.Next(topIndex);
                EnemySpaceShip enemy = enemyPrototypes[enemyIndex];
                if(enemy.difficulty <= sumDifficulty)
                {
                    GameObject.Instantiate(
                            enemy.transform,
                            PlayerSpaceShip.GetPosition() + Vector2_Extensions.FromRandomAngle() * SpawnRadius,
                            Quaternion.identity)
                        .gameObject.SetActive(true);
                    sumDifficulty -= enemy.difficulty;
                }
                else
                    topIndex = enemyIndex;
            }
        }

        // public void LoadFromJson()
        // {
        //     TextAsset jasonFile = (TextAsset)Resources.Load(ResourceName);
        //     var enemyData = JsonUtility.FromJson<EnemyData>(jasonFile.text);
        //     var enemyList = enemyData.EnemyList;
        //     Enemies = enemyList.ToDictionary(item => item.Name);
        //     // Enemies = JsonUtility
        //     //         .FromJson<EnemyData>(jasonFile.text)
        //     //         .EnemyList
        //     //         .ToDictionary(item => item.Name);
        // }

        // public void DEBUG_CreateExampleFile()
        // {
        //     EnemyData enemyData = new();
        //     // List<Stats_Prototype> list = new();
        //     enemyData.EnemyList = new EnemyStats[4];
        //     enemyData.EnemyList[0] = new EnemyStats();
        //     enemyData.EnemyList[1] = new EnemyStats();

        //     File.WriteAllText("enemies_example.json", JsonUtility.ToJson(enemyData, true));
        // }
    }
}