using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float speed = 6f;   //设置玩家的移动速度
	Vector3 movement;          // 存储玩家移动的方向,是个向量类型 
	Animator anim;             //定义一个动画器类型的变量
	Rigidbody playerRigidbody; //定义一个刚体类型的变量
	int floorMask;            //定义一个int型的变量,存储射线将要与其碰撞的地板层
	float camRayLength = 100f;  //定义摄像机射线的长度
	
	// Use this for initialization
	void Start () {
		
		floorMask = LayerMask.GetMask ("Floor"); //获取到地板层,射线只与这一层的物体碰撞
		anim = GetComponent <Animator> ();         //获取Player对象上的Animator组件
		playerRigidbody = GetComponent <Rigidbody> (); //获取Player对象上的刚体组件
	}
	
	//固定更新函数，默认设置每秒50帧，也就是每隔0.02s执行一次该函数
	void FixedUpdate ()
	{
		
		float h = Input.GetAxisRaw("Horizontal"); //获取用户键盘上的按键，对应水平按键 A,D （方向键的左右）
		float v = Input.GetAxisRaw("Vertical"); //获取用户键盘上的按键，对应垂直按键W,S （方向键的上下）
		
		
		Move (h, v);  //Player对象运动函数
		
		
		Turning (); //Player对象旋转函数，使得Player对象转向鼠标光标的方向
		
		
		Animating (h, v);  //Player对象状态逻辑
	}
	
	
	void Move (float h, float v)
	{
		
		movement.Set (h, 0f, v);  //获取按键的信息
		
		
		movement = movement.normalized * speed * Time.deltaTime; //normalizer单位化向量 *速度*时间= 位移
		
		playerRigidbody.MovePosition (transform.position + movement); //设置Player对象的位置
	}
	
	
	void Turning (){
		
		//根据用户鼠标的点击位置，计算出一条摄像机射线
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition); 
		
		
		RaycastHit floorHit;
		
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			//地板上的点击位置-角色当前的位置,向量的方向：Player对象指向鼠标的点击位置
			Vector3 playerToMouse = floorHit.point - transform.position; 
			
			
			
			playerToMouse.y = 0f;  //y方向为0
			
			
			Quaternion newRotatation = Quaternion.LookRotation (playerToMouse); //旋转四元素
			
			
			playerRigidbody.MoveRotation (newRotatation); //设置Player玩家的旋转信息
		}
	}
	
	
	void Animating (float h, float v)
	{
		//若用户按上了WSAD（即运动方向键），则角色状态为运动状态，否则为Idel状态
		bool walking = h != 0f || v != 0f;
		
		
		anim.SetBool ("IsWalking", walking); 
	}
	
}