using UnityEngine.UI;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public InputField MemberID, PlayerScore;
    public int ID;

    private void Start()
    {
        /*//LootLockerSDKManager.StartSession("player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Succes");
            }
            else
            {
                Debug.Log("Failed");
            }
        });*/
    }


    public void SubmitScore()
    {
        //LootLockerSDKManager.SubmitScore(MemberID.text, int.Parse(PlayerScore.text), ID, (response) =>
        /*{
            if (response.success)
            {
                Debug.Log("Succes");
            }
            else
            {
                Debug.Log("Failed");
            }
        });*/
    }
}
