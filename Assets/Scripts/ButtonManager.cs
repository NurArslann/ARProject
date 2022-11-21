using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ButtonManager : MonoBehaviour
{
    private Button btn;
    [SerializeField] private RawImage buttonImage;
    public GameObject furniture;
    private int _itemId;
    private Sprite _buttonTexture;

    public Sprite ButtonTexture
    {
        set
        {
            _buttonTexture = value;
            buttonImage.texture = _buttonTexture.texture;
        }
    }

    public int ItemId
    {
        set
        {
            _itemId = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);

    }
    void Update()
    {
        if (UIManager.Instance.OnEntered(gameObject))
        {
            transform.localScale = Vector3.one*2;
            //UIManagerde objenin üzerine resim gelip gelmediði kontrol edilir eðer öyleyse boyutu 2 katýna çýkar
           // transform.DOScale(Vector3.one * 3, 0.2f);//0.2f süresini belirliyor

        }
        else
        {
            //transform.localScale = Vector3.one;
            transform.DOScale(Vector3.one, 0.2f);
        }

    }

    void SelectObject()
    {
        //DataHander.Instance.furniture = furniture;
        DataHander.Instance.SetFurniture(_itemId);
    }
}
