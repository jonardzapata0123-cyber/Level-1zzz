
using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform door;
    public Transform doorPivot;
    public GameObject interactionText;

    [Header("Opening Settings")]
    public float openAngle = 90f;
    public float openSpeed = 3f;

    private bool playerNearby;
    private bool isOpen;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        if (doorPivot == null)
        {
            Debug.LogError("DoorPivot is not assigned!", this);
            enabled = false;
            return;
        }

        closedRotation = doorPivot.localRotation;

        openRotation = closedRotation *
            Quaternion.Euler(0f, openAngle, 0f);

        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        Quaternion targetRotation =
            isOpen ? openRotation : closedRotation;

        doorPivot.localRotation = Quaternion.Slerp(
            doorPivot.localRotation,
            targetRotation,
            Time.deltaTime * openSpeed
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (interactionText != null)
            {
                interactionText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (interactionText != null)
            {
                interactionText.SetActive(false);
            }
        }
    }
}
