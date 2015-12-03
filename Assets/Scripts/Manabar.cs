// This script is based on GradientGUIBars

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Manabar : MonoBehaviour
{
	
	//Declare variables
	
	//for the CurrentValue, and the Value it will be after the update
	private float CurrentValue;
	public float Value;
	
	//FadeValue is current amount the bar is faded 
	private float FadeValue;
	//FadeFactor is a little complex, Open the ReadMe file to know more
	public float FadeFactor = 6f;

	public Texture2D Mask;

	//Carries the colors that the GUIbar will be
	public List<Color> GradientColors = new List<Color> ();
	public float refillSpeed = 0.001f;
	
	//These are used for redrawing the GUIBar
	private Gradient g = new Gradient ();
	private GradientColorKey[] gck;
	private GradientAlphaKey[] gak;
	private Color[] MaskPixels; 
	
	//Stanard OnGUI Method
	void OnGUI ()
	{
		if (Value < 1) 
			Value = Value + refillSpeed;
		
		//UpdateBar is a very large function so i'm only excuting it when i have to.
		if (Mathf.Round (CurrentValue * 100f) != Mathf.Round (Value * 100f)) {
			UpdateBar ();
		}

		Color MaxGradientColor = new Color (g.Evaluate (Value * 0.99f).r, g.Evaluate (Value * 0.99f).g, g.Evaluate (Value * 0.99f).b, 1.0f);
		Text text = transform.Find ("Text").GetComponent<Text> ();
		text.color = MaxGradientColor;

		string TextString = ((int)(Value * 100)).ToString () + "%";
		text.text = TextString;
	}
	
	//this method will redraw the bar
	private void UpdateBar ()
	{
		//update the gradient
		UpdateGradient ();
		
		//error handling
		if (g == null) {
			return;
		}

		Texture2D ValueBar = (Texture2D)transform.Find ("Value").GetComponent<RawImage> ().texture;
		
		//for each pixle in the ValueBar, we will change the color to w/e it is in the gradient
		int y = 0;
		while (y < ValueBar.height) {
			int x = 0;
			float xf = 0f;
			while (x < ValueBar.width) {
				Color gC = g.Evaluate (xf / Mask.width);
				
				if (Mask.GetPixel (x, y).a > 0.1f) {
					ValueBar.SetPixel (x, y, gC);
				}
				x = x + 1;
				xf = xf + 1;
			}
			y = y + 1;
		}
		
		//set the new colors on the ValueBar
		ValueBar.Apply ();
	}
	
	//this method will update the gradient
	private void UpdateGradient ()
	{
		//error handling
		if (g == null) {
			return;
		}
		
		//set the new value
		CurrentValue = Value;
		
		//the FadeFactor is used to set the FadeValue, see ReadMe document for more Info
		FadeValue = ((Mathf.Sin ((Value) * 3.14f)) / FadeFactor);
		
		if (FadeFactor == 0) {
			print ("FadeFactor = 0 does not produce a good gradient all the way through the bar");
		}
		
		//clamping values of variables
		FadeFactor = Mathf.Clamp (FadeFactor, -1f, 20f);
		CurrentValue = Mathf.Clamp (CurrentValue, 0f, 1f);
		Value = Mathf.Clamp (Value, 0f, 1f);
		FadeValue = Mathf.Clamp (FadeValue, 0.0001f, 1f);
		
		//create variable to store the colors for the gradient
		gck = new GradientColorKey[GradientColors.Count]; 
		
		//add colors to gradient
		int i = 0;
		float f = 0f;
		while (i < GradientColors.Count) {
			gck [i].color = GradientColors [i];
			gck [i].time = f / (GradientColors.Count - 1);
			i++;
			f++;
		}
		
		//if you do not want to use these colors you can hardcode them like so 
		/*
		 * gck[0].color = [any color];
		 * gck[0].time =  [float number between 0, and 1];
		 * 
		 * gck[1].color = [any color];
		 * gck[1].time =  [float number between 0, and 1];
		 * 
		 * ...etc etc etc
		 * 
		 */
		
		//set the alpha keys for the gradient
		gak = new GradientAlphaKey[3]; 
		gak [0].alpha = 1.0f; 
		gak [0].time = 0.0f; 
		
		gak [1].alpha = 1.0f;
		gak [1].time = CurrentValue - (FadeValue / 2); 
		
		gak [2].alpha = 0.00f;
		gak [2].time = CurrentValue + (FadeValue / 2); 
		
		//add keys to gradient
		g.SetKeys (gck, gak);
		
	}
	
	
	
	
	//The following methods can be used within other code do change how the GUIBar Looks
	public void AddNewColor (Color color, int Key)
	{
		GradientColors.Insert (Key, color);
	}
	
	public void ChangeColor (Color color, int Key)
	{
		GradientColors [Key] = color;
	}
	
	public void RemoveColor (int Key)
	{
		GradientColors.RemoveAt (Key);
	}
	
	public void SetNewValue (float V)
	{
		Value = V;
	}	
	
	public void SetNewValue (double V)
	{
		Value = (float)V;
	}
	
	public void SetNewValue (float V, float MV)
	{
		Value = V / MV;
	}
	
	public void SetNewValue (double V, double MV)
	{
		Value = (float)V / (float)MV;
	}
	
	public void ForceUpdate ()
	{
		UpdateBar ();
	}
	
}
