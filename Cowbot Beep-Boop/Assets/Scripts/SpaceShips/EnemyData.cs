using System;
using System.Collections.Generic;

/// <summary>
/// Data class needed for Unity to read json
/// </summary>
namespace Cowbot_Beep_Boop.SpaceShips
{
    [Serializable]
    public class EnemyData
    {
        public EnemyStats[] EnemyList;
    }
}