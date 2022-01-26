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
            (2, 0), // 0
            (4, 0), // 1
            (8, 0), // 2
            (16, 2), // 3
            (32, 2), // 4
            (64, 2), // 5
            (128, 3), // 6
            (256, 3), // 7
            (512, 3), // 8
            (1024, 3), // 9
            (2048, 3), // 10
        };

        public StageEndMenu stageEndMenu;

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
            while(sumDifficulty > 0 && sumDifficulty >= minDifficulty && Enemies.Count <= EnemyLimit)
            {
                int enemyIndex = minIndex + rng.Next(topIndex - minIndex) + 1;
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
                EndStage();
            }
        }

        void EndStage()
        {
            currentStage++;
            if(currentStage >= Stages.Length)
                Victory();
            else
            {
                NextStage();
                stageEndMenu.Open();
            }
        }

        void NextStage()
        {
            // Debug.Log($"Generating stage with sumDifficulty: {Stages[currentStage].sumDifficulty}, minDifficulty: {Stages[currentStage].minDifficulty}");
            SpawnEnemies(Stages[currentStage]);
        }

        void Victory()
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
    }
}