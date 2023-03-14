using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(PlayerCombat), typeof(PlayerMovement))]
public class PlayerManager : MonoBehaviour
{
    private float MaxHealth = 100;
    private float MaxStamina = 100;
    [Range(0, 100)]
    [SerializeField] private int health = 100;

    [Range(0, 100)] 
    [SerializeField] private int stamina = 100;
    public int Health
    {
        get
        { 
            return health;
        }
        set
        {
            if ((value - health) > 0)
            {
                
            }else if ((value - health) < 0)
            {
                StartCoroutine(TakeDamage(0.3f));
                _animator.SetTrigger("Hit");
            }

            healthBar.fillAmount = (value / MaxHealth);

            health = Mathf.Max(0, (int)Mathf.Min(value, MaxHealth));
        }
    }

    public int Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            staminaBar.fillAmount = (value / MaxStamina);
            
            stamina = Mathf.Max(0, (int)Mathf.Min(value, MaxStamina));
        }
    }

    [Space(20)] 
    [Header("Components")] 
    public Camera cam;
    public SpriteRenderer _sr;
    public Animator _animator;
    public PlayerCombat pc;
    public PlayerMovement pm;

    [Space(20)] 
    [Header("UI")] 
    public Image healthBar;
    public Image staminaBar;

    [Space(20)] 
    [Header("Other")] 
    public Vector2 _mousePosition;

    public void Update()
    {
        _mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private IEnumerator TakeDamage(float duration)
    {
        pc.weaponTransform.gameObject.SetActive(false);
        _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 0.5f);
        yield return new WaitForSeconds(duration);
        _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1);
        pc.weaponTransform.gameObject.SetActive(true);
    }
}
