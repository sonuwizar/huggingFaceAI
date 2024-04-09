using UnityEngine;

public class InputTouchHit : MonoBehaviour
{
	private int count;
	private Vector3 initialPosition;
	public delegate void ScreenTouchActions (RaycastHit arg);
	public static event ScreenTouchActions OnScreenTouch;

	void FixedUpdate ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (count < 1)
			{
				initialPosition = Input.mousePosition;
				count++;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
            Debug.Log("Click");
			RaycastHit hit;
#if UNITY_EDITOR
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
#else 
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
			count = 0;
			if (Physics.Raycast (ray, out hit)) {
				
				if (OnScreenTouch != null)
                {
					OnScreenTouch (hit);
				}
			}
		}
	}




}
