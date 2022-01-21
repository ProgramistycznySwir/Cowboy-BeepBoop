using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public PlayerSpaceShip player;
    public Image healthBar;

    public TMPro.TextMeshProUGUI text_HP;

    public void UpdateHP(float newHP)
    {
        text_HP.text = $"HP: {player.health_max} / {newHP}";
        healthBar.fillAmount = player.health_max / newHP;
    }
    void Start()
    {
        player.SubscribeToHealthChange(UpdateHP);
    }

}
