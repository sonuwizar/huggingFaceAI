using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LearnVerseTrail
{
    public string Id;
    public MasterVirtualWorld SelectedMultiVerse;
    public List<Lab> Labcontent;
}

[Serializable]
public class MasterVirtualWorld
{
    public string EnvName;
    public string EnvAssetName;
    public string EnvDescription;
    public string EnvVersion;
    public string EnvImgUrl;
}

[Serializable]
public class Lab
{
    public string EnvName;
    public string EnvAssetName;
    public string EnvImgUrl;
    public string EnvVersion;
    public string EnvDescription;
    public string Subject;
    public string Chapter;
    public string Topic;
    public Dictionary<string, string> Pdfs;
    public Dictionary<string, string> Videos;
    public Dictionary<string, string> ArActivities;
    public Dictionary<string, string> WebActivityLink;
    public Dictionary<string, List<WebDesc>> ActivityContent;
    public Dictionary<string, string> WebGameLink;
    public List<Quest> Quests;
}

public class WebDesc
{
    public string Desc;
    public string Url;
}

[Serializable]
public class Quest
{
    public string Id;
    public string Subject;
    public string Chapter;
    public string Topic;
    public Enviorment Enviorment;
    public Dictionary<int, List<InGameLearningMods>> Levels;
}

[Serializable]
public class Enviorment
{
    public string name;
    public string assetName;
    public string Description;
    public string image;
    public int maxLevel;
    public int maxquestionperLevel;
    public bool IsSelected;
}

[Serializable]
public class InGameLearningMods
{
    public string Id;
    public List<string> Tags;
    public string Name;
    public string? Description;
    public List<LearningResource> Resources;
    public List<LearningItemQuestions> Questions;
    public bool IsSelected;
    public string Grade;
    public string Subject;
    public string Unit;
    public string Topic;
    public string Chapter;
    public string LearningConcept;
    public string Kind;
    public string DescriptionType;
    public string Difficulty;
    public bool IsLearn;
    public bool IsPractice;
    public bool IsChallenge;
    public bool IsItemAssigned;
    public bool IsReviewer;
}

public class LearningResource
{
    public int Id;
    public string CreatedOn;
    public string LastModified;
    public bool Deleted;
    public bool Enabled;
    public bool Activated;
    public int AuthorId;
    public string Title;
    public string Kind;
    public string FilePath;
    public string Language;
    public int ParentId;
    public int Series;
    public bool IsCopy;
}

[Serializable]
public class LearningItemQuestions
{
    public string Id;
    public DateTime CreatedOn;
    public DateTime LastModified;
    public bool Deleted;
    public bool Enabled;
    public bool Activated;
    public int AuthorId;
    public string Difficulty;
    public string Format;
    public string Hint;
    public string HintType;
    public string Text;
    public string TextType;
    public string CorrectAnswer;
    public int ParentId;
    public int Series;
    public string NodeType;
    public bool IsCopy;
    public string Node1;
    public string Node2;
    public string Node3;
    public string Node4;
    public string Node5;
    public string Node6;
    public string Node7;
    public string Node8;
    public int Tag1;
    public int Tag2;
    public int Tag3;
    public int Tag4;
    public bool isAiAssigned;
    public int AiLookupId;
    public bool IsRed;

    #region Get Node Value
    public string GetNodeValue(string nodeName)
    {
        switch (nodeName)
        {
            case "Node1": return Node1;
            case "Node2": return Node2;
            case "Node3": return Node3;
            case "Node4": return Node4;
            case "Node5": return Node5;
            case "Node6": return Node6;
            case "Node7": return Node7;
            case "Node8": return Node8;
            default: return null;
        }
    }
    #endregion

    #region Question Type
    public enum QuestionType { SingleChoiceQuestion, MultipleChoiceQuestion, BlankWithOptions, MatchTheFollowing, PutInTheBucket, FillInTheBlanks, FillInTheBlanks_Multiple, Sequencing };

    QuestionType questionType;

    public QuestionType GetQuestionType()
    {
        switch (Format)
        {
            case "m": questionType = QuestionType.SingleChoiceQuestion; break;
            case "q": questionType = QuestionType.MultipleChoiceQuestion; break;
            case "o": questionType = QuestionType.BlankWithOptions; break;
            case "x": questionType = QuestionType.MatchTheFollowing; break;
            case "p": questionType = QuestionType.PutInTheBucket; break;
            case "b": questionType = QuestionType.FillInTheBlanks; break;
            case "t": questionType = QuestionType.FillInTheBlanks_Multiple; break;
            case "d": questionType = QuestionType.Sequencing; break;
            default: Debug.Log("Question Type Not Available"); break;
        }
        return questionType;
    }

    #endregion
}

#region Learnverse Trail



#endregion
