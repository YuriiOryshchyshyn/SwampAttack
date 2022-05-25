using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed;
    [SerializeField] protected Bullet _bullet;
    [SerializeField] private float _rechargeTime;
    [SerializeField] private string _ShootAnimationName;
    [SerializeField] private Weapons _weaponIndex;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;
    public float RechargeTime => _rechargeTime;
    public string ShootName => _ShootAnimationName;
    public Weapons Index => _weaponIndex;

    public abstract void Shoot(Transform shootPoint);

    internal void Buy()
    {
        _isBuyed = true;
    }
}
public enum Weapons
{
    Gun = 0,
    Axe = 1
}
