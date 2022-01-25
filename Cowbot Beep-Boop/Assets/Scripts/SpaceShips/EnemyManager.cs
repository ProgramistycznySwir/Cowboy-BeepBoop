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
        // public int EnemyCount { get; private set; }
        public List<EnemySpaceShip> Enemies { get; private set; } = new List<EnemySpaceShip>();
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

        public void SpawnEnemies(int sumDifficulty, int minDifficulty = 0)
        {
            int topIndex = enemyPrototypes.Count - 1;
            int minIndex = 0;
            System.Random rng = new();
            while(sumDifficulty > 0)
            {
                int enemyIndex = minIndex + rng.Next(topIndex - minIndex);
                EnemySpaceShip enemy = enemyPrototypes[enemyIndex];
                if(enemy.difficulty > sumDifficulty)
                {
                    topIndex = enemyIndex;
                    continue;
                }
                if(enemy.difficulty < minDifficulty)
                {
                    minIndex = enemyIndex;
                    continue;
                }
                
                Transform newEnemy = GameObject.Instantiate(
                        enemy.transform,
                        PlayerSpaceShip.GetPosition() + Vector2_Extensions.FromRandomAngle() * SpawnRadius,
                        Quaternion.identity);
                newEnemy.gameObject.SetActive(true);
                Enemies.Add(newEnemy.GetComponent<EnemySpaceShip>());
                sumDifficulty -= enemy.difficulty;
            }
        }

        public void RemoveEnemy(EnemySpaceShip enemy)
        {
            Enemies.Remove(enemy);
            if(Enemies.Count <= 0)
            {
                // TODO: Behaviour when player defeats all enemies.
                EndLevel();
            }
        }

        void EndLevel()
        {
            Debug.Log("You've won!!");
        }

        public Vector2 GetClosestEnemyPosition()
        {
            Vector2 playerPos = PlayerSpaceShip.GetPosition();
            Vector2 closestPos = Vector2.zero;
            float closestDistance = float.MaxValue;
            for(int i = 0; i < Enemies.Count; i++)
            {
                var enemy = Enemies[i];
                float dist = Vector2.Distance(playerPos, enemy.transform.position);
                if(dist < closestDistance)
                {
                    closestDistance = dist;
                    closestPos = enemy.transform.position;
                }
            }
            return closestPos;
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