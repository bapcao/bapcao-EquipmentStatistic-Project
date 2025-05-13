using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject Use_btn;
    [SerializeField] private GameObject RentOut_btn;

    public void ClickUse()
    {
        if (WeaponPanel.SelectedPanel != null)
        {
            WeaponStateManager.Instance.SetWeaponState(WeaponPanel.SelectedPanel, WeaponState.Used);
        }
    }

    public void ClickRentOut()
    {
        if (WeaponPanel.SelectedPanel != null)
        {
            WeaponStateManager.Instance.SetWeaponState(WeaponPanel.SelectedPanel, WeaponState.RentedOut);
        }
    }
}