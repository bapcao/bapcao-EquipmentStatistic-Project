using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Information")]
    [SerializeField] private int weaponID;
    [SerializeField] private string weaponName;
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private int damage;
    [SerializeField] private int dispersion;
    [SerializeField] private int rateOfFire;
    [SerializeField] private int reloadSpeed;
    [SerializeField] private int ammunition;

    [SerializeField] private GameObject projectilePrefab;

    [Header("Weapon Sounds")]
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip reloadSound;

    public int WeaponID => weaponID;
    public string WeaponName => weaponName;
    public int Damage => damage;
    public int Dispersion => dispersion;
    public int RateOfFire => rateOfFire;
    public int ReloadSpeed => reloadSpeed;
    public int Ammunition => ammunition;
    public Sprite WeaponSprite => weaponSprite;
}