using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Organic_Wizard
{
    [Serializable]
    public class SavedData
    {
        private const int NB_SUPP_SKILLS = 8;
        private const int NB_ATTACK_SKILLS = 4;
        private const int NB_REC_SKILLS = 4;

        private static string SAVE_FILE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "savedData.dat");

        #region Attributes

        public static bool AttackMode
        {
            get
            {
                return Instance._attackMode;
            }

            set
            {
                Instance._attackMode = value;
            }
        }

        public static bool RecoveryMode
        {
            get
            {
                return Instance._recoveryMode;
            }

            set
            {
                Instance._recoveryMode = value;
            }
        }

        public static bool UseBuffSkills
        {
            get
            {
                return Instance._useSupportSkills;
            }

            set
            {
                Instance._useSupportSkills = value;
            }
        }

        public static bool HpRecovery
        {
            get
            {
                return Instance._hpRecovery;
            }

            set
            {
                Instance._hpRecovery = value;
            }
        }
        public static bool MpRecovery
        {
            get
            {
                return Instance._mpRecovery;
            }

            set
            {
                Instance._mpRecovery = value;
            }
        }
        public static int HpRecoverySkill
        {
            get
            {
                return Instance._hpRecoverySkill;
            }

            set
            {
                Instance._hpRecoverySkill = value;
            }
        }
        public static int MpRecoverySkill
        {
            get
            {
                return Instance._mpRecoverySkill;
            }

            set
            {
                Instance._mpRecoverySkill = value;
            }
        }

        public static int HpRecoveryPercent
        {
            get
            {
                return Instance._hpRecoveryPercent;
            }

            set
            {
                Instance._hpRecoveryPercent = value;
            }
        }
        public static int MpRecoveryPercent
        {
            get
            {
                return Instance._mpRecoveryPercent;
            }

            set
            {
                Instance._mpRecoveryPercent = value;
            }
        }

        public static int RecoverySkillPecent
        {
            get
            {
                return Instance._recoverySkillPercent;
            }

            set
            {
                Instance._recoverySkillPercent = value;
            }
        }
        public static int MaxTravelRange
        {
            get
            {
                return Instance._maxTravelRange;
            }

            set
            {
                Instance._maxTravelRange = value;
            }
        }

        public static bool Loot
        {
            get
            {
                return Instance._loot;
            }

            set
            {
                Instance._loot = value;
            }
        }

        public static bool DarkMode
        {
            get
            {
                return Instance._darkMode;
            }

            set
            {
                Instance._darkMode = value;
            }
        }


        #endregion

        static SavedData _instance = null;

        #region Private Members

        private int[] _attackSkills = null;
        private int[] _recoverySkills = null;
        private BuffSkillInfo[] _buffSkills = null;
        private bool _attackMode = false;
        private bool _recoveryMode = false;
        private bool _useSupportSkills = false;
        private bool _hpRecovery = false;
        private bool _mpRecovery = false;
        private int _hpRecoverySkill = Constants.NONE;
        private int _mpRecoverySkill = Constants.NONE;
        private int _hpRecoveryPercent = 0;
        private int _mpRecoveryPercent = 0;
        private int _recoverySkillPercent = 0;
        private int _maxTravelRange = 0;
        private bool _loot;
        private bool _darkMode;
        #endregion

        private static SavedData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SavedData();
                return _instance;
            }
        }

        public static void SetRecoverySkillAtPos(int pos, int skillValue)
        {
            if (pos >= 0 && pos < Instance._recoverySkills.Length)
            {
                Instance._recoverySkills[pos] = GetAvailableSkillValue(Instance._recoverySkills, skillValue, pos);
            }
        }

        public static void SetAttackSkillAtPos(int pos, int skillValue)
        {
            if (pos >= 0 && pos < Instance._attackSkills.Length)
            {
                Instance._attackSkills[pos] = GetAvailableSkillValue(Instance._attackSkills, skillValue, pos);
            }
        }

        public static void SetBuffSkillAtPos(int index, int skillNumber, bool useOnParty)
        {
            if (index >= 0 && index < Instance._buffSkills.Length)
            {
                int usedIndex = GetBuffSkillIndex(skillNumber);

                if (skillNumber == Constants.NONE || usedIndex == -1 || usedIndex == index)
                {
                    BuffSkillInfo buff = Instance._buffSkills[index] ?? new BuffSkillInfo();
                    buff.Skill = skillNumber;
                    buff.UseOnParty = useOnParty;
                    Instance._buffSkills[index] = buff;
                }
            }
        }

        private static int GetBuffSkillIndex(int skillNumber)
        {
            int index = -1;

            for (int i = 0; i < Instance._buffSkills.Length; i++)
            {
                if (Instance._buffSkills[i].Skill == skillNumber)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private static int GetAvailableSkillValue(int[] array, int nb, int pos)
        {
            if (nb < 0 || nb >= 10 || array == null)
            {
                return Constants.NONE;
            }

            int index = -1;
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == nb)
                {
                    index = i;
                    break;
                }
            }
            //if it doesn't exist or is the currernt pos
            return (index == -1 || index == pos) ? nb : Constants.NONE;
        }

        private SavedData()
        {
            _attackSkills = new int[NB_ATTACK_SKILLS];
            for (int i = 0; i < NB_ATTACK_SKILLS; i++)
            {
                _attackSkills[i] = Constants.NONE;
            }

            _recoverySkills = new int[NB_REC_SKILLS];
            for (int i = 0; i < NB_REC_SKILLS; i++)
            {
                _recoverySkills[i] = Constants.NONE;
            }

            _buffSkills = new BuffSkillInfo[NB_SUPP_SKILLS];
            for (int i = 0; i < NB_SUPP_SKILLS; i++)
            {
                _buffSkills[i] = new BuffSkillInfo();
            }

            _maxTravelRange = 30;
        }

        public static int GetAttackSkillsCount()
        {
            if (Instance._attackSkills.Length > 0)
                return _instance._attackSkills.Length;
            else
                return 0;
        }

        public static int GetAttackSkillAtPos(int index)
        {
            if (Instance._attackSkills.Length > index && index >= 0)
            {
                return Instance._attackSkills[index];
            }

            return 0;
        }

        public static int GetHealSkillsCount()
        {
            if (Instance._recoverySkills.Length > 0)
                return _instance._recoverySkills.Length;
            else
                return 0;
        }

        public static int GetHealSkillAtPos(int index)
        {
            if (Instance._recoverySkills.Length > index && index >= 0)
            {
                return Instance._recoverySkills[index];
            }

            return Constants.NONE;
        }

        public static List<BuffSkillInfo> GetBuffSkills()
        {
            List<BuffSkillInfo> skills = new List<BuffSkillInfo>();

            foreach (var skill in Instance._buffSkills)
            {
                skills.Add(skill);
            }

            return skills;
        }

        public static void Init()
        {
            if (_instance == null)
                _instance = new SavedData();

            LoadSavedData();
            Save();

            Theme.SetTheme(DarkMode ? Theme.ETheme.Dark : Theme.ETheme.Normal);
        }

        public static void Save()
        {
            if (File.Exists(SAVE_FILE_PATH))
            {
                File.Delete(SAVE_FILE_PATH);
            }

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(SAVE_FILE_PATH, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, Instance);
            }
        }

        private static void LoadSavedData()
        {
            if (File.Exists(SAVE_FILE_PATH))
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(SAVE_FILE_PATH, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    if (stream.Length > 0)
                    {
                        SavedData obj = null;
                        try
                        {
                            obj = (SavedData)formatter.Deserialize(stream);
                        }
                        catch
                        {
                            obj = new SavedData();
                        }
                        stream.Close();
                        _instance = obj;
                    }
                }
            }
        }

        [Serializable]
        public class BuffSkillInfo
        {
            public BuffSkillInfo()
            {
                Skill = Constants.NONE;
            }

            public bool IsValid { get { return Skill != Constants.NONE; } }

            public int Skill { get; set; }
            public bool UseOnParty { get; set; }
        }
    }
}
