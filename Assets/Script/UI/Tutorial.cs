using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject obj;

    private Button tutorialBtn;

    // Start is called before the first frame update
    void Start()
    {
        tutorialBtn = GetComponent<Button>();
        tutorialBtn.onClick.AddListener(Open);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pad_Start"))
        {
            Open();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            obj.SetActive(false);
        }
    }

    void Open()
    {
        obj.SetActive(true);
        AudioKit.PlayFX("SelectSE", 1.0f);
    }
}