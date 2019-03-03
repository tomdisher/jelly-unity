using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {

	[SerializeField]
	private RectTransform itemsBarRect;


	// Use this for initialization
	void Start () {
		if (itemsBarRect == null){
			Debug.LogError ("No items bar item referenced");
		}
		
	}

	public void SetItemCount(int _cur, int _max) {
		float _value = (float)_cur / _max;
		itemsBarRect.localScale = new Vector3 (_value, itemsBarRect.localScale.y, itemsBarRect.localScale.z);

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
