using TMPro;
using UnityEngine;

public class HighscoreUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] letterBoxes;

    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    [SerializeField] private int selectedLetter = 0;

    [SerializeField] private int selectedBox = 0;

    [SerializeField] private float switchDelay = 0.1f;

    [SerializeField] private GameObject arrow;

    [SerializeField] private Highscores highscores;

    private float time;

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.PlayersWon)
            return;
        
        if ((time > switchDelay))
        {
            var inputVertical = Input.GetAxis("Vertical1");
            if (inputVertical != 0)
            {
                selectedLetter += (int)inputVertical;
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

            var inputHorizontal = Input.GetAxis("Horizontal1");
            if (inputHorizontal != 0)
            {
                selectedBox += (int)inputHorizontal;
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
        UpdateUi(selectedBox);
        Submit();
    }

    void UpdateUi(int index)
    {
        letterBoxes[index].text = alphabet[selectedLetter].ToString();
        arrow.transform.position = letterBoxes[index].transform.position;
    }

    void Submit()
    {
        var inputHorizontal = Input.GetButton("YButton");
        if (inputHorizontal)
        {
            StartCoroutine(highscores.UploadNewHighscore(letterBoxes[0].text+ letterBoxes[1].text + letterBoxes[2].text, (int)GameController.Instance._timer));
        }
    }

}
