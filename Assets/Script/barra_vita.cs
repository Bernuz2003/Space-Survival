using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barra_vita : MonoBehaviour
{
	[SerializeField] private Slider slider;

	public void updateHealthBar(float corrente, float max) {
	
		slider.value = corrente/max;
	} 
    // Update is called once per frame
    void Update()
    {
        
    }
}
