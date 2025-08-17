using UnityEngine;

public class CreditViewer : MonoBehaviour
{
    [SerializeField] private GameObject creditViewer;

    public void ViewCredits()
    {
        if (creditViewer != null)
            creditViewer.SetActive(!creditViewer.activeSelf);
    }
}
