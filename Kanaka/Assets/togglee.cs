using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class togglee : MonoBehaviour
{
    // Start is called before the first frame update

    private RectTransform m_rectTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MarkLayoutForRebuild();
    }

    void MarkLayoutForRebuild()
    {
        //Debug.Log("MarkLayoutForRebuild() called.");

        if (m_rectTransform == null)
            m_rectTransform = GetComponent<RectTransform>();

        if (m_rectTransform != null)
            LayoutRebuilder.MarkLayoutForRebuild(m_rectTransform);
    }
}
