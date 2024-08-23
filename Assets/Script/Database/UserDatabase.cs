using UnityEngine;

[CreateAssetMenu(fileName = "UserDatabase", menuName = "Database/UserDatabase")]
public class UserDatabase : ScriptableObject
{
    public UserData[] users;
}
