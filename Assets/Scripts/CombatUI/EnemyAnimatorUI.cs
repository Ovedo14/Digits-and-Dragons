using UnityEngine;

public class EnemyAnimatorUI : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private EnemyManager _enemyManager;

    [SerializeField] private Vector2 _bossOffset = new Vector2(4.2f, 0.5f);


    private AnimatorOverrideController _overrideController;

    void OnEnable()
    {
        EventBus.Subscribe<OnDamageDealt>(HandleDamageDealt);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnDamageDealt>(HandleDamageDealt);
    }

    public void SetupEnemy(EnemyData enemy)
    {
        _spriteRenderer.sprite = enemy.EnemySprite;

        // Check if this is the final boss, adjusts if yes
        if (RunManager.Instance.IsFinalBoss) {
            transform.localPosition = _bossOffset;
            transform.rotation = Quaternion.identity;
        }

        _overrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _overrideController;

        _overrideController["Idle"] = enemy.IdleAnimation;
        _overrideController["Attack"] = enemy.AttackAnimation;
    }

    private void HandleDamageDealt(OnDamageDealt evt)
    {
        if (!evt.ToPlayer) return;
        PlayAttack();
    }

    private void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }
}