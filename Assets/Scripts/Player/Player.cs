using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WeaponsTransition))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private Animator _animator;
    private Weapon _currentWeapon;
    private int _currentHealth;
    private int _currentWeaponNumber = 0;
    private float _rechargeWeaponTime;
    private WeaponsTransition _weaponTransitions;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction Die;

    public int Money { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _weaponTransitions = GetComponent<WeaponsTransition>();
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentHealth = _health;
        HealthChanged?.Invoke(_currentHealth, _health);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _rechargeWeaponTime <= 0)
        {
            MakeShoot();
            _rechargeWeaponTime = _currentWeapon.RechargeTime;
        }

        _rechargeWeaponTime -= Time.deltaTime;
    }

    private void MakeShoot()
    {
        _currentWeapon.Shoot(_shootPoint);
        _animator.SetTrigger(_currentWeapon.ShootName);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
    }

    public void OnEnemyDied(int reward)
    {
        Money += reward;
    }

    internal void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Die?.Invoke();
            Destroy(gameObject);
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        _weaponTransitions.TransitAnimation(weapon, _animator);
        _animator.SetInteger("CurrentWeapon", (int)weapon.Index);
    }
}