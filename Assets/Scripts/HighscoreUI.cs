using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] letterBoxes;

    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    [SerializeField] private int selectedLetter = 0;

    [SerializeField] private int selectedBox = 0;

    [SerializeField] private float switchDelay = 0.1f;

    [SerializeField] private GameObject arrow;
    private float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((time > switchDelay))
        {
            var inputHorizontal = Input.GetAxis("Vertical1");
            if (inputHorizontal != 0)
            {
                selectedLetter += (int)inputHorizontal;
                if (selectedLetter >= alphabet.Length)
                {
                    selectedLetter = 0;
                }
                else if (selectedLetter < 0)
                {
                    selectedLetter = alphabet.Length - 1;
                }
                time = 0;
                
            }

            var inputVertical = Input.GetAxis("Horizontal1");
            if (inputVertical != 0)
            {
                selectedBox += (int)inputVertical;
                if (selectedBox >= letterBoxes.Length)
                {
                    selectedBox = 0;
                }
                else if (selectedBox < 0)
                {
                    selectedBox = letterBoxes.Length - 1;
                }
                time = 0;
            }
        }
        time += Time.deltaTime;
        updateUI(selectedBox);
    }

    void updateUI(int index)
    {
        letterBoxes[index].text = alphabet[selectedLetter].ToString();
        arrow.transform.position = letterBoxes[index].transform.position;
    }


}
