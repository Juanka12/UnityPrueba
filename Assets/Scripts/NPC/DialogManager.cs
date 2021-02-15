using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;

    public void StartDialog (DialogNPC dialogo){
        sentences = new Queue<string>();
        sentences.Clear();
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogo.NPCname;
        foreach (string sentence in dialogo.dialogo)
        {
            sentences.Enqueue(sentence);
        }

        NextSentence();
    }

    private void NextSentence(){
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence){
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void EndDialog(){
        gameObject.SetActive(false);
    }
}
