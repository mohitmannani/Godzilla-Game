using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoSystem : MonoBehaviour
{
    #region Ammo Settings

    [Header("Ammo Settings")]
    public int maxAmmo = 30;
    public int currentAmmo;

    #endregion

    #region Reload Settings

    [Header("Reload Settings")]
    public float reloadTime = 1f;
    private bool isReloading = false;

    #endregion

    #region UI References

    [Header("UI References")]
    public Text ammoText; // Public Text field for direct assignment

    #endregion

    #region Initialization

    private void Start()
    {
        InitializeAmmo();
        UpdateAmmoText();
    }

    private void InitializeAmmo()
    {
        currentAmmo = maxAmmo;
    }

    #endregion

    #region Main Update Loop

    private void Update()
    {
        if (isReloading)
            return;

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    #endregion

    #region Ammo Actions

    private void Fire()
    {
        if (currentAmmo > 0)
        {
            Debug.Log("Firing! Ammo left: " + currentAmmo);
            currentAmmo--;
            UpdateAmmoText();
        }
        else
        {
            Debug.Log("Out of ammo! Reload!");
        }
    }

    public IEnumerator Reload() // Made this public
    {
        if (CanReload())
        {
            isReloading = true;
            Debug.Log("Reloading...");

            // Play reload animation or sound if necessary
            yield return new WaitForSeconds(reloadTime);

            currentAmmo = maxAmmo;
            isReloading = false;

            Debug.Log("Reloaded! Current ammo: " + currentAmmo);
            UpdateAmmoText();
        }
    }

    #endregion

    #region Helper Methods

    public bool CanReload() // Made this public
    {
        return currentAmmo < maxAmmo;
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo;
        }
    }

    #endregion
}


