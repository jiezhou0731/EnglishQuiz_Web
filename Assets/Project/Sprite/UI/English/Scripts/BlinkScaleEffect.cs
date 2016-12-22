using UnityEngine;
using System.Collections;

public class BlinkScaleEffect : MonoBehaviour {

	public float fixedScale = 1f;
	public float variantScale = 0.3f;
	public float frequency = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	float index=0;
	void Update () {
		index += Time.deltaTime;
		float scale = Mathf.Abs(variantScale*Mathf.Cos (frequency*index)) + fixedScale;

		transform.localScale = Vector3.one * scale;
	}
}
