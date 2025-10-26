using UnityEngine;
using TMPro;
using System.Text;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;

    public ScoreEntry(string name, int score)
    {
        playerName = name;
        this.score = score;
    }
}

[System.Serializable]
public class ScoreboardData
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}

public class ScoreScript : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TMP_InputField nameInputField;

    private string saveFilePath;
    private int currentScore = 0; // Guardamos el score del jugador para no depender del ScoreHolder

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/scores.json";

        // Capturar la puntuación actual (una sola vez)
        ScoreHolder scoreHolder = FindAnyObjectByType<ScoreHolder>();
        if (scoreHolder != null)
        {
            currentScore = scoreHolder.points;
            Debug.Log($"✅ Puntuación capturada desde ScoreHolder: {currentScore}");
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró ScoreHolder. Se usará puntuación 0.");
        }

        // Mostrar tabla actual al inicio
        ShowScoreboard(LoadScores());
    }

    // Llamado desde el botón "Guardar puntuación"
    public void SaveCurrentScore()
    {
        string playerName = string.IsNullOrWhiteSpace(nameInputField.text) ? "Jugador" : nameInputField.text;

        ScoreboardData scoreboard = LoadScores();

        // Añadir el nuevo score
        scoreboard.scores.Add(new ScoreEntry(playerName, currentScore));

        // Ordenar por puntaje descendente
        scoreboard.scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Guardar en archivo (no recortamos, mantenemos todo)
        SaveScores(scoreboard);

        // Mostrar solo top 3
        ShowScoreboard(scoreboard);
    }

    private void ShowScoreboard(ScoreboardData scoreboard)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("TOP 3");

        int rank = 1;
        foreach (var entry in scoreboard.scores)
        {
            sb.AppendLine($"{rank}. {entry.playerName} - {entry.score}");
            rank++;

            if (rank > 3) // 🔹 Solo mostramos los 3 primeros
                break;
        }

        if (scoreboard.scores.Count == 0)
            sb.AppendLine("No hay puntuaciones todavía.");

        scoreText.text = sb.ToString();
    }

    private void SaveScores(ScoreboardData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    private ScoreboardData LoadScores()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<ScoreboardData>(json);
        }
        else
        {
            return new ScoreboardData();
        }
    }
}
