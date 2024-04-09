using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DescriptionWriter
{
    public class TextWriter : MonoBehaviour
    {
        public static TextWriter instance;

        public float timePerChar = 1f;

        [Header("UI Panles")]
        public GameObject descPanel;
        public GameObject descSkipBtn, descCloseBtn;
        public Text descText;
        public Image descCharacterImage;

        [Header("Sprites")]
        public Sprite playerSprite;
        public Sprite npcSprite;

        [Header("Audio Clip")]
        public AudioClip typingAudioClip;

        internal bool isWritting, descWriting;

        public delegate void OnClickClose();
        public static event OnClickClose onClickClose;

        Coroutine writingRoutine, multiLineDescRoutine;
        AudioSource audioSource;
        Text currText;
        string currMsg;
        int charIndex;
        float timer;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            if (descCloseBtn != null) 
            {
                descCloseBtn.SetActive(false);
                descCloseBtn.GetComponent<Button>().onClick.AddListener(() => ClickDescCloseBtn());
            }
            descSkipBtn.GetComponent<Button>().onClick.AddListener(() => SkipDesc());
        }

        internal void SetWriter(Text uiText, string msg)
        {
            writingRoutine = StartCoroutine(AddWriter(uiText, msg));
        }

        IEnumerator AddWriter(Text uiText, string msg)
        {
            while (isWritting)
            {
                yield return null;
            }

            currText = uiText;
            currMsg = msg;
            charIndex = 0;
            isWritting = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (currText != null)
            {
                timer -= Time.deltaTime;
                while (timer <= 0)
                {
                    timer += timePerChar;
                    charIndex++;
                    currText.text = currMsg.Substring(0, charIndex);
                    //audioSource.PlayOneShot(typingAudioClip);
                    if (charIndex >= currMsg.Length)
                    {
                        currText = null;
                        isWritting = false;
                        return;
                    }
                    else if (charIndex + 4 >= currMsg.Length)
                    {
                        descSkipBtn.SetActive(false);
                    }
                }
            }
        }

        internal void Skip_Writer()
        {
            if (currText != null)
            {
                charIndex = currMsg.Length - 2;
                currText.text = currMsg.Substring(0, charIndex);
            }
        }

        #region Multi Line desc
        internal void SetMultiLineDesc(string[] msgs)
        {
            descWriting = true;
            descCloseBtn.SetActive(false);
            descPanel.SetActive(true);
            if (multiLineDescRoutine != null)
            {
                StopCoroutine(multiLineDescRoutine);
            }
            multiLineDescRoutine = StartCoroutine(MultiLineDesc_Coroutine(msgs));
        }

        IEnumerator MultiLineDesc_Coroutine(string[] msgs)
        {
            for (int i = 0; i < msgs.Length; i++)
            {
                descSkipBtn.SetActive(true);
                SetWriter(descText, msgs[i]);

                yield return new WaitForEndOfFrame();
                while (isWritting)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(1.5f);
            }

            //yield return new WaitForSeconds(1);
            descWriting = false;
            descCloseBtn.SetActive(true);
        }

        public void SkipDesc()
        {
            descSkipBtn.SetActive(false);
            Skip_Writer();
        }

        public void SkipMultiLineDesc()
        {
            if (isWritting)
            {
                Skip_Writer();
            }
            isWritting = false;
            descWriting = false;
            ClickDescCloseBtn();
            if (multiLineDescRoutine != null)
            {
                StopCoroutine(multiLineDescRoutine);
            }
        }

        public void ClickDescCloseBtn()
        {
            descCloseBtn.SetActive(false);
            descPanel.SetActive(false);
            descSkipBtn.SetActive(false);

            if(onClickClose!=null)
            {
                onClickClose();
            }

        }
        #endregion

        #region Multi Line desc with icon
        internal void SetMultiLineDesc_WithIcon(InstructionsData[] instData)
        {
            descWriting = true;
            descCloseBtn.SetActive(false);
            descPanel.SetActive(true);
            if (multiLineDescRoutine != null)
            {
                StopCoroutine(multiLineDescRoutine);
            }
            multiLineDescRoutine = StartCoroutine(MultiLineDescWithIcon_Coroutine(instData));
        }

        IEnumerator MultiLineDescWithIcon_Coroutine(InstructionsData[] instData)
        {
            for (int i = 0; i < instData.Length; i++)
            {
                for (int j = 0; j < instData[i].multiLineMsg.Length; j++)
                {
                    descSkipBtn.SetActive(true);
                    SetWriter(descText, instData[i].multiLineMsg[j]);

                    //set character icon
                    if (instData[i].isNpc)
                    {
                        descCharacterImage.sprite = npcSprite;
                    }
                    else
                    {
                        descCharacterImage.sprite = playerSprite;
                    }
                    
                    yield return new WaitForEndOfFrame();
                    while (isWritting)
                    {
                        yield return null;
                    }
                    yield return new WaitForSeconds(1.5f);
                }
            }
            
            //yield return new WaitForSeconds(1);
            descWriting = false;
            descCloseBtn.SetActive(true);
        }
        #endregion
    }

    [System.Serializable]
    public class InstructionsData
    {
        public bool isNpc;
        public string[] multiLineMsg;
    }
}
