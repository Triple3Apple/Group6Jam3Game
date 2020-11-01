using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private Item.ItemType keyRequired = Item.ItemType.GoldKey;
    [SerializeField] private GameObject keyPrompt;

    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("Triggered");
        if (Player.SearchForItem(new Item { itemType = keyRequired }))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            keyPrompt.SetActive(true);
            keyPrompt.GetComponent<Text>().text = keyRequired.ToString() + " is required to leave.";
            StartCoroutine("UITimeout");
        }
    }

    private IEnumerator UITimeout()
    {
        yield return new WaitForSeconds(1f);
        keyPrompt.SetActive(false);
    }
}
