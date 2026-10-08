using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class ComboSystem : MonoBehaviour
{
    public float jumpForce = 6f;
    public float sideForce = 3f;
    public float spinForce = 5f;

    // time we wait after the last key before checking the combo
    public float waitTime = 0.5f;

    public AudioClip flipRightSound;
    public AudioClip jumpUpSound;
    public AudioClip flipLeftSound;

    Rigidbody rb;
    AudioSource audioSource;

    // here we save the keys the player pressed, U = up, D = down
    string keys = "";
    float lastKeyTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) AddKey("U");
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) AddKey("D");
        if (Keyboard.current.qKey.wasPressedThisFrame) AddKey("Q");
        if (Keyboard.current.aKey.wasPressedThisFrame) AddKey("A");

        // when the player stops pressing keys, we check what they wrote
        if (keys != "" && Time.time - lastKeyTime > waitTime)
        {
            CheckCombo();
            keys = "";
        }
    }

    void AddKey(string key)
    {
        keys += key;
        lastKeyTime = Time.time;
        Debug.Log("Keys: " + keys);
    }

    void CheckCombo()
    {
        // ↑ + ↑ + ↓ + ↓ + Q + A  -> jump and flip to the right
        if (keys == "UUDDQA")
        {
            rb.AddForce(new Vector3(sideForce, jumpForce, 0), ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 0, -spinForce), ForceMode.Impulse);
            audioSource.PlayOneShot(flipRightSound);
            Debug.Log("Combo: flip right");
        }
        // ↑ + ↑ + ↑ + ↓ + Q + A  -> jump straight up spinning
        else if (keys == "UUUDQA")
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, spinForce, 0), ForceMode.Impulse);
            audioSource.PlayOneShot(jumpUpSound);
            Debug.Log("Combo: jump up");
        }
        // ↑ + ↑ + ↑  -> jump and flip to the left
        else if (keys == "UUU")
        {
            rb.AddForce(new Vector3(-sideForce, jumpForce, 0), ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 0, spinForce), ForceMode.Impulse);
            audioSource.PlayOneShot(flipLeftSound);
            Debug.Log("Combo: flip left");
        }
        else
        {
            Debug.Log("No combo");
        }
    }
}
