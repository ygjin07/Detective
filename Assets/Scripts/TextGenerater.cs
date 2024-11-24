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

    [System.Serializable] // JsonUtility가 직렬화할 수 있도록 표시
    public class Query
    {
        public string input_text;
    }

    [System.Serializable] // JsonUtility가 직렬화할 수 있도록 표시
    public class Response
    {
        public string generated_text;
    }

    // Colab에서 생성된 ngrok URL을 여기에 입력하세요
    private string apiUrl = "https://ab3b-34-44-217-240.ngrok-free.app/gene";

    /// <summary>
    /// 텍스트 생성 요청을 Python 서버로 전송합니다.
    /// </summary>
    /// <param name="inputText">API에 전달할 입력 텍스트</param>
    public IEnumerator GenerateText(string character, string prompt, string before_emotion, string condition, string evidence)
    {
        // input_text를 하나의 묶음으로 전송 (학습 시 사용한 형식대로)
        string inputText = $"캐릭터-{character}\n질문-{prompt}\n감정-{before_emotion}\n조건-{condition}\n증거-{evidence}";
        //string inputText = "이 털에 대해 짐작가는 것은 없나요?";
        
        Query queryData = new Query();
        queryData.input_text = inputText;

        // 객체를 JSON으로 직렬화
        string jsonData = JsonUtility.ToJson(queryData);

        Debug.Log(jsonData);
        // HTTP 요청 생성
        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // 요청 전송
            yield return www.SendWebRequest();

            // 응답 처리
            if (www.result == UnityWebRequest.Result.Success)
            {
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                // 응답 메시지 출력
                Debug.Log("Response: " + response.generated_text);
            }
            else
            {
                // 오류 출력
                Debug.Log("Error: " + www.error);
                Debug.Log("Response Code: " + www.responseCode);
            }

            www.uploadHandler?.Dispose();
            www.downloadHandler?.Dispose();
        }
    }

    /// <summary>
    /// Unity Inspector에서 이 함수를 호출해 테스트할 수 있습니다.
    /// </summary>
    [ContextMenu("Test Generate Text")]
    public void TestGenerate()
    {
        // 코루틴 실행
        StartCoroutine(GenerateText("꼬마 용의자", "이 털에 대해 짐작가는 것은 없나요?", "진정", "", "무언가의 털"));
    }
}
