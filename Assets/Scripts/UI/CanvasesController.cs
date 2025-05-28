using UnityEngine;

public class CanvasesController : MonoBehaviour
{
    [SerializeField] private Canvas cashCanvas;
    [SerializeField] private Canvas questsCanvas;
    [SerializeField] private Canvas endCanvas;

    public void DisableCanvases()
    {
        cashCanvas.gameObject.SetActive(false);
        questsCanvas.gameObject.SetActive(false);
    }

    public void EnableEnd()
    {
        endCanvas.gameObject.SetActive(true);
    }
}