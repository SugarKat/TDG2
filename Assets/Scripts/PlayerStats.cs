using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour {

    public static int Money;
    public int startMoney = 150;
    public TextMeshProUGUI PlayerMoney;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
    }

    void Update()
    {
        if (PlayerMoney != null)
        {
            PlayerMoney.text = "$" + Money.ToString();
        }
        else
            Debug.LogError("Nepateikta kur reikia rodyti");
    }

}
