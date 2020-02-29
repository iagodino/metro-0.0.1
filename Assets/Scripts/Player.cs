using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public Transform groundCheck;
    public float jumpForce;
    public float fireRate;
    public ConsumableItem item;
    public int maxHeath;
    public int maxMana;

    private float speed;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool onGround;
    private bool jump = false;
    private bool doubleJump;
    public Weapon weaponEquipped;
    private Animator anim;
    private Attack attack;
    private float nextAttack;
    public int health;
    public int mana;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        anim = GetComponent<Animator>();
        attack = GetComponentInChildren<Attack>();
    }

    // Update is called once per frame
    void Update()
    {

        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (onGround)
            doubleJump = false;

        if(Input.GetButtonDown("Jump") && (onGround || !doubleJump))
        {
            jump = true;
            if (!doubleJump && !onGround)
                doubleJump = true;
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextAttack && weaponEquipped != null)
        {
            anim.SetTrigger("Attack");
            attack.PlayAnimation(weaponEquipped.animation);
            nextAttack = Time.time + fireRate;

        }

        if (Input.GetButtonDown("Fire3"))
        {
            UseItem(item);
            Inventory.inventory.RemoveItem(item);
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        if(h > 0 && !facingRight)
        {
            Flip();
        }
        else if(h < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void AddWeapon(Weapon weapon)
    {
        weaponEquipped = weapon;
        attack.SetWeapon(weaponEquipped.damage);
    }

    public void UseItem(ConsumableItem item)
    {
        health += item.healthGain;
        if(health > maxHeath)
        {
            health = maxHeath;
        }
        mana += item.manaGain;
        if(mana > maxMana)
        {
            mana = maxMana;
        }
    }
}
