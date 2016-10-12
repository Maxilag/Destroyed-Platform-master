using UnityEngine;
using System.Collections;
using DG.Tweening;

public class OVRScreenFadeCenter : MonoBehaviour {
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
		StartCoroutine (FadeOut());
	}

	void OnDestroy()
	{
		if (fadeMaterial != null)
		{
			Destroy(fadeMaterial);
		}
	}
	public IEnumerator FadeOut()
	{
		float elapsedTime = 0.0f;
		Color color = fadeColor;
		color.a = 0f;
		fadeMaterial.color = color;
		isFading = true;
		while (elapsedTime < fadeTime)
		{
			yield return fadeInstruction;
			elapsedTime += Time.deltaTime;
			color.a = Mathf.Clamp01(elapsedTime / fadeTime);
			fadeMaterial.color = color;
		}
		isFading = false;

	}

	/// <summary>
	/// Renders the fade overlay when attached to a camera object
	/// </summary>
	public void OnPostRender()
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
