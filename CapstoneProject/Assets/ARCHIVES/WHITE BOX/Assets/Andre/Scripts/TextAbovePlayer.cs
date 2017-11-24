using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAbovePlayer : MonoBehaviour {

    public GameObject player;
    public Font TheFont;

    private GameObject m_floatingText;
    private TextMesh m_textMesh;
    private Vector3 playersCurrentPos;
    //transform of the floating text
    private Transform m_transform;

    private Transform m_floatingText_Transform;
    private Transform m_cameraTransform;
    public Color startColour = Color.red;
    public Color endColour = Color.blue;
    public float speedChange = 5;


    Vector3 lastPOS = Vector3.zero;
    Quaternion lastRotation = Quaternion.identity;

    private void Awake()
    {
       
            m_transform = transform;
            m_floatingText = new GameObject(this.name + " floating text");

            // Reference to Transform is lost when TMP component is added since it replaces it by a RectTransform.
            //m_floatingText_Transform = m_floatingText.transform;
            //m_floatingText_Transform.position = m_transform.position + new Vector3(0, 15f, 0);

            m_cameraTransform = Camera.main.transform;
        
    }

    private void Start()
    {

        m_floatingText_Transform = m_floatingText.transform;
        m_floatingText_Transform.position = m_transform.position + new Vector3(0, 9f, 0);

        m_textMesh = m_floatingText.AddComponent<TextMesh>();
        m_textMesh.font = Resources.Load("Fonts/ARIAL", typeof(Font)) as Font;
        m_textMesh.GetComponent<Renderer>().sharedMaterial = m_textMesh.font.material;
        m_textMesh.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        m_textMesh.anchor = TextAnchor.LowerCenter;
        m_textMesh.fontSize = 8;

        StartCoroutine(DisplayFloatingText("+20"));
    }

    public IEnumerator DisplayFloatingText(string text = "Default")
    {
        m_textMesh.text = text;
        float textDuration = 2.0f; // How long the text is alive. 
        float elapseTime = 0f;
        float alpha = 255;
        float fadeDuration = textDuration;

        Vector3 start_pos = player.transform.position;

        while (elapseTime < textDuration)
        {
            elapseTime += Time.deltaTime * speedChange;
            alpha = Mathf.Clamp(alpha - (Time.deltaTime / fadeDuration) * 255, 0, 255);
            m_floatingText_Transform.position += new Vector3(0, .8f * Time.deltaTime, 0);
            m_textMesh.color = Color.Lerp(startColour, endColour, elapseTime);
            // Align floating text perpendicular to Camera.

            if (!lastPOS.Compare(m_cameraTransform.position, 1000) || !lastRotation.Compare(m_cameraTransform.rotation, 1000))
            {
                lastRotation = m_cameraTransform.rotation;
                m_floatingText_Transform.rotation = lastRotation;
                playersCurrentPos = player.transform.position;
                m_transform.forward = new Vector3(playersCurrentPos.x, 0, playersCurrentPos.z);
            }

            yield return null;
            //m_floatingText_Transform.position = start_pos;
        }

        
    }

    public IEnumerator DisplayTextMeshFloatingText()
    {
        float CountDuration = 2.0f; // How long is the countdown alive.    
        float starting_Count = 20f; // At what number is the counter starting at.
        float current_Count = starting_Count;
        

        Vector3 start_pos = player.transform.position;
        Color32 start_color = m_textMesh.color;
        float alpha = 255;
        int int_counter = 0;

        float fadeDuration = 3 / starting_Count * CountDuration;

        while (fadeDuration < 1)
        {
            current_Count -= (Time.deltaTime / CountDuration) * starting_Count;

            if (current_Count <= 3)
            {
                //Debug.Log("Fading Counter ... " + current_Count.ToString("f2"));
                alpha = Mathf.Clamp(alpha - (Time.deltaTime / fadeDuration) * 255, 0, 255);
            }

            int_counter = (int)current_Count;
            m_textMesh.text = int_counter.ToString();
            //Debug.Log("Current Count:" + current_Count.ToString("f2"));

            m_textMesh.color = new Color32(start_color.r, start_color.g, start_color.b, (byte)alpha);

            // Move the floating text upward each update
            m_floatingText_Transform.position += new Vector3(0, .8f * Time.deltaTime, 0);

            // Align floating text perpendicular to Camera.

            if (!lastPOS.Compare(m_cameraTransform.position, 1000) || !lastRotation.Compare(m_cameraTransform.rotation, 1000))
            {
                lastRotation = m_cameraTransform.rotation;
                m_floatingText_Transform.rotation = lastRotation;
                playersCurrentPos = player.transform.position;
                m_transform.forward = new Vector3(playersCurrentPos.x, 0, playersCurrentPos.z);
            }



            yield return new WaitForEndOfFrame();
        }

        //Debug.Log("Done Counting down.");

        yield return new WaitForSeconds(Random.Range(0.1f, 1.0f));

        m_floatingText_Transform.position = start_pos;

        StartCoroutine(DisplayTextMeshFloatingText());
    }
}
