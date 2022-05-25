using UnityEngine;

public class Axe : Weapon
{
    [SerializeField] private int _damage;

    public override void Shoot(Transform shootPoint)
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(shootPoint.position, new Vector2(2, 2), 0);

        foreach (var item in enemies)
        {
            if (item.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
    }
}
