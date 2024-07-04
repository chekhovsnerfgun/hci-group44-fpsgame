using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject winText; // Assign this in the Inspector

    void Start()
    {
        if (winText != null)
        {
            winText.SetActive(false); // Ensure the text is initially hidden
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You Win!");
            if (winText != null)
            {
                winText.SetActive(true); // Display the win text
            }
            // Additional win logic can go here
        }
    }
}

