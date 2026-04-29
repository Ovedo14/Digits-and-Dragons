using UnityEngine;

public class CharacterAnimatorUI : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private AnimatorOverrideController _overrideController;

    void OnEnable()
    {
        EventBus.Subscribe<OnDamageDealt>(HandleDamageDealt);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnDamageDealt>(HandleDamageDealt);
    }

    void Start()
    {
        CharacterData character = RunManager.Instance.CurrentCharacter;

        _spriteRenderer.sprite = character.CharacterSprite;

        //Create an override controller and swap animations
        _overrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _overrideController;

        _overrideController["Idle"] = character.IdleAnimation;
        _overrideController["Attack"] = character.AttackAnimation;
    }

     private void HandleDamageDealt(OnDamageDealt evt)
    {
        if (evt.ToPlayer) return;
        PlayAttack();
    }
    public void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }
}