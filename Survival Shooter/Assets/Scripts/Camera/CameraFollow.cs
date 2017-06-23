using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;	//目标位置信息
	public float smoothing = 5.0f;	//摄像机跟随移动时的平滑度
	Vector3 offset;	//摄像机与主角之间的距离

	// Use this for initialization
	void Start () {
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		//增加鼠标滑轮放大缩小的功能
		if (Input.GetAxis ("Mouse ScrollWheel") < 0)	//鼠标滑轮向后滑
		{
			//增加视野，缩小功能
			if(Camera.main.fieldOfView <= 100)
				Camera.main.fieldOfView += 2;
			if(Camera.main.orthographicSize <= 20)
				Camera.main.orthographicSize += 0.5f;
		}
		if (Input.GetAxis ("Mouse ScrollWheel" ) > 0)	//鼠标向前滑
		{
			if(Camera.main.fieldOfView > 2)
				Camera.main.fieldOfView -= 2;
			if(Camera.main.orthographicSize >= 1)
				Camera.main.orthographicSize -= 0.5f;
		}

		Vector3 targetCamPos = target.position + offset;	//目标距离
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		//设置摄像机的位置，通过Lerp(过渡函数)，从一个值向另一个值过渡差值运算
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
