using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunScipt : MonoBehaviour
{
    [Header("Cấu hình bắn đạn")]
    public GameObject Bullet;    // Kéo Prefab viên đạn vào đây
    public Transform firePoint;        // Vị trí đạn bay ra (nếu không có, đạn sẽ tự ra từ tâm nhân vật)
    public float bulletSpeed = 25f;    // Tốc độ bay của viên đạn

    private float nextFireTime = 0f;   // Thời điểm tiếp theo được phép bắn
    private float cooldownTime = 1f;   // Thời gian hồi chiêu (5 giây)
    public TextMeshProUGUI ammoDisplay;
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    [SerializeField]
    private int ammo = 9;

    public Transform camera;
    [SerializeField] private bool isReload = false;
   
    IEnumerator ReloadCoroutine()
    {
        ammoDisplay.text = "Reloading";
        isReload = true;
        
        yield return new WaitForSeconds(4f);
        ammo = 9;
        isReload = false;
        ammoDisplay.text = "" + ammo + " / 9";
    }

    void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }
    
    void Start()
    {
        ammoDisplay.text = "" + ammo + " / 9";
    }

    // Update is called once per frame
    void Update()
    {
        if  (Input.GetMouseButtonDown(0))
        {
            if (particleSystem != null)
            {
                if (ammo > 0 && isReload == false && Time.time >= nextFireTime)
                {
                    
                    Shoot();
                    // Cập nhật thời điểm tiếp theo được bắn = thời gian hiện tại + 5 giây
                    nextFireTime = Time.time + cooldownTime;
                    particleSystem.Play();
                    ammo -= 1;
                    ammoDisplay.text = "" + ammo + " / 9";
                }
            }
            
        }
        if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }

        if (ammo == 0)
        {
            Reload();
        }
    }
    void Shoot()
    {
        // 1. Xác định vị trí và góc quay để tạo đạn
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        Quaternion spawnRotation = firePoint != null ? Bullet.transform.rotation : transform.rotation;

        // 2. Tạo bản sao của viên đạn (Spawn)
        GameObject bulletClone = Instantiate(Bullet, spawnPosition, spawnRotation);

        // 3. Tìm thành phần Rigidbody (hoặc Rigidbody2D nếu là game 2D) để đẩy đạn bay về phía trước
        Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity =  camera.transform.forward * bulletSpeed; 
            // Lưu ý: Nếu dùng Unity phiên bản cũ hơn 2023, thay 'linearVelocity' bằng 'velocity' nhé!
        }

        bulletClone.GetComponent<BulletScript>().enabled = true;

    }
}
