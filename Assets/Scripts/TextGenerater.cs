using System.Collections;
using System.Net;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Query
{
    public string input_text;
}

public class TextGenerater : MonoBehaviour
{
    private Talkcontroller tc;

    public TextGenerater(Talkcontroller tc)
    {
        this.tc = tc;   
    }

    [System.Serializable] // JsonUtility�� ����ȭ�� �� �ֵ��� ǥ��
    public class Query
    {
        public string input_text;
    }

    [System.Serializable] // JsonUtility�� ����ȭ�� �� �ֵ��� ǥ��
    public class Response
    {
        public string generated_text;
    }

    // Colab���� ������ ngrok URL�� ���⿡ �Է��ϼ���
    private string apiUrl = "https://ab3b-34-44-217-240.ngrok-free.app/gene";

    /// <summary>
    /// �ؽ�Ʈ ���� ��û�� Python ������ �����մϴ�.
    /// </summary>
    /// <param name="inputText">API�� ������ �Է� �ؽ�Ʈ</param>
    public IEnumerator GenerateText(string character, string prompt, string before_emotion, string condition, string evidence)
    {
        // input_text�� �ϳ��� �������� ���� (�н� �� ����� ���Ĵ��)
        string inputText = $"ĳ����-{character}\n����-{prompt}\n����-{before_emotion}\n����-{condition}\n����-{evidence}";
        //string inputText = "�� �п� ���� ���۰��� ���� ������?";
        
        Query queryData = new Query();
        queryData.input_text = inputText;

        // ��ü�� JSON���� ����ȭ
        string jsonData = JsonUtility.ToJson(queryData);

        Debug.Log(jsonData);
        // HTTP ��û ����
        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // ��û ����
            yield return www.SendWebRequest();

            // ���� ó��
            if (www.result == UnityWebRequest.Result.Success)
            {
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                // ���� �޽��� ���
                Debug.Log("Response: " + response.generated_text);
            }
            else
            {
                // ���� ���
                Debug.Log("Error: " + www.error);
                Debug.Log("Response Code: " + www.responseCode);
            }

            www.uploadHandler?.Dispose();
            www.downloadHandler?.Dispose();
        }
    }

    /// <summary>
    /// Unity Inspector���� �� �Լ��� ȣ���� �׽�Ʈ�� �� �ֽ��ϴ�.
    /// </summary>
    [ContextMenu("Test Generate Text")]
    public void TestGenerate()
    {
        // �ڷ�ƾ ����
        StartCoroutine(GenerateText("���� ������", "�� �п� ���� ���۰��� ���� ������?", "����", "", "������ ��"));
    }
}
