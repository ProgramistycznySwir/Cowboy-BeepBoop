using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsDisplay : MonoBehaviour
{
    public EnemySpaceShip enemy;
    public Transform HP_Bar;
    Vector3 HP_Bar_Size;
    // Start is called before the first frame update
    void Start()
    {
        HP_Bar_Size = HP_Bar.localScale;
        enemy.SubscribeToHealthChange(HP_OnChange);
    }
    
    public void HP_OnChange(float hp)
    {
        HP_Bar.localScale = new Vector3(HP_Bar_Size.x * hp / enemy.health_max, HP_Bar_Size.y, HP_Bar_Size.z);
    }
}
