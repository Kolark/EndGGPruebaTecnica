using UnityEngine;
using UnityEngine.UI;
using TMPro;
//class attached to the UI components that represent the InventorySlot
public class UIInventorySlot : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI keyText; 


    public Button Button => button;
    public Image Img => img;


    public void ActivateKeyText(string text)
    {
        keyText.gameObject.SetActive(true);
        keyText.text = text;
    }

    public void DeActivateKeyText()
    {
        keyText.gameObject.SetActive(false);
    }
}
