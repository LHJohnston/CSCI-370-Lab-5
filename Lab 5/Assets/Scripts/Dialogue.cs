using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialougePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialouge;
    private int index;
    public float wordSpeed;
    public bool monsterClose;

    void Update()
    {
        if(monsterClose){
            
                dialougePanel.SetActive(true);
                StartCoroutine(Typing());
        }
        else{
            zeroText();
        }
        
    } 
    
    public void zeroText(){
        dialogueText.text = "";
        index = 0;
        dialougePanel.SetActive(false);

    }

    public void NextLine(){
         if(index < dialouge.Length-1){
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
         }
         else{
            zeroText();
         }
    }

    IEnumerator Typing(){
        foreach(char letter in dialouge[index].ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            monsterClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            monsterClose = false;
            zeroText();
        }
    }
}
