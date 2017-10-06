using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(RaycastHit2D))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour {

    public Rigidbody2D rb;
    public BoxCollider2D bc;

    //public GameObject ammo; // selected ammo
    public GameObject weapon; // selected weapon
    public GameObject weaponSpawnPoint;
    public RectTransform healthBarBg;
    public RectTransform healthBar;

    SpriteRenderer spriteRenderer;
    Animator anim;

    public float speed;
    public float jumpForce;
    public float groundCheckRadius;
    public float ammoSpeed = 20f;

    public bool onGround;
    public bool isFacingLeft;
    public bool useWeapon;
    public bool damaged;
    public bool isDead;
    public bool isFalling;

    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public Image damageImage;
    public Slider healthSlider;

    Hud h;

    float _health;
    float moveValue;
    public float currentHealth;
    public float maxHealth = 100;
    public float percentHealth;
    public int switchGun;
    int _damage;
    string _name;

    public AudioClip jumpSFX;
    public AudioClip shootSFX;
    public AudioClip hurtSFX;

    public AudioSource shootSource;
    public AudioSource jumpSource;
    public AudioSource hurtSource;

    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    Vector3 shootDirection;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start() {
        if (speed <= 0)
            speed = 7.0f;
        if (jumpForce <= 0)
            jumpForce = 15.0f;
        if (groundCheckRadius == 0)
            groundCheckRadius = 0.2f;
        damageImage = h.flashScreen;
        health = maxHealth;
        if(useWeapon) {
            Instantiate(weapon, weaponSpawnPoint.transform.position, weaponSpawnPoint.transform.rotation);
        }
        healthBar = h.healthBar;
        healthBarBg = h.healthBarBg;
    }

    public void DetectFall() {
        if (rb.velocity.y < -0.1 && !onGround) {
            isFalling = true;
        } else {
            isFalling = false;
        }
    }

    public void Fall() {
        anim.SetBool("isFalling", true);
        playAnim("Fall");
        Debug.Log("Player is Falling");
    }

    public void Roll() {
        anim.SetBool("isRolling", true);
        playAnim("Roll");
        Debug.Log("Player is Rolling");
    }

    public void Jump() {
        jumpSource.clip = jumpSFX;
        jumpSource.Play();
        playAnim("Jump");
        Debug.Log("Player jumped");
    }

    public void Duck() {
        anim.SetTrigger("Duck");
        playAnim("Duck");
        Debug.Log("Player ducked");
    }

    public void Flip() {
        isFacingLeft = !isFacingLeft;
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x *= -1;
        transform.localScale = scaleFactor;
    }

    public void playAnim(string name) {
        anim.Play(name);
    }

    void kill() {
        anim.SetBool("isDead", true);
        playAnim("Dead");
        Debug.Log("Player is dead");
        //GameManager.instance.gameWon = false;
        //GameManager.instance.gameOver = false;
    }

    void OnDestroy() {
        kill();
    }

    public float health {
        get { return _health; }
        set { _health = value; }
    }

    public int damage {
        get { return _damage; }
        set { _damage = value; }
    }

    public string characterName {
        get { return _name; }
        set { _name = value; }
    }

    public void TakeDamage(int amount) {
        damaged = true;
        health = health - amount;
        float newHealth = calculateHealth();
        h.healthBar.sizeDelta = new Vector2(newHealth, h.healthBar.sizeDelta.y);
        hurtSource.Play();
        anim.SetTrigger("isHit");
        playAnim("Hit");
        Debug.Log("Player has taken damage! Health: " + health);
    }

    public float calculateHealth() {
        percentHealth = (maxHealth - health) * 100;
        float width = percentHealth / 100;
        width = h.healthBarBg.sizeDelta.x - width;
        Debug.Log("Health: " + width);
        return width;
    }

    //#if UNITY_EDITOR
    //    public override void Die() {
    //        GameManager instance = GameManager.instance as GameManager;
    //        instance.GameOver();
    //    }
    //#endif

}
