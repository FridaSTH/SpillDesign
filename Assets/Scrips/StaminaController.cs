using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    private float timer;


    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 0.99f) {
            timer += Time.deltaTime;
            if (timer >= 0.1f) {
                Vector2 previousScale = transform.localScale;
                Vector2 newScale = new Vector2(previousScale.x + 0.01f, 1f);
                transform.localScale = newScale;
                timer = 0f;
            }
        }
    }

    // Scale down stamina per dash
    public void UseDash() {
        Vector2 previousScale = transform.localScale;
        Vector2 newScale = new Vector2(previousScale.x - 0.3f, 1f);
        transform.localScale = newScale;
    }


}
