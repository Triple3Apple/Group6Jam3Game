using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private Item.ItemType keyRequired = Item.ItemType.GoldKey;
    [SerializeField] private GameObject keyPrompt = null;

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.CompareTag("Player"))
        {
            return;
        }

        //Debug.Log("Triggered");
        if (Player.SearchForItem(new Item { itemType = keyRequired }))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(3);
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
