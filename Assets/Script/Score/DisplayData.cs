using TMPro;
using UnityEngine;

public class DisplayData : MonoBehaviour
{
    public UserDatabase userDatabase;
    public TextMeshProUGUI userName1;
    public TextMeshProUGUI userName2;
    public TextMeshProUGUI userName3;
    public TextMeshProUGUI userName4;
    public TextMeshProUGUI userName5;
    public TextMeshProUGUI userName6;
    public TextMeshProUGUI userName7;
    public TextMeshProUGUI userName8;

    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    public TextMeshProUGUI score3;
    public TextMeshProUGUI score4;
    public TextMeshProUGUI score5;
    public TextMeshProUGUI score6;
    public TextMeshProUGUI score7;
    public TextMeshProUGUI score8;

    void Start()
    {
        if (userDatabase != null && userDatabase.users != null)
        {
            // データベースからC#の配列にデータを格納
            UserData[] userArray = new UserData[userDatabase.users.Length];
            userArray = userDatabase.users;

            // Scoreに基づいて降順にソート
            System.Array.Sort(userArray, (x, y) => y.score.CompareTo(x.score));

            // ソート後の配列の内容を表示
            userName1.text = userArray[0].name;
            score1.text = userArray[0].score.ToString();

            userName2.text = userArray[1].name;
            score2.text = userArray[1].score.ToString();

            userName3.text = userArray[2].name;
            score3.text = userArray[2].score.ToString();

            userName4.text = userArray[3].name;
            score4.text = userArray[3].score.ToString();

            userName5.text = userArray[4].name;
            score5.text = userArray[4].score.ToString();

            userName6.text = userArray[5].name;
            score6.text = userArray[5].score.ToString();

            userName7.text = userArray[6].name;
            score7.text = userArray[6].score.ToString();

            userName8.text = userArray[7].name;
            score8.text = userArray[7].score.ToString();
        }
    }
}
