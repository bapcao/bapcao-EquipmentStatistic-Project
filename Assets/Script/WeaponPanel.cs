using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public enum WeaponState { None, Used, RentedOut }

public class WeaponPanel : MonoBehaviour, IPointerClickHandler
{
    [Header("Weapon Data")]
    [SerializeField] private WeaponData weaponData;

    [Header("UI Components")]
    [SerializeField] private Image weaponImage;
    [SerializeField] private Image weapon_img;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI dispersionText;
    [SerializeField] private TextMeshProUGUI rateOfFireText;
    [SerializeField] private TextMeshProUGUI reloadSpeedText;
    [SerializeField] private TextMeshProUGUI ammunitionText;
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Outline Settings")]
    [SerializeField] private Outline panelOutline;
    [SerializeField] private Color activeOutlineColor = new Color(1f, 0.5f, 0f);
    [SerializeField] private Color inactiveOutlineColor = Color.clear; // Clear for no outline when inactive

    private static WeaponPanel selectedPanel;
    public static WeaponPanel SelectedPanel => selectedPanel;

    public static List<WeaponPanel> allPanels = new List<WeaponPanel>();
    public WeaponData WeaponData => weaponData;

    private void Awake()
    {
        allPanels.Add(this);
    }

    private void OnDestroy()
    {
        allPanels.Remove(this);
    }

    private void Start()
    {
        UpdateWeaponInfo();
        SetOutlineInactive();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectedPanel != null && selectedPanel != this)
        {
            selectedPanel.SetOutlineInactive();
        }
        selectedPanel = this;
        SetOutlineActive();
        if (weapon_img != null && weaponData != null)
        {
            weapon_img.sprite = weaponData.WeaponSprite;
        }
        UpdateWeaponInfo();
    }

    public void UpdateWeaponInfo()
    {
        if (weaponData == null)
        {
            return;
        }

        if (weaponImage != null) weaponImage.sprite = weaponData.WeaponSprite;
        if (weaponNameText != null) weaponNameText.text = weaponData.WeaponName;
        if (damageText != null) damageText.text = weaponData.Damage.ToString();
        if (dispersionText != null) dispersionText.text = weaponData.Dispersion.ToString();
        if (rateOfFireText != null) rateOfFireText.text = weaponData.RateOfFire.ToString() + " RPM";
        if (reloadSpeedText != null) reloadSpeedText.text = weaponData.ReloadSpeed.ToString() + "%";
        if (ammunitionText != null) ammunitionText.text = weaponData.Ammunition.ToString() + "/100";
        if (statusText != null)
        {
            WeaponState state = WeaponStateManager.Instance.GetWeaponState(this);
            if (state == WeaponState.Used)
            {
                statusText.text = state.ToString();
                statusText.color = new Color32(41, 248, 0, 255); // #29F800 (green)
            }
            else if (state == WeaponState.RentedOut)
            {
                statusText.text = state.ToString();
                statusText.color = new Color32(210, 117, 55, 255); // #D27537 (orange)
            }
            else
            {
                statusText.text = "";
            }
        }
    }

    private void SetOutlineActive()
    {
        if (panelOutline != null)
        {
            panelOutline.effectColor = activeOutlineColor;
            panelOutline.enabled = true;
        }
    }

    private void SetOutlineInactive()
    {
        if (panelOutline != null)
        {
            panelOutline.effectColor = inactiveOutlineColor;
            panelOutline.enabled = false;
        }
    }
}