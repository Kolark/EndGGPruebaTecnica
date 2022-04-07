using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
}
