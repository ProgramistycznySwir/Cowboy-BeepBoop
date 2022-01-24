using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI HP_Text;
    public Image HP_Bar;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerSpaceShip.GetPlayer().SubscribeToHealthChange(Health_OnChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Health_OnChange(float hp)
    {
        HP_Text.text = $"HP: {(int)hp}/{(int)PlayerSpaceShip.GetPlayer().health_max}";
        HP_Bar.fillAmount = (float)hp / PlayerSpaceShip.GetPlayer().health_max;
    }
}
