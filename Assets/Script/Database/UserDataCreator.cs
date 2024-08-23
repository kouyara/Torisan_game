#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserDataCreator : MonoBehaviour
{
    public UserDatabase userDatabase; // UserDatabase ScriptableObjectをアタッチ
    public InputField usernameInputField; // UIのInputFieldをアタッチ
    public Button button;

    void Start()
    {
        button.onClick.AddListener(() => OnSubmitUsername());
    }
    public void OnSubmitUsername()
    {
        if (userDatabase != null && usernameInputField != null)
        {
            string name = usernameInputField.text;

            if (!string.IsNullOrEmpty(name))
            {
                CreateAndSaveNewUserData(name);

                // 入力フィールドをクリア
                usernameInputField.text = "";

                Debug.Log($"User '{name}' added");
                SceneManager.LoadScene("MainGame");
            }
            else
            {
                Debug.LogError("Username is empty!");
            }
        }
        else
        {
            Debug.LogError("UserDatabase or InputField is not set!");
        }
    }
    public void CreateAndSaveNewUserData(string username)
    {
        if (userDatabase != null)
        {
            // 新しいUserDataオブジェクトをScriptableObjectからインスタンス化
            UserData newUser = ScriptableObject.CreateInstance<UserData>();
            newUser.name = username;
            newUser.id = GenerateUniqueId();
            newUser.score = 0; // 初期スコアを設定

            // ユーザーのアセットファイルとして保存
            SaveUserDataAsAsset(newUser);

            // UserDatabaseに追加
            AddUserToDatabase(newUser);

            Debug.Log($"New UserData object created for username '{username}' with ID {newUser.id} and saved as an asset.");
        }
        else
        {
            Debug.LogError("UserDatabase is not set!");
        }
    }

    private int GenerateUniqueId()
    {
        // IDを生成 (既存の最大ID+1を割り当てる簡易的な方法)
        int maxId = 0;
        foreach (var user in userDatabase.users)
        {
            if (user.id > maxId)
            {
                maxId = user.id;
            }
        }
        return maxId + 1;
    }

    private void AddUserToDatabase(UserData newUser)
    {
        // 現在のデータベースのユーザーリストをリストに変換して追加
        var userList = new System.Collections.Generic.List<UserData>(userDatabase.users);
        userList.Add(newUser);
        userDatabase.users = userList.ToArray();
    }

    private void SaveUserDataAsAsset(UserData userData)
    {
        // アセット保存用のパスを決定 (Assets/Resources/UserData/username.asset)
        string assetPath = $"Assets/Database/UserData/{userData.name}.asset";

        // ディレクトリが存在しない場合は作成
        string directory = System.IO.Path.GetDirectoryName(assetPath);
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }

        // UserDataオブジェクトをアセットとして保存
        AssetDatabase.CreateAsset(userData, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif
