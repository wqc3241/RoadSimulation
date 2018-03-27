using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTypeButton : MonoBehaviour {

    [SerializeField] private string buttonName;
    [SerializeField] private Color activeColor = new Color(0, 1, 0, 1);
    [SerializeField] private Color deActiveColor = new Color(1, 1, 1, 1);

    private Button button;
    private Image img;
    private ButtonManager BM;
    [SerializeField]private bool active = false;

	// Use this for initialization
	void Start ()
    {
        buttonName = GetComponentInChildren<Text>().text;
        button = GetComponent<Button>();
        img = GetComponent<Image>();
        BM = GetComponentInParent<ButtonManager>();

    }

    public void setActive(bool boo)
    {
        if (boo)
        {
            img.color = activeColor;
        }
        else
        {
            img.color = deActiveColor;
        }

        active = boo;
    }

    public void sendClickInfo()
    {

        if (BM == null)
        {
            //this is start button;
            GameManager.GM.loadNextScene();
            return; 
        }

        Debug.Log("set info");
        if (!active)
        {
            BM.deActiveAllChild();
            BM.updateTestType(buttonName);
        }

        this.setActive(!active);
    }


    public string getButtonName()
    {
        return buttonName;
    }

}
