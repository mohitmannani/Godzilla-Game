using UnityEngine;
using System.Collections;

public class IPCatapult : IPSwitchableObject
{
    [Tooltip("The upward force exerted on the player by the catapult.")]
    public float upForce;
    [Tooltip("The forward force exerted on the player by the catapult.")]
    public float forwardForce;

    private bool launching = false;

    private float savedUpForce;
    private float savedForwardForce;

    public override void Start()
    {
        base.Start();
        SaveCheckpointState();
    }

    void OnTriggerEnter(Collider other)
    {
        if (MKSceneManager.instance.inputLocked || !activated)
            return;

        if (other.gameObject.CompareTag("PlayerMain") && !launching)
        {
            launching = true;

            Vector3 upVector = transform.TransformDirection(Vector3.up);
            Vector3 forwardVector = transform.TransformDirection(Vector3.forward);

            StartCoroutine("LaunchCatapult");
            other.attachedRigidbody.AddForce(upVector * upForce, ForceMode.Acceleration);
            other.attachedRigidbody.AddForce(forwardVector * forwardForce, ForceMode.Acceleration);
        }
    }

    IEnumerator LaunchCatapult()
    {
        GetComponent<AudioSource>().Play();

        Vector3 startRot = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 endRot = new Vector3(45.0f, 0.0f, 0.0f);

        float duration = 0.125f;
        float timer = 0.0f;

        while (transform.localEulerAngles.x < 45.0f)
        {
            timer += Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(startRot, endRot, timer / duration);
            yield return null;
        }

        timer = 0.0f;
        while (transform.localEulerAngles.x > 0.0f)
        {
            timer += Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(endRot, startRot, timer / duration);
            yield return null;
        }

        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        launching = false;
    }

    public override void SaveCheckpointState()
    {
        base.SaveCheckpointState();

        savedUpForce = upForce;
        savedForwardForce = forwardForce;
    }

    public override void RestoreCheckpointState()
    {
        base.RestoreCheckpointState();

        upForce = savedUpForce;
        forwardForce = savedForwardForce;
    }
}
