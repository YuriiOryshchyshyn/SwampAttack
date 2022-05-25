using System.Collections.Generic;
using UnityEngine;

public class WeaponsTransition : MonoBehaviour
{
    [SerializeField] private List<Transit> _animationTransitions;

    private Dictionary<Weapon, AnimationClip> _toWeaponTransitAnimationsDictionary;

    private void Awake()
    {
        _toWeaponTransitAnimationsDictionary = new Dictionary<Weapon, AnimationClip>();
        foreach (var transit in _animationTransitions)
        {
            _toWeaponTransitAnimationsDictionary.Add(transit.To, transit.Clip);
        }
    }

    public void TransitAnimation(Weapon toWeapon, Animator animator)
    {
        if (!_toWeaponTransitAnimationsDictionary.ContainsKey(toWeapon))
            return;

        animator.Play(_toWeaponTransitAnimationsDictionary[toWeapon].name);
    }
}

[System.Serializable]
public class Transit
{ 
    [SerializeField] private Weapon _to;
    [SerializeField] private AnimationClip _transitionClip;

    public Weapon To => _to;
    public AnimationClip Clip => _transitionClip;
}
