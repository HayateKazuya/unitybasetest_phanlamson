using UnityEngine;

public enum MoveDirection
{
    Up,         
    Down,       
    Left,       
    Right,      
    Forward,    
    Backward    
}

[System.Serializable]
public class PlatformSetup
{
    public Transform platform;                 
    public MoveDirection direction;            // Hướng bục sẽ ĐÂM RA
    public float distance = 4f;                
    
    [HideInInspector] public Vector3 targetPos; // Vị trí đích (chính là vị trí bạn xếp trên map)
}

[RequireComponent(typeof(BoxCollider))]
public class Platform : MonoBehaviour, Interface 
{
    [Header("Platforms Configuration")]
    public PlatformSetup[] platforms;          
    public float moveSpeed = 5f;

    private bool isActivated = false;

    private void Start()
    {
        foreach (var setup in platforms)
        {
            if (setup.platform != null)
            {
                // 1. Lưu vị trí bạn đang xếp trên Scene làm ĐÍCH ĐẾN CUỐI CÙNG
                setup.targetPos = setup.platform.position;

                Vector3 moveVector = Vector3.zero;
                switch (setup.direction)
                {
                    case MoveDirection.Up:      moveVector = Vector3.up; break;
                    case MoveDirection.Down:    moveVector = Vector3.down; break;
                    case MoveDirection.Left:    moveVector = Vector3.left; break;
                    case MoveDirection.Right:   moveVector = Vector3.right; break;
                    case MoveDirection.Forward: moveVector = Vector3.forward; break;
                    case MoveDirection.Backward:moveVector = Vector3.back; break;
                }

                // 2. Tính toán vị trí xuất phát (lùi lại ngược hướng đâm ra để giấu vào tường/sàn)
                Vector3 startPos = setup.targetPos - (moveVector * setup.distance);
                
                // 3. Vừa vào game là dịch chuyển bục nhảy giấu đi ngay lập tức
                setup.platform.position = startPos;
            }
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            foreach (var setup in platforms)
            {
                if (setup.platform != null)
                {
                    // Từ vị trí đang bị giấu, trượt ra đúng vị trí bạn đã xếp trên Scene
                    setup.platform.position = Vector3.MoveTowards(
                        setup.platform.position, 
                        setup.targetPos, 
                        moveSpeed * Time.deltaTime
                    );
                }
            }
        }
    }

    public void Interact()
    {
        if (isActivated) return;
        isActivated = true;
        
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.green;
        }
    }
}