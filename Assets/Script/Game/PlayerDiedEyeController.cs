
using UnityEngine;

public class PlayerDiedEyeController : MonoBehaviour
{
    [SerializeField] private PlayerController _dogPlayerController;
    //[SerializeField] private PlayerController _catPlayerController;
    [SerializeField] private GameObject _dogDiedEyeImage;
    [SerializeField] private GameObject _catDiedEyeImage;
    [SerializeField] private GameObject _GameOverImage;


    // Start is called before the first frame update
    void Start()
    {
        _dogDiedEyeImage.SetActive(false);
        _catDiedEyeImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_GameOverImage.activeSelf == true)
        {
            if(_dogPlayerController.GetActionIndex() == 1)
            {
                _dogDiedEyeImage.SetActive(true);
            }
            else
            {
                _catDiedEyeImage.SetActive(true);
            }
        }
    }
}
