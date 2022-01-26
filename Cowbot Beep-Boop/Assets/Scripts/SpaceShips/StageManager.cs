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
    public class StageManager : MonoBehaviour
    {
        public const float SpawnRadius = 20f;

        static StageManager _instance;
        public static StageManager GetInstance() => _instance;

        public StageManager() => _instance = this;

        public List<EnemySpaceShip> enemyPrototypes;
        public List<EnemySpaceShip> Enemies { get; private set; } = new List<EnemySpaceShip>();
        public const int EnemyLimit = 25;
        public int currentStage { get; private set; } = 0;
        public static readonly (int sumDifficulty, int minDifficulty)[] Stages = new[]{
            (0, 0), // 0
            (2, 0), // 1
            (4, 0), // 2
            (8, 0), // 3
            (16, 2), // 4
            (32, 2), // 5
            (64, 2), // 6
            (128, 3), // 7
            (256, 3), // 8
            (512, 3), // 9
            (1024, 6), // 10
            (2048, 6), // 11
        };

        public StageEndMenu stageEndMenu;
        public VictoryMenu victoryMenu;
        public VictoryMenu gameOverMenu;

        void Start()
        {
            PrepareEnemies();

            NextStage();
        }

        public void PrepareEnemies()
        {
            enemyPrototypes = transform.GetComponentsInChildrenWithDepthOne<EnemySpaceShip>().ToList();
            enemyPrototypes.Sort((left, right) => left.difficulty.CompareTo(right.difficulty));
        }

        public void SpawnEnemies((int sumDifficulty, int minDifficulty) tuple)
            => SpawnEnemies(tuple.sumDifficulty, tuple.minDifficulty);
        public void SpawnEnemies(int sumDifficulty, int minDifficulty = 0)
        {
            int topIndex = enemyPrototypes.Count - 1;
            int minIndex = 0;
            System.Random rng = new();
            Debug.Log(currentStage);
            // while(sumDifficulty > 0 && sumDifficulty >= minDifficulty && Enemies.Count <= EnemyLimit)
            while(sumDifficulty > 0 && sumDifficulty >= enemyPrototypes[minIndex].difficulty && Enemies.Count <= EnemyLimit)
            {
                int enemyIndex = minIndex + rng.Next(topIndex - minIndex + 1);
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
            if(Enemies.Count <= 0)
            {
                // currentStage++;
                NextStage();
            }
        }

        public void RemoveEnemy(EnemySpaceShip enemy)
        {
            Enemies.Remove(enemy);
            if(Enemies.Count <= 0)
            {
                // TODO: Behaviour when player defeats all enemies.
                EndStage();
            }
        }

        void EndStage()
        {
            if(currentStage < Stages.Length)
            {
                NextStage();
                stageEndMenu.Open();
            }
            else
                Victory();
        }

        void NextStage()
        {
            // Debug.Log($"Generating stage with sumDifficulty: {Stages[currentStage].sumDifficulty}, minDifficulty: {Stages[currentStage].minDifficulty}");
            // Debug.Log("OI MATE");
            currentStage++;
            SpawnEnemies(Stages[currentStage]);
        }

        void Victory()
        {
            Debug.Log("You've won!!");
            victoryMenu.Open();
        }
        public void GameOver()
        {
            Debug.Log("You've lost!!");
            gameOverMenu.Open();
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
    }
}