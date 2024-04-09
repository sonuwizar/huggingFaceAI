using System.Collections.Generic;
using UnityEngine;

namespace Customisation.Ver2
{
    public class ReadWriteDataToJson_V2 : MonoBehaviour
    {
        public static ReadWriteDataToJson_V2 instance;

        AvtarCustomisedDataCollector customisedDataCollector;
        string jsonData;

        ProfileCollector profileCollector;
        string profileJson;

        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            customisedDataCollector = new AvtarCustomisedDataCollector();
            customisedDataCollector.modelColor = new List<string>();
            customisedDataCollector.modelPartIndexList = new List<int>();
            GetDataFromJson();
            InitProfile();
        }

        void GetDataFromJson()
        {
            if (PlayerPrefs.HasKey("json_v2"))
            {
                jsonData = PlayerPrefs.GetString("json_v2");

                customisedDataCollector = JsonUtility.FromJson<AvtarCustomisedDataCollector>(jsonData);
                WriteDataToJson();
            }
            else
            {
                InitialiseDataInJson();
            }
        }

        void InitialiseDataInJson()
        {
        }

        void WriteDataToJson()
        {
            jsonData = JsonUtility.ToJson(customisedDataCollector);
            PlayerPrefs.SetString("json_v2", jsonData);
        }

        #region color
        public void InitialiseColorList(int listLength)
        {
            if (customisedDataCollector.modelColor.Count == 0)
            {
                for (int i = 0; i < listLength; i++)
                {
                    customisedDataCollector.modelColor.Add(Color.black.ToString());
                }
                WriteDataToJson();
            }
        }

        public void StoreColor(Color color, int index)
        {
            customisedDataCollector.modelColor[index] = color.ToString();
        }

        public Color GetColor(int index)
        {
            string currStr = customisedDataCollector.modelColor[index];
            currStr = currStr.Replace("RGBA(", "");
            currStr = currStr.Replace(")", "");
            string[] rgbaStr = currStr.Split(","[0]);
            Color currColor = Color.black;
            for (int j = 0; j < rgbaStr.Length; j++)
            {
                currColor[j] = System.Single.Parse(rgbaStr[j]);
            }
            return currColor;
        }

        #endregion

        #region Dress

        public void InitializePartsIndex(int length)
        {
            if (customisedDataCollector.modelPartIndexList.Count == 0)
            {
                for (int i = 0; i < length; i++)
                {
                    customisedDataCollector.modelPartIndexList.Add(0);
                }
                WriteDataToJson();
            }
        }

        public void StorePartsOption(int partIndex, int partOptionIndex)
        {
            customisedDataCollector.modelPartIndexList[partIndex] = partOptionIndex;
        }

        public int GetPartOption(int partIndex)
        {
            return customisedDataCollector.modelPartIndexList[partIndex];
        }

        public void SetAvtar(int index)
        {
            customisedDataCollector.modelIndex = index;
        }

        public int GetAvtar()
        {
            return customisedDataCollector.modelIndex;
        }

        #endregion

        #region Profile

        public ProfileCollector GetProfile()
        {
            return profileCollector;
        }

        public void InitProfile()
        {
            if (PlayerPrefs.HasKey("profileData"))
            {
                profileJson = PlayerPrefs.GetString("profileData");
                profileCollector = JsonUtility.FromJson<ProfileCollector>(profileJson);
            }
            else
            {
                profileCollector = new ProfileCollector()
                {
                    name = "",
                    gradeIndex = 0,
                    ageIndex = 0,
                    email = "",
                    password = ""
                };

                UpdateProfileToDB();
            }
        }

        public void SetProfile(string name, int gradeIndex, int ageIndex, string email, string password)
        {
            if (name != null) profileCollector.name = name;
            if (gradeIndex != null) profileCollector.gradeIndex = gradeIndex;
            if (ageIndex != null) profileCollector.ageIndex = ageIndex;
            if (email != null) profileCollector.email = email;
            if (password != null) profileCollector.password = password;

            UpdateProfileToDB();
        }

        void UpdateProfileToDB()
        {
            profileJson = JsonUtility.ToJson(profileCollector);
            PlayerPrefs.SetString("profileData", profileJson);
        }

        #endregion

        public void StoreCustomisedDataToDb()
        {
            WriteDataToJson();
        }
    }

    public class AvtarCustomisedDataCollector
    {
        public int modelIndex;
        public List<string> modelColor;     //for Color
        public List<int> modelPartIndexList;    //for Dress
    }

    public class ProfileCollector
    {
        public string name;
        public int gradeIndex, ageIndex;
        public string email;
        public string password;
    }

}

