using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cowbot_Beep_Boop.Strategies
{
    public interface IStrategy
    {
        Color GetColor();
        Vector2 Fire(Vector2 position, Vector2 playerPosition);
        (float throttle, float steering) Move(Vector2 position, Vector2 forward, Vector2 playerPosition);

        public IStrategy GetStrategy(StrategyType type)
            => type switch {
                StrategyType.Default => new DefaultStrategy(),
                StrategyType.ChargeAtEnemy => new ChargeAtEnemyStrategy(),
                StrategyType.KeepDistance => new KeepDistanceStrategy(),
                _ => new DefaultStrategy()
            };
    }

    public enum StrategyType { Default, ChargeAtEnemy, KeepDistance }
}
