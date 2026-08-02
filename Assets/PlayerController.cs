using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Cấu hình bắn đạn")]
    public GameObject AWM_Bullet;    // Kéo Prefab viên đạn vào đây
    public Transform firePoint;        // Vị trí đạn bay ra (nếu không có, đạn sẽ tự ra từ tâm nhân vật)
    public float bulletSpeed = 25f;    // Tốc độ bay của viên đạn

    private float nextFireTime = 0f;   // Thời điểm tiếp theo được phép bắn
    private float cooldownTime = 1f;   // Thời gian hồi chiêu (5 giây)

    void Update()
    {
        // Kiểm tra nếu bấm phím Space VÀ thời gian hiện tại đã vượt qua thời gian chờ
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            // Cập nhật thời điểm tiếp theo được bắn = thời gian hiện tại + 5 giây
            nextFireTime = Time.time + cooldownTime;
        }
    }

    void Shoot()
    {
        // 1. Xác định vị trí và góc quay để tạo đạn
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        Quaternion spawnRotation = firePoint != null ? AWM_Bullet.transform.rotation : transform.rotation;

        // 2. Tạo bản sao của viên đạn (Spawn)
        GameObject bulletClone = Instantiate(AWM_Bullet, spawnPosition, spawnRotation);

        // 3. Tìm thành phần Rigidbody (hoặc Rigidbody2D nếu là game 2D) để đẩy đạn bay về phía trước
        Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity =  Vector3.back * bulletSpeed; 
            // Lưu ý: Nếu dùng Unity phiên bản cũ hơn 2023, thay 'linearVelocity' bằng 'velocity' nhé!
        }

        // 4. Tự động xóa bản sao này sau 3 giây
        Destroy(bulletClone, 3f);
    }
}