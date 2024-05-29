

using UnityEngine;
using Dan.Main;
public static class LeaderBoard 
{
    // Start is called before the first frame update

    private static string publicKey= "4d20065bde55aba7263e07336e9700cc9ed70e2784ecc740ae4253ae301e2e5a";
   
    public static void GetUserName(int i)
    {
        string userName = null;
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) => {
            userName = msg[i].Username;
        }));
        
    }

    public static void SetLeaderBoardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicKey, username, score, ((msg) => {
            Debug.Log("NewEntry");
        }));
    }
  
}
