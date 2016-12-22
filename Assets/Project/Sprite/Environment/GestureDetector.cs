using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Gesture2D;

public class GestureDetector : MonoBehaviour {

	private Transform swipeTrail;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		TouchRecorder.Update ();
		if (TouchRecorder.GetCurrentFrame () != null) {
			//Vector2 touchPos = new Vector2 (TouchRecorder.GetCurrentFrame ().touches [0].position.x, TouchRecorder.GetCurrentFrame ().touches [0].position.y);
		}
	}
}



namespace Gesture2D
{
		
	public class SwipeDetector {

		static public void Check(){
			if (CheckTap (0.01f,2f)) {
				EventManager.TriggerEvent ("GestureRecognizer_Tap");
			}
			if (CheckTrend ("x", "+", 0.06f)) {
				EventManager.TriggerEvent ("GestureRecognizer_SwipeRight");
			}
			if (CheckTrend ("x", "-", 0.06f)) {
				EventManager.TriggerEvent ("GestureRecognizer_SwipeLeft");
			}
			/*
			if (CheckTrend ("y", "-", 0.06f)) {
				EventManager.TriggerEvent ("GestureRecognizer_SwipeDown");
			}

			if (CheckTrend ("y", "+", 0.2f)) {
				EventManager.TriggerEvent ("GestureRecognizer_SwipeUp");
			}*/
		}

		static public bool CheckDuration(int startFrame, float maxDuration) {
			if (Time.fixedTime - TouchRecorder.frames [startFrame].fixedTime > maxDuration) {
				return false;
			} else {
				return true;
			}
		}

		static public bool CheckTap(float threshold=0.01f, float maxDuration=0.8f) {
			if (TouchRecorder.GetCurrentFrame() != null) {
				return false;
			}
			int currentFrameId = TouchRecorder.currentFrame;
			int lastFrameId = Util.NumberRotate (currentFrameId, -1, TouchRecorder.frames.Length);
			if (TouchRecorder.frames [lastFrameId] == null) {
				return false;
			}
			int q = lastFrameId;
			int p = Util.NumberRotate (q, -1, TouchRecorder.frames.Length);
			while (p!=currentFrameId && TouchRecorder.frames [p] != null && CheckDuration (p, maxDuration)) {
				q = p;
				p = Util.NumberRotate (q, -1, TouchRecorder.frames.Length);
			}
			if (p!=currentFrameId && TouchRecorder.frames [p] == null) {
				if ((Mathf.Abs (TouchRecorder.GetFloatAt (lastFrameId, "x") - TouchRecorder.GetFloatAt (q, "x")) < threshold) 
					&& (Mathf.Abs (TouchRecorder.GetFloatAt (lastFrameId, "y") - TouchRecorder.GetFloatAt (q, "y")) < threshold)){
					return true;
				}
			}
			return false;
		}

