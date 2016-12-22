using UnityEngine;
using System.Collections;

public class UIPanelHeightAdapter : MonoBehaviour {
		public bool topFixed = true;
		public float heightInPercentage;
		void Start () {
		}	

		// Update is called once per frame
		void Update () {
			if (topFixed) {
				GetComponent<UIPanel> ().bottomAnchor.Set (1f, -Screen.height * heightInPercentage*1.5f);
			} else {
				GetComponent<UIPanel> ().topAnchor.Set (0f, Screen.height * heightInPercentage*1.5f);
			}
		}
	}
