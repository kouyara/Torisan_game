using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum FruitsType{
    apple = 1,
    kiwi,
    melon,
    orange,
    pear,
    suica,
    suica2,
    torisan
}
public class Fruits : MonoBehaviour
{
    public FruitsType fruitsType;
    private static int fruits_serial = 0;
    private int my_serial;
    bool isDestroyed = false;
    public static UnityEvent OnGameOver = new UnityEvent();
    private bool isInside = false;

    [SerializeField] private Fruits newFruitsPrefab;
    [SerializeField] private int score;

    public static UnityEvent<int> OnScoreAdded = new UnityEvent<int>();

    private void Awake()
    {
        //生成されたフルーツにシリアル番号を通して、生成された順番で判定する。
        my_serial = fruits_serial;
        fruits_serial++;
        OnGameOver.AddListener(() =>
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
        });
    }
    IEnumerator Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (rb.isKinematic)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        if (!isInside)
        {
            OnGameOver.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        isInside = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (isDestroyed)
        {
            return;
        }
        OnGameOver.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDestroyed)
        {
            return;
        }
        if(other.gameObject.TryGetComponent(out Fruits otherFruits))
        {
            if(otherFruits.fruitsType == this.fruitsType)
            {
                if(my_serial < otherFruits.my_serial)
                {
                    OnScoreAdded.Invoke(score);

                    isDestroyed = true;
                    otherFruits.isDestroyed = true;
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);

                    if (newFruitsPrefab == null)
                    {
                        return;
                    }

                    //自分とフルーツの座標と回転の平均を取る。
                    Vector3 center = (transform.position + other.transform.position) / 2;
                    Quaternion rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, 0.5f);
                    Fruits newFruits = Instantiate(newFruitsPrefab, center, rotation);

                    //自分とフルーツの速度と角速度の平均を取る。
                    Rigidbody2D newRigidbody = newFruits.GetComponent<Rigidbody2D>();
                    Vector3 newVelocity = (this.GetComponent<Rigidbody2D>().velocity + otherFruits.GetComponent<Rigidbody2D>().velocity) / 2;
                    newRigidbody.velocity = newVelocity;

                    float newAngularVelocity = (this.gameObject.GetComponent<Rigidbody2D>().angularVelocity + otherFruits.GetComponent<Rigidbody2D>().angularVelocity) / 2;
                    newRigidbody.angularVelocity = newAngularVelocity;
                }
            }
        }
    }
}
