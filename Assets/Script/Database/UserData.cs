using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "Database/UserData")]
public class UserData : ScriptableObject
{
    public string name;
    public int id;
    public int score;
}