		static public bool CheckTrend(string axis, string trend, float threshold=0.1f, float maxDuration=0.5f) {
			if (TouchRecorder.GetCurrentFrame() == null || TouchRecorder.GetCurrentFrame().touches.Count == 0) {
				return false;
			}
			int q = TouchRecorder.currentFrame;
			int p = Util.NumberRotate (q, -1, TouchRecorder.frames.Length);
			for (int trendP = trend.Length-1; trendP >-1; trendP--) {
				int startFrame = q;
				if (trend [trendP] == '-') {
					if ( TouchRecorder.frames [p]==null || CheckDuration(p,maxDuration) && TouchRecorder.GetFloatAt(q,axis) >=  TouchRecorder.GetFloatAt(p,axis)) {
						return false;
					}
					while ( TouchRecorder.frames [p]!=null && CheckDuration(p,maxDuration) && TouchRecorder.GetFloatAt(q,axis)  < TouchRecorder.GetFloatAt(p,axis) ) {
						q = p;
						p = Util.NumberRotate (q, -1, TouchRecorder.frames.Length);
						if (p == TouchRecorder.currentFrame) {
							if (trendP == 0 && Mathf.Abs(TouchRecorder.GetFloatAt(startFrame,axis)- TouchRecorder.GetFloatAt(q,axis)  )>threshold) {
								return true;
							} else {
								return false;
							}
						}
					}

				} else if (trend [trendP] == '+') {
					if (TouchRecorder.frames [p]==null || CheckDuration(p,maxDuration) && TouchRecorder.GetFloatAt(q,axis)  <= TouchRecorder.GetFloatAt(p,axis) ) {
						return false;
					}
					while (TouchRecorder.frames [p]!=null && CheckDuration(p,maxDuration) && TouchRecorder.GetFloatAt(q,axis)  > TouchRecorder.GetFloatAt(p,axis) ) {
						q = p;
						p = Util.NumberRotate (q, -1, TouchRecorder.frames.Length);	
						if (p == TouchRecorder.currentFrame) {
							if (trendP == 0 && Mathf.Abs(TouchRecorder.GetFloatAt(startFrame,axis)-TouchRecorder.GetFloatAt(q,axis)  )>threshold) {
								return true;
							} else {
								return false;
							}
						}
					}
				}
				if (Mathf.Abs(TouchRecorder.GetFloatAt(startFrame,axis)-TouchRecorder.GetFloatAt(q,axis))<threshold){
					return false;
				}
			}
			return true;
		}
	}

	class TouchRecorder{
		static public int currentFrame = -1;
		static public Frame[] frames = new Frame[100];
		static public Frame GetCurrentFrame(){
			if (currentFrame >= 0) {
				return frames [currentFrame];
			} else {
				return null;
			}
		}
		static public void Update(){
			currentFrame = Util.NumberRotate (currentFrame, 1, frames.Length);
			frames [currentFrame] = Frame.CaptureCurrentFrame ();
		}

		static public float GetFloatAt(int frameId, string axis){
			switch (axis) {
			case "x":
				return frames [frameId].touches[0].relativePosition.x; 
			case "y":
				return frames [frameId].touches[0].relativePosition.y;
			default:
				return 0;
			}
		}
	}

	class Frame{
		public List<Touch> touches = new List<Touch> ();
		public float fixedTime;
		public static bool mouseButtonPressed = false;

		static public Frame CaptureCurrentFrame(){
			// On Phone
			if (Input.touchCount > 0) {
				Frame currentFrame = new Frame ();
				for (int i = 0; i < Input.touchCount; ++i) {
					Touch newTouch = new Touch (Input.GetTouch (i).phase, Input.GetTouch (i).position);
					currentFrame.touches.Add (newTouch);
				}
				if (currentFrame.touches.Count == 0) {
					return null;
				}
				currentFrame.fixedTime = Time.fixedTime;
				return currentFrame;
			}
			// On PC
			if (Input.GetMouseButtonUp(0) ==true ) {
				mouseButtonPressed = false;
			}
			if (Input.GetMouseButtonDown(0) ==true ) {
				mouseButtonPressed = true;
			}

			if (mouseButtonPressed ==true ) {
				Frame currentFrame = new Frame ();
				Touch newTouch = new Touch (TouchPhase.Moved, Input.mousePosition);
				currentFrame.touches.Add (newTouch);
				currentFrame.fixedTime = Time.fixedTime;
				return currentFrame;
			}

			return null;
		}

		override public string ToString(){
			string str = "";
			for (int i = 0; i < touches.Count; i++) {
				str += touches [i].ToString ()+" ";
			}
			return str;
		}
	}

	class Touch{
		public Vector2 position;
		public Vector2 relativePosition;
		public TouchPhase touchPhase;

		public Touch(TouchPhase touchPhase, Vector2 position){
			this.position = position;
			this.relativePosition = new Vector2 (position.x/Screen.width,position.y/Screen.height);
			this.touchPhase = touchPhase;
		}

		override public string ToString(){
			string st = this.relativePosition+" "+touchPhase;
			return st;
		}
	}


}