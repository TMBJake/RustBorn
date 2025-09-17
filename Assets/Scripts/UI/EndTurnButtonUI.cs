using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour

{
    private int endTurnCount = 0; //temp
    public GameObject collectRewardsButton; //temp
    public void OnClick()
    {
        BattleFlowManager.Instance.EndPlayerTurn();
    }

}


