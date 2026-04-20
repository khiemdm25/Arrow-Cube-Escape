# Arrow Cube Escape

## 1.Kiến trúc Class


Project được xây dựng với các class chính:

- **LineRendererSnapFixer**: Chỉnh sửa hình ảnh Positions Size Index của line
- **LineRendererHead**: Xác định vị trí đầu, hướng của line
- **LineSegmeniColliderSpawer2D**: Tạo ra các collider, điều chỉnh khích thước hướng xoay của collider
- **LineAnimation**: Tạo hiệu ướng chuyển động cho line
- **LineRaycastGun, LineHitChecker**: Kiểm tra bằng Layer, collider xem line có bị chặn hay không
- **LineClick**: Xác định line được chọn và kiểm tra xem line được di chuyển hay không
- **LevelSO**: lưu dưu liệu level(Map, điểm) bằng ScriptableObject
- **LevelData, LevelManager**: Quản lý các level bằng ScriptableObject và PlayerPrefs của người chơi, khóa hoặc mở nếu đủ điều kiện
- **RotateCube**: Xoay khối lập phương theo chiều lên, xuống, trái, phải
- **LevelUIController, LevelSpawner**: Chọn level hợp lệ và sinh ra level đó
- **GameManager**: Quản lí thắng hoặc chưa hoàn thành của level người chơi

Luồng hoạt động:

1. LevelUIController -> LevelManager -> LoadLevel -> LevelSO
2. LevelSpawner 
3. LineClick -> LineRaycastGun -> LineAnimation
4. GameManager


### 2.Lời Giải


 - **Level 1**: Right -> Left -> Top -> Forward -> Top -> Down
 - **Level 2**: Left -> Top -> Right -> Right -> Left -> Right -> Top -> Right -> Down
 - **Level 3**: Top -> Forward -> Right -> Down -> Left -> Top -> Right -> Left -> Down ->Forward
 

#### 3.Nhưng gì đã làm được


- Spawner level theo cấp độ 
- Arrow movement, check, Input System
- ScriptableObject, PlayerPrefs và 3 Level mẫu
- Có DOTween và Coroutine
- Chỉ 1 scene Gameplay
- UI overlay cho Win


##### 4.Những gì chưa làm được


- Chưa có UI hoàn chỉnh
- Chưa có thật toán kiểm tra lời giải
- Chưa có hoàn tác nước đi
- Chưa có chấm điểm star rating