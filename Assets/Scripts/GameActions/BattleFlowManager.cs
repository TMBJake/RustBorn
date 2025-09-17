using UnityEngine;
using System.Collections;

public class BattleFlowManager : MonoBehaviour
{
    public static BattleFlowManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RegisterPerformers();
        Debug.Log("[BattleFlowManager] Waiting for player turn...");
        // Player turn will begin after MatchSetupSystem triggers the DrawCardsGA
    }

    private void RegisterPerformers()
    {
        // Enemy logic
        ActionSystem.AttachPerformer<EnemyPrepareIntentGA>(EnemyIntentPerformer.PerformIntentPrep);
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer.PerformEnemyTurn);

        // Player + card logic
        ActionSystem.AttachPerformer<GainShieldGA>(action =>
        {
            Player.Instance.GainShield(action.Amount);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<DealDamageGA>(action =>
        {
            EnemyManager.Instance.DealDamageToDefaultTarget(action.Amount);

            // Show reward panel if all enemies are dead
            if (EnemyManager.Instance.AllEnemiesDead() && collectRewardsPanel != null)
            {
                collectRewardsPanel.SetActive(true);
                Debug.Log("[BattleFlowManager] All enemies defeated. Showing rewards panel.");
            }

            return EmptyCoroutine();
        });


        ActionSystem.AttachPerformer<DealUnblockableDamageGA>(action =>
        {
            EnemyManager.Instance.DealUnblockableToDefaultTarget(action.Amount);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<DamageRandomEnemiesGA>(action =>
        {
            EnemyManager.Instance.DamageRandomEnemies(action.Damage, action.Count);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<DrawCardsGA>(action =>
        {
            return CardSystem.Instance.PerformDraw(action.Amount);
        });

        ActionSystem.AttachPerformer<SelfDamageGA>(action =>
        {
            Player.Instance.TakeDamage(action.Amount);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<GainManaGA>(action =>
        {
            Player.Instance.GainMana(action.Amount);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<HealGA>(action =>
        {
            Player.Instance.Heal(action.Amount);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<RemoveAllDebuffsGA>(action =>
        {
            StatusSystem.Instance.RemoveAllDebuffs(Player.Instance);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<ApplyStatusGA>(action =>
        {
            StatusSystem.Instance.Apply(Player.Instance, action.Status, action.Duration, action.Magnitude);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<ConditionalDamageGA>(action =>
        {
            int damage = action.Condition(null) ? action.BonusDamage : action.BaseDamage;
            EnemyManager.Instance.DealDamageToDefaultTarget(damage);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<ChooseCardFromDrawPileToPlayGA>(action =>
        {
            DrawPileUI.Instance.ShowSelectable();
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<CompositeGA>(action =>
        {
            foreach (var subAction in action.Actions)
                ActionSystem.Instance.AddReaction(subAction);
            return EmptyCoroutine();
        });

        ActionSystem.AttachPerformer<DiscardRandomGA>(action =>
        {
            return CardSystem.Instance.PerformRandomDiscards(action.Amount);
        });

        ActionSystem.AttachPerformer<DamageAllEnemiesGA>(action =>
        {
            EnemyManager.Instance.DamageAllEnemies(action.Amount);
            return EmptyCoroutine();
        });
    }
    public GameObject collectRewardsPanel;

    public void EndPlayerTurn()
    {
        Debug.Log("[BattleFlowManager] Player ended turn. Executing enemy phase...");

        // CASE 1: All enemies already dead before enemy turn
        if (EnemyManager.Instance.AllEnemiesDead())
        {
            Debug.Log("[BattleFlowManager] No enemies alive. Showing rewards immediately.");
            if (collectRewardsPanel != null)
                collectRewardsPanel.SetActive(true);
            return;
        }

        // CASE 2: Let enemies act, then check again after delay
        StartCoroutine(WaitForEnemyTurn());
    }

    private IEnumerator WaitForEnemyTurn()
    {
        ActionSystem.Instance.Perform(new EnemyTurnGA(), () =>
        {
            StartCoroutine(CheckForDeathThenProceed());
        });

        yield return null;
    }

    private IEnumerator CheckForDeathThenProceed()
    {
        // Wait 1.5s to let death animations + Destroy() complete
        yield return new WaitForSeconds(1.6f);

        if (EnemyManager.Instance.AllEnemiesDead())
        {
            Debug.Log("[BattleFlowManager] Enemies defeated after their turn. Showing rewards.");
            if (collectRewardsPanel != null)
                collectRewardsPanel.SetActive(true);
        }
        else
        {
            StartNextPlayerTurn(); // Continue turn loop
        }
    }





    private void StartNextPlayerTurn()
    {
        Player.Instance.StartTurn(); // Reset shield & mana here

        ActionSystem.Instance.Perform(new EnemyPrepareIntentGA(), () =>
        {
            Debug.Log("[BattleFlowManager] Enemy intents prepared. Player can act.");
        });
    }

    private static IEnumerator EmptyCoroutine()
    {
        yield return null;
    }

    public IEnumerator EnemyDamageRoutine(int amount)
    {
        float timer = 0f;
        while (!EnemyManager.Instance.AllEnemiesDead() && timer < 3f)
        {
            yield return null;
            timer += Time.deltaTime;
        }


        // Delay to let death animation play
        yield return new WaitForSeconds(1.6f);

        if (EnemyManager.Instance.AllEnemiesDead() && collectRewardsPanel != null)
        {
            collectRewardsPanel.SetActive(true);
            Debug.Log("[BattleFlowManager] All enemies defeated. Showing rewards panel.");
        }
    }

}
