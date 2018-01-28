using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainMenuButtonEnum { START , CREDITS, BACK};

public class UIMainMenuButtonScript : MonoBehaviour {

    public MainMenuButtonEnum thisButtonType;
    private Collider2D col;

    private Vector3 startScale;


	// Use this for initialization
	void Start () {
        col = this.GetComponent<Collider2D>();
        startScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, startScale, Time.deltaTime * 8f);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            switch (thisButtonType)
            {
                case MainMenuButtonEnum.START:
                    SceneManager.LoadScene("TestScene");
                    break;
                case MainMenuButtonEnum.CREDITS:
                    SceneManager.LoadScene("Credits");
                    break;
                case MainMenuButtonEnum.BACK:
                    SceneManager.LoadScene("MainMenu");
                    break;
                default:
                    SceneManager.LoadScene("MainMenu");
                    break;
            }
        }

        if (Input.GetMouseButton(0))
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, startScale * 1.5f, Time.deltaTime * 8f);
        }
    }

}
