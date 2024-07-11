using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelFollow : MonoBehaviour
{

    public Text RH_Label;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 RH_LabelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        RH_Label.transform.position = RH_LabelPos;
        
    }
}
