using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGetForce : MonoBehaviour {

    public BlockVariables blockTtype;
    public int answersNumber;

    Slider slider;
    Material materialSlider;


    void Start()
    {
        slider = GetComponent<Slider>();

        //Image não usa sharedMaterial então tive que instanciar um e atribuir de volta
        materialSlider = GetComponent<Image>().material;
        materialSlider = new Material(materialSlider);
        GetComponent<Image>().material = materialSlider;

        blockTtype.listening(this);
    }
    

    public void changeForce(float force)
    {
        if (!enabled)
            return;

        slider.value = force;
        if(slider.value == slider.maxValue)
        {
            //verifica se estar certo e desativa
            GameObject.FindObjectOfType<ManagerUI>().checkAwerns(answersNumber);
            GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f);
            this.enabled = false;
        }

        materialSlider.SetFloat("_Blender", slider.normalizedValue);
    }

}
