using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContentFitter : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        HorizontalLayoutGroup hg = GetComponent<HorizontalLayoutGroup>();
        int chilCount = transform.childCount-1;
        float Childwith = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        float with = hg.spacing * chilCount+ chilCount* Childwith+hg.padding.left;

        GetComponent<RectTransform>().sizeDelta = new Vector2(with, 291);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
