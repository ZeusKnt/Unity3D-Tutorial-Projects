using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;

    
    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;         


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }
    

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
		m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
		EngineAudio ();
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
		//如果什么按键都没有按
		if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f)
		{
			//如果当前播放的声音是正在移动的音频
			if (m_MovementAudio.clip == m_EngineDriving)
			{
				//把音频设置为待机时的声音
				m_MovementAudio.clip = m_EngineIdling;
				//音调在上面定义的区间里取一个随机值
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		}
		else
		{
			//当按下某个按键时，播放的是待机时的音频
			if(m_MovementAudio.clip == m_EngineIdling)
			{
				m_MovementAudio.clip = m_EngineDriving;
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play();
			}
		}
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
		Move ();
		Turn ();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
		//创建一个向量用来保存移动的距离
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

		//改变当前物体的距离
		m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
		//每帧输入的值转化成角度值
		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

		//旋转的角度
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		//应用旋转的角度
		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
    }
}