using System;
using System.Collections.Generic;
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
        public const int NONE = -1;

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

        public static bool SupportSkills
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

        public static  bool HpRecovery
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
                Instance._recoverySkills[pos] = GetValidSkillValue(skillValue);
            }
        }

        public static void SetAttackSkillAtPos(int pos, int skillValue)
        {
            if (pos >= 0 && pos < Instance._attackSkills.Length)
            {
                Instance._attackSkills[pos] = GetValidSkillValue(skillValue);
            }
        }

        private static int GetValidSkillValue(int nb)
        {
            if (nb < 0 || nb >= 10)
                nb = NONE;
            return nb;
        }

        static SavedData _instance = null;

        #region Private Members

        private int[] _attackSkills = null;
        private int[] _recoverySkills = null;
        private SupportSkillInfo[] _supportSkills = null;
        private bool _attackMode = false;
        private bool _recoveryMode = false;
        private bool _useSupportSkills = false;
        private bool _hpRecovery = false;
        private bool _mpRecovery = false;
        private int _hpRecoverySkill = NONE;
        private int _mpRecoverySkill = NONE;
        private int _hpRecoveryPercent = 0;
        private int _mpRecoveryPercent = 0;
        private int _recoverySkillPercent = 0;
        #endregion

        private SavedData()
        {
            _attackSkills = new int[NB_ATTACK_SKILLS];
            for (int i = 0; i < NB_ATTACK_SKILLS; i++)
            {
                _attackSkills[i] = NONE;
            }

            _recoverySkills = new int[NB_REC_SKILLS];
            for (int i = 0; i < NB_REC_SKILLS; i++)
            {
                _recoverySkills[i] = NONE;
            }

            _supportSkills = new SupportSkillInfo[NB_SUPP_SKILLS];
            for (int i = 0; i < NB_SUPP_SKILLS; i++)
            {
                _supportSkills[i] = new SupportSkillInfo();
            }
        }

        public static int GetAttackSkillAtPos(int index)
        {
            if (Instance._attackSkills.Length > index && index >= 0)
            {
                return Instance._attackSkills[index];
            }

            return 0;
        }

        public static int GetRecoverySkillAtPos(int index)
        {
            if (Instance._recoverySkills.Length > index && index >= 0)
            {
                return Instance._recoverySkills[index];
            }

            return 0;
        }

        public static SupportSkillInfo GetSupportSkillAtPos(int index)
        {
            if (Instance._supportSkills.Length > index && index >= 0)
            {
                return Instance._supportSkills[index];
            }

            return null;
        }

        public static void Init()
        {
            if (_instance == null)
                _instance = new SavedData();

            LoadSavedData();
            Save();
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
                stream.Close();
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
        public class SupportSkillInfo
        {
            public SupportSkillInfo()
            {
                Skill = NONE;
            }

            public int Skill { get; set; }
            public bool UseOnParty { get; set; }
        }
    }
}
