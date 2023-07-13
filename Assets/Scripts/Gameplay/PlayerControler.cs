using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    public int finishScore;
    public int score;
    public int level;

    public float scale;
    public float radius;
    public bool sizeUp;

    public Transform visuals;

    public CapsuleCollider detector;
    public BoxCollider[] colliders;
    public List<Rigidbody> victims;

    public Animator cutscene_anim;

    public Rigidbody rb;

    public Slider slider;
    public MeshRenderer water;

    private float speed = 0.35f;
    private float speedHalved;
    public float speedOrigin = 0.35f;

    public AudioSource sfx;
    public Animator bunnyAnim;

    public ParticleSystem vfx;

    private void Awake()
    {
        GameManager.gm.pc = this;
    }

    private void Start()
    {
        speedHalved = speedOrigin * 0.75f;
        RefreshScale();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (GameManager.gm.gameOver)
        {
            return;
        }

        if (finishScore <= score)
        {
            water.enabled = false;
            cutscene_anim.Play("cutscene_fadeout");
            RefreshScale();
            if (SceneManager.GetActiveScene().name == "Gameplay0_Akas")
                StartCoroutine(GameManager.gm.SceneTransition(2));
            else
                StartCoroutine(GameManager.gm.SceneTransition(1));
        }

        var nearbyObjects = Physics.OverlapSphere(transform.position, radius);
        foreach (var nearbyObject in nearbyObjects)
        {
            if (nearbyObject.gameObject == gameObject || nearbyObject.gameObject.layer != 9)
            {
                continue;
            }

            Rigidbody nearbyObjectRb = nearbyObject.GetComponentInParent<Rigidbody>();
            if (!nearbyObjectRb || victims.Contains(nearbyObjectRb))
            {
                continue;
            }
            else
            {
                victims.Add(nearbyObjectRb);
                nearbyObject.gameObject.layer = 10;
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.gm.gameOver)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal"); // set a float to control horizontal input
        float vertical = Input.GetAxis("Vertical"); // set a float to control vertical input
        PlayerMove(horizontal, vertical); // Call the move player function sending horizontal and vertical movements

        if (transform.position.z < 16)
            transform.position = new Vector3(transform.position.x, transform.position.y, 16.15f);
        else if (transform.position.z > 35)
            transform.position = new Vector3(transform.position.x, transform.position.y, 34.85f);
        else if (transform.position.x > 31)
            transform.position = new Vector3(30.85f, transform.position.y, transform.position.z);
        else if (transform.position.x < 19)
            transform.position = new Vector3(19.15f, transform.position.y, transform.position.z);
    }

    private void PlayerMove(float h, float v)
    {
        if (h != 0f || v != 0f) // If horizontal or vertical are pressed then continue
        {
            if (h != 0f && v != 0f) // If horizontal AND vertical are pressed then continue
            {
                speed = speedHalved; // Modify the speed to adjust for moving on an angle
            }
            else // If only horizontal OR vertical are pressed individually then continue
            {
                speed = speedOrigin; // Keep speed to it's original value
            }

            Vector3 targetDirection = new Vector3(h, 0f, v); // Set a direction using Vector3 based on horizontal and vertical input
            rb.MovePosition(rb.position + targetDirection * speed * Time.deltaTime); // Move the players position based on current location while adding the new targetDirection times speed
            RotatePlayer(targetDirection); // Call the rotate player function sending the targetDirection variable

            vfx.Play();
        }
    }

    private void RotatePlayer(Vector3 dir)
    {
        rb.MoveRotation(Quaternion.LookRotation(dir)); // Rotate the player to look at the new targetDirection
    }

    public void AddScore(int amount)
    {
        sfx.Play();
        bunnyAnim.SetTrigger("Eat");

        score += amount;
        RefreshScale();
        if (score % 11 == 0)
        {
            level++;
        }

        slider.value = score;
    }

    public void RefreshScale()
    {
        scale++;

        float newScale = scale / 21f;
        visuals.localScale = new Vector3(newScale, newScale, newScale);
        detector.radius = newScale;

        radius = newScale;

        foreach (var coll in colliders)
        {
            var direction = coll.center.normalized * 50.025f;
            coll.center = direction * (1 + newScale / 100f);
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public void SpeedBuff(float speed, float duration)
    {
        StartCoroutine(OnBuff(speed, duration));
    }

    IEnumerator OnBuff(float s, float t)
    {
        float s1 = speedOrigin;
        float s2 = speedHalved;

        speedOrigin *= s;
        speedHalved *= s;

        yield return new WaitForSeconds(t);

        speedOrigin = s1;
        speedHalved = s2;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(
            transform.position - new Vector3(radius, 0, 0),
            transform.position + new Vector3(radius, 0, 0),
            Color.black
        );
        Debug.DrawLine(
            transform.position - new Vector3(0, 0, radius),
            transform.position + new Vector3(0, 0, radius),
            Color.black
        );
    }
}
