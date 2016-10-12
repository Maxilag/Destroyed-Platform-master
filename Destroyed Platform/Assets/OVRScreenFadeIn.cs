using UnityEngine;
using System.Collections;

public class OVRScreenFadeIn : MonoBehaviour {
	public float fadeTime = 1.0f;
	private float dist;

	/// <summary>
	/// The initial screen color.
	/// </summary>
	public Color fadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);

	private Material fadeMaterial = null;
	private bool isFading = false;
	private YieldInstruction fadeInstruction = new WaitForEndOfFrame();
	void Awake()
	{
		// create the fade material
		fadeMaterial = new Material(Shader.Find("Oculus/Unlit Transparent Color"));
	}

	public void OnEnable(){
		StartCoroutine (FadeIn());
	}

	void OnDestroy()
	{
		if (fadeMaterial != null)
		{
			Destroy(fadeMaterial);
		}
	}
		
	public IEnumerator FadeIn()
		{
			float elapsedTime = 0.0f;
			fadeMaterial.color = fadeColor;
			Color color = fadeColor;
			isFading = true;
			while (elapsedTime < fadeTime)
			{
				yield return fadeInstruction;
				elapsedTime += Time.deltaTime;
				color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
				fadeMaterial.color = color;
			}
			isFading = false;
		}

	void OnPostRender()
	{
		if (isFading)
		{
			fadeMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Color(fadeMaterial.color);
			GL.Begin(GL.QUADS);
			GL.Vertex3(0f, 0f, -12f);
			GL.Vertex3(0f, 1f, -12f);
			GL.Vertex3(1f, 1f, -12f);
			GL.Vertex3(1f, 0f, -12f);
			GL.End();
			GL.PopMatrix();
		}
	}
}
