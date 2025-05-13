using UnityEngine;
using System.Collections.Generic;

public class WeaponStateManager : MonoBehaviour
{
    public static WeaponStateManager Instance { get; private set; }

    private Dictionary<int, WeaponState> weaponStates = new Dictionary<int, WeaponState>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWeaponState(WeaponPanel panel, WeaponState newState)
    {
        if (panel == null || panel.WeaponData == null) return;

        int weaponID = panel.WeaponData.WeaponID;
        WeaponState currentState = GetWeaponState(panel);

        if (currentState == newState) return;

        // Nếu đặt trạng thái Used hoặc RentedOut, xóa trạng thái tương ứng ở các panel khác
        if (newState != WeaponState.None)
        {
            foreach (WeaponPanel otherPanel in WeaponPanel.allPanels)
            {
                if (otherPanel != panel && GetWeaponState(otherPanel) == newState)
                {
                    SetWeaponState(otherPanel, WeaponState.None);
                }
            }
        }

        weaponStates[weaponID] = newState;
        SaveState(weaponID, newState);
        panel.UpdateWeaponInfo();
    }

    public WeaponState GetWeaponState(WeaponPanel panel)
    {
        if (panel == null || panel.WeaponData == null) return WeaponState.None;

        int weaponID = panel.WeaponData.WeaponID;
        if (!weaponStates.ContainsKey(weaponID))
        {
            weaponStates[weaponID] = LoadState(weaponID);
        }
        return weaponStates[weaponID];
    }

    private void SaveState(int weaponID, WeaponState state)
    {
        PlayerPrefs.SetInt($"WeaponState_{weaponID}", (int)state);
        PlayerPrefs.Save();
    }

    private WeaponState LoadState(int weaponID)
    {
        return (WeaponState)PlayerPrefs.GetInt($"WeaponState_{weaponID}", (int)WeaponState.None);
    }

    [ContextMenu("Test Set Used for Selected")]
    public void TestSetUsed()
    {
        if (WeaponPanel.SelectedPanel != null)
        {
            SetWeaponState(WeaponPanel.SelectedPanel, WeaponState.Used);
        }
    }

    [ContextMenu("Test Set RentedOut for Selected")]
    public void TestSetRentedOut()
    {
        if (WeaponPanel.SelectedPanel != null)
        {
            SetWeaponState(WeaponPanel.SelectedPanel, WeaponState.RentedOut);
        }
    }
}