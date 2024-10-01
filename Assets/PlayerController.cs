using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _bullet; // <--- Private fields should starts with a _
    [SerializeField] private Transform _rifleStart;
    [SerializeField] private Text _hpText;

    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _victory;

    [SerializeField] private int _health; // <--- Change this float to int.. This should be a private variable.

    void Start()
    {
        // Destroy(this); // <--- Remove this line
        ChangeHealth(0); // <--- This method is redundant cause should be a incemental and decremental mehotd before and then a method to set the health value.
    }

    public void ChangeHealth(int hp)
    {
        _health += hp;
        if (_health > 100)
        {
            _health = 100;
        }
        else if (_health <= 0)
        {
            Lost();
        }
        _hpText.text = _health.ToString();
    }

    public void Win()
    {
        _victory.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    public void Lost()
    {
        _gameOver.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject buf = Instantiate(_bullet);
            buf.transform.position = _rifleStart.position;
            buf.GetComponent<Bullet>().setDirection(transform.forward);
            buf.transform.rotation = transform.rotation;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Collider[] tar = Physics.OverlapSphere(transform.position, 2);
            foreach (var item in tar)
            {
                if (item.tag == "Enemy")
                {
                    Destroy(item.gameObject);
                }
            }
        }

        Collider[] targets = Physics.OverlapSphere(transform.position, 3);
        foreach (var item in targets)
        {
            if (item.tag == "Heal")
            {
                ChangeHealth(50);
                Destroy(item.gameObject);
            }
            if (item.tag == "Finish")
            {
                Win();
            }
            if (item.tag == "Enemy")
            {
                Lost();
            }
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
