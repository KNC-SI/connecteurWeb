using ADODB;
using MG2S_Mail;
using MG2S_SBOClass;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SAPbobsCOM;
using System;
using System.Collections;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading;

namespace RunScriptOvh
{
    class clsGeneralSetting : ISerializable
    {
        public string LastSettingsError;

        public static string AppName;

        public clsGeneralSetting.enLogType LogMode;

        private static object K;

        private Hashtable _SETTINGS;

        public string FTP_Serveur;

        public bool FTP_UseSSL;

        public string FTP_Login;

        private string _FTP_Password;

        public string FTP_Racine;

        public string SMTP_Server;

        public bool SMTP_UseSSL;

        public bool SMTP_AuthReq;

        public string SMTP_Login;

        private string _SMTP_Pass;

        public string SMTP_DefaultSender;

        public bool SMTP_SendToBCC;

        public string SMTP_BCC;

        public bool LogInFile;

        public string LogInFile_Directory;

        public int LogInFile_Level;

        public bool LogInSBOTools;

        public bool LogInADO;

        public string LogInADO_DBName;

        public int LogInADO_Level;

        public bool LogInWinEvents;

        public int LogInWinEvents_Level;

        public bool ErrToMail;

        public int ErrToMail_Level;

        public string ErrToMail_Sender;

        public string ErrToMail_To;

        public string ErrToMail_Subject;

        public bool ErrToMail_Limited;

        public int ErrToMail_LimitedMinutes;

        public bool SaveSBO_Before;

        public string SaveSBO_Before_Rep;

        public int SaveSBO_Before_Limit;

        public bool SaveSBO_After;

        public string SaveSBO_After_Rep;

        public int SaveSBO_After_Limit;

        public int DBCount
        {
            get
            {
                return this._SETTINGS.Count;
            }
        }

        public clsGeneralSetting.DataBaseSettings DBItem(object index)
        {


            
                if (!(index is string))
                {
                    return (clsGeneralSetting.DataBaseSettings)this._SETTINGS[RuntimeHelpers.GetObjectValue(index)];
                }
                int dBCount = checked(this.DBCount - 1);
                for (int i = 0; i <= dBCount; i = checked(i + 1))
                {
                    clsGeneralSetting.DataBaseSettings dataBaseSettings = (clsGeneralSetting.DataBaseSettings)this._SETTINGS[RuntimeHelpers.GetObjectValue(index)];
                    if (Operators.CompareString(dataBaseSettings.name, index.ToString(), false) == 0)
                    {
                        return dataBaseSettings;
                    }
                }
                return null;
            
        }

        public string FTP_Password
        {
            get
            {
                string str;
                try
                {
                    str = Objets.StrDecrypt(this._FTP_Password, Conversions.ToString(clsGeneralSetting.K), false);
                }
                catch (Exception exception)
                {
                    ProjectData.SetProjectError(exception);
                    str = "";
                    ProjectData.ClearProjectError();
                }
                return str;
            }
            set
            {
                this._FTP_Password = Objets.StrCrypt(value, Conversions.ToString(clsGeneralSetting.K));
            }
        }

        public static string Key
        {
            set
            {
                clsGeneralSetting.K = Objets.MD5(string.Concat(value, "K¨."), false);
            }
        }

        private DateTime LastMailLogSend
        {
            get
            {
                DateTime dateTime;
                if (this.LogMode == clsGeneralSetting.enLogType.LocalRegistry)
                {
                    string appName = clsGeneralSetting.AppName;
                    dateTime = new DateTime(1900, 1, 1);
                    return Conversions.ToDate(Objets.LocalGetSetting(appName, "MailTrace", "LastSend", dateTime));
                }
                string str = clsGeneralSetting.AppName;
                dateTime = new DateTime(1900, 1, 1);
                return Conversions.ToDate(Objets.GlobalGetSetting(str, "MailTrace", "LastSend", dateTime, null));
            }
            set
            {
                if (this.LogMode != clsGeneralSetting.enLogType.LocalRegistry)
                {
                    Objets.GlobalSaveSetting(clsGeneralSetting.AppName, "MailTrace", "LastSend", value, null);
                }
                else
                {
                    Objets.LocalSaveSetting(clsGeneralSetting.AppName, "MailTrace", "LastSend", value);
                }
            }
        }

        public string SMTP_Pass
        {
            get
            {
                string str;
                try
                {
                    str = Objets.StrDecrypt(this._SMTP_Pass, Conversions.ToString(clsGeneralSetting.K), false);
                }
                catch (Exception exception)
                {
                    ProjectData.SetProjectError(exception);
                    str = "";
                    ProjectData.ClearProjectError();
                }
                return str;
            }
            set
            {
                this._SMTP_Pass = Objets.StrCrypt(value, Conversions.ToString(clsGeneralSetting.K));
            }
        }

        //static clsGeneralSetting()
        //{
        //    clsGeneralSetting.AppName = MyProject.Application.Info.ProductName;
        //    clsGeneralSetting.K = Objets.MD5(string.Concat(MyProject.Application.Info.ProductName, "K¨."), false);
        //}

        public clsGeneralSetting()
        {
            this.LogMode = clsGeneralSetting.enLogType.GlobalRegistry;
            this._SETTINGS = new Hashtable();
            this.FTP_Racine = "/";
            this.SMTP_AuthReq = true;
            this.LogInFile_Level = 3;
            this.LogInADO_Level = 3;
            this.LogInWinEvents_Level = 0;
            this.ErrToMail_Level = 0;
            this.ErrToMail_Limited = false;
            this.ErrToMail_LimitedMinutes = 1;
            this.SaveSBO_Before_Limit = 20;
            this.SaveSBO_After_Limit = 100;
        }

        public clsGeneralSetting(SerializationInfo info, StreamingContext context)
        {
            this.LogMode = clsGeneralSetting.enLogType.GlobalRegistry;
            this._SETTINGS = new Hashtable();
            this.FTP_Racine = "/";
            this.SMTP_AuthReq = true;
            this.LogInFile_Level = 3;
            this.LogInADO_Level = 3;
            this.LogInWinEvents_Level = 0;
            this.ErrToMail_Level = 0;
            this.ErrToMail_Limited = false;
            this.ErrToMail_LimitedMinutes = 1;
            this.SaveSBO_Before_Limit = 20;
            this.SaveSBO_After_Limit = 100;
            try
            {
                this._SETTINGS = (Hashtable)info.GetValue("_SETTINGS", this._SETTINGS.GetType());
                this.FTP_Serveur = info.GetString("FTP_Serveur");
                this.FTP_UseSSL = info.GetBoolean("FTP_UseSSL");
                this.FTP_Login = info.GetString("FTP_Login");
                this._FTP_Password = info.GetString("_FTP_Password");
                this.FTP_Racine = info.GetString("FTP_Racine");
                this.SMTP_Server = info.GetString("SMTP_Server");
                this.SMTP_UseSSL = info.GetBoolean("SMTP_UseSSL");
                this.SMTP_AuthReq = info.GetBoolean("SMTP_AuthReq");
                this.SMTP_Login = info.GetString("SMTP_Login");
                this._SMTP_Pass = info.GetString("_SMTP_Pass");
                this.SMTP_DefaultSender = info.GetString("SMTP_DefaultSender");
                this.SMTP_SendToBCC = info.GetBoolean("SMTP_SendToBCC");
                this.SMTP_BCC = info.GetString("SMTP_BCC");
                this.LogInFile = info.GetBoolean("LogInFile");
                this.LogInFile_Directory = info.GetString("LogInFile_Directory");
                this.LogInFile_Level = info.GetInt32("LogInFile_Level");
                this.LogInSBOTools = info.GetBoolean("LogInSBOTools");
                this.LogInADO = info.GetBoolean("LogInADO");
                this.LogInADO_DBName = info.GetString("LogInADO_DBName");
                this.LogInADO_Level = info.GetInt32("LogInADO_Level");
                this.LogInWinEvents = info.GetBoolean("LogInWinEvents");
                this.LogInWinEvents_Level = info.GetInt32("LogInWinEvents_Level");
                this.ErrToMail = info.GetBoolean("ErrToMail");
                this.ErrToMail_Level = info.GetInt32("ErrToMail_Level");
                this.ErrToMail_Limited = info.GetBoolean("ErrToMail_Limited");
                this.ErrToMail_LimitedMinutes = info.GetInt32("ErrToMail_LimitedMinutes");
                this.ErrToMail_Sender = info.GetString("ErrToMail_Sender");
                this.ErrToMail_Subject = info.GetString("ErrToMail_Subject");
                this.ErrToMail_To = info.GetString("ErrToMail_To");
                this.SaveSBO_Before = info.GetBoolean("SaveSBO_Before");
                this.SaveSBO_Before_Limit = info.GetInt32("SaveSBO_Before_Limit");
                this.SaveSBO_Before_Rep = info.GetString("SaveSBO_Before_Rep");
                this.SaveSBO_After = info.GetBoolean("SaveSBO_After");
                this.SaveSBO_After_Limit = info.GetInt32("SaveSBO_After_Limit");
                this.SaveSBO_After_Rep = info.GetString("SaveSBO_After_Rep");
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                ProjectData.ClearProjectError();
            }
        }

        public clsGeneralSetting.DataBaseSettings AddDB(string pName)
        {
            //if (this.DBExists(pName))
            //{
            //    return null;
            //}
            clsGeneralSetting.DataBaseSettings dataBaseSetting = new clsGeneralSetting.DataBaseSettings(pName);
            this._SETTINGS.Add(this.DBCount, dataBaseSetting);
            return dataBaseSetting;
        }

        //public bool DBExists(string pName)
        //{
        //    int dBCount = checked(this.DBCount - 1);
        //    for (int i = 0; i <= dBCount; i = checked(i + 1))
        //    {
        //        if (Operators.CompareString(this[i].name, pName, false) == 0)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public static clsGeneralSetting FromXML(string sXML)
        {
            clsGeneralSetting _clsGeneralSetting;
            try
            {
                SoapFormatter soapFormatter = new SoapFormatter();
                byte[] numArray = new byte[checked(checked(sXML.Length * 2) + 1)];
                Encoding.UTF8.GetBytes(sXML, 0, sXML.Length, numArray, 0);
                MemoryStream memoryStream = new MemoryStream(numArray);
                soapFormatter.TypeFormat = FormatterTypeStyle.XsdString;
                _clsGeneralSetting = (clsGeneralSetting)soapFormatter.Deserialize(memoryStream);
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                _clsGeneralSetting = new clsGeneralSetting();
                ProjectData.ClearProjectError();
            }
            return _clsGeneralSetting;
        }

        public clsGeneralSetting.Logger getLogger(string pFilename = null, string pEventSource = null)
        {
            return new clsGeneralSetting.Logger(this, pFilename, pEventSource);
        }


        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("_SETTINGS", this._SETTINGS);
                info.AddValue("FTP_Serveur", this.FTP_Serveur);
                info.AddValue("FTP_UseSSL", this.FTP_UseSSL);
                info.AddValue("FTP_Login", this.FTP_Login);
                info.AddValue("_FTP_Password", this._FTP_Password);
                info.AddValue("FTP_Racine", this.FTP_Racine);
                info.AddValue("SMTP_Server", this.SMTP_Server);
                info.AddValue("SMTP_UseSSL", this.SMTP_UseSSL);
                info.AddValue("SMTP_AuthReq", this.SMTP_AuthReq);
                info.AddValue("SMTP_Login", this.SMTP_Login);
                info.AddValue("_SMTP_Pass", this._SMTP_Pass);
                info.AddValue("SMTP_DefaultSender", this.SMTP_DefaultSender);
                info.AddValue("SMTP_SendToBCC", this.SMTP_SendToBCC);
                info.AddValue("SMTP_BCC", this.SMTP_BCC);
                info.AddValue("LogInFile", this.LogInFile);
                info.AddValue("LogInFile_Directory", this.LogInFile_Directory);
                info.AddValue("LogInFile_Level", this.LogInFile_Level);
                info.AddValue("LogInSBOTools", this.LogInSBOTools);
                info.AddValue("LogInADO", this.LogInADO);
                info.AddValue("LogInADO_DBName", this.LogInADO_DBName);
                info.AddValue("LogInADO_Level", this.LogInADO_Level);
                info.AddValue("LogInWinEvents", this.LogInWinEvents);
                info.AddValue("LogInWinEvents_Level", this.LogInWinEvents_Level);
                info.AddValue("ErrToMail", this.ErrToMail);
                info.AddValue("ErrToMail_Level", this.ErrToMail_Level);
                info.AddValue("ErrToMail_Limited", this.ErrToMail_Limited);
                info.AddValue("ErrToMail_LimitedMinutes", this.ErrToMail_LimitedMinutes);
                info.AddValue("ErrToMail_Sender", this.ErrToMail_Sender);
                info.AddValue("ErrToMail_Subject", this.ErrToMail_Subject);
                info.AddValue("ErrToMail_To", this.ErrToMail_To);
                info.AddValue("SaveSBO_Before", this.SaveSBO_Before);
                info.AddValue("SaveSBO_Before_Limit", this.SaveSBO_Before_Limit);
                info.AddValue("SaveSBO_Before_Rep", this.SaveSBO_Before_Rep);
                info.AddValue("SaveSBO_After", this.SaveSBO_After);
                info.AddValue("SaveSBO_After_Limit", this.SaveSBO_After_Limit);
                info.AddValue("SaveSBO_After_Rep", this.SaveSBO_After_Rep);
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                ProjectData.ClearProjectError();
            }
        }

        public void RemoveDB(string pName)
        {
            clsGeneralSetting.DataBaseSettings dataBaseSettings;
            Hashtable hashtables = new Hashtable();
            int num = 0;
            int dBCount = checked(this.DBCount - 1);
            for (int i = 0; i <= dBCount; i = checked(i + 1))
            {
                 dataBaseSettings = (clsGeneralSetting.DataBaseSettings)this._SETTINGS[RuntimeHelpers.GetObjectValue(i)];
                if (Operators.CompareString(dataBaseSettings.name, pName, false) != 0)
                {
                    hashtables.Add(num, dataBaseSettings);
                    num = checked(num + 1);
                }
            }
            this._SETTINGS = hashtables;
        }

        public void SendErrorMail(ref clsGeneralSetting.Logger pLogger, string pTo = null)
        {
            string errMailBody = pLogger.getErrMailBody();
            pLogger.ClearLog();
            if (Operators.CompareString(errMailBody, "", false) != 0)
            {
                if (DateTime.Compare(this.LastMailLogSend.AddMinutes((double)this.ErrToMail_LimitedMinutes), DateAndTime.Now) < 0)
                {
                    this.LastMailLogSend = DateAndTime.Now;
                }
                else if (this.ErrToMail_Limited)
                {
                    pLogger.AddLog("Envoi du message par e-mail annulé, temps entre 2 envois trop rapproché", clsGeneralSetting.Logger.En_LogDetail.D_Infos_Importante);
                    return;
                }
                if (!this.SendMail(this.ErrToMail_Subject, errMailBody, Conversions.ToString(Operators.AddObject(this.ErrToMail_To, Interaction.IIf(pTo != null, string.Concat(",", pTo), ""))), null, this.ErrToMail_Sender, null, false, null))
                {
                    pLogger.AddLog(this.LastSettingsError, clsGeneralSetting.Logger.En_LogDetail.A_Erreurs_Critiques);
                }
            }
        }

        public void SendErrorMail(string pMessage, string pTo = null, string pAttach = null)
        {
            try
            {
                if (!this.ErrToMail)
                {
                    this.getLogger(null, null).AddLog(string.Concat(pMessage, "\r\nCe message aurait du être envoyé par e-mail, mais le paramétrage ne le permet pas."), clsGeneralSetting.Logger.En_LogDetail.C_Avertissements);
                }
                else
                {
                    if (DateTime.Compare(this.LastMailLogSend.AddMinutes((double)this.ErrToMail_LimitedMinutes), DateAndTime.Now) < 0)
                    {
                        this.LastMailLogSend = DateAndTime.Now;
                    }
                    else if (this.ErrToMail_Limited)
                    {
                        this.getLogger(null, null).AddLog(string.Concat(pMessage.Trim(), "\r\nCe message aurait du être envoyé par e-mail, mais le temps entre 2 envois est trop rapproché."), clsGeneralSetting.Logger.En_LogDetail.C_Avertissements);
                        return;
                    }
                    if (this.SendMail(this.ErrToMail_Subject, pMessage, Conversions.ToString(Operators.AddObject(this.ErrToMail_To, Interaction.IIf(pTo != null, string.Concat(",", pTo), ""))), pAttach, this.ErrToMail_Sender, null, false, null))
                    {
                        this.getLogger(null, null).AddLog(this.LastSettingsError, clsGeneralSetting.Logger.En_LogDetail.A_Erreurs_Critiques);
                        return;
                    }
                }
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                ProjectData.ClearProjectError();
            }
        }

        public bool SendMail(string psubject, string pbody, string pto, string pAttach = null, string pfrom = null, string pCC = null, bool pHtml = false, string pBCC = null)
        {
            bool flag;
            MailTools mailTool;
            try
            {
                string sMTPDefaultSender = this.SMTP_DefaultSender;
                if (pfrom != null)
                {
                    sMTPDefaultSender = pfrom;
                }
                mailTool = (this.SMTP_AuthReq ? new MailTools(sMTPDefaultSender, pto.Replace(";", ","), psubject, pbody, this.SMTP_Server, this.SMTP_Login, this.SMTP_Pass, pHtml, null, this.SMTP_UseSSL) : new MailTools(sMTPDefaultSender, pto.Replace(";", ","), psubject, pbody, this.SMTP_Server, "", "", pHtml, null, this.SMTP_UseSSL));
                if (pAttach != null)
                {
                    string[] strArrays = pAttach.Split(new char[] { '|' });
                    for (int i = 0; i < checked((int)strArrays.Length); i = checked(i + 1))
                    {
                        string str = strArrays[i];
                        if (Operators.CompareString(str.Trim(), "", false) != 0)
                        {
                            mailTool.Attach(str);
                        }
                    }
                }
                if (pto != null)
                {
                    mailTool.AddRecipient(pto.Replace(";", ","), true);
                }
                if (pCC != null)
                {
                    mailTool.AddCC(pCC.Replace(";", ","), true);
                }
                if (pBCC != null)
                {
                    mailTool.AddBCC(pBCC.Replace(";", ","), true);
                }
                if (this.SMTP_SendToBCC)
                {
                    mailTool.AddBCC(this.SMTP_BCC.Replace(";", ","), false);
                }
                if (mailTool.Send(true))
                {
                    flag = true;
                }
                else
                {
                    this.LastSettingsError = mailTool.LastErrorString;
                    flag = false;
                }
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                this.LastSettingsError = exception.Message;
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        //public clsGeneralSetting ShowParamsWindows(bool pDefineFtp = true, bool pDefineSMTP = true)
        //{
        //    FrmGeneralSettings frmGeneralSetting = new FrmGeneralSettings()
        //    {
        //        Settings = this,
        //        NeedFtp = pDefineFtp,
        //        NeedSMTP = pDefineSMTP
        //    };
        //    frmGeneralSetting.ShowDialog();
        //    return frmGeneralSetting.Settings;
        //}

        public string ToXML()
        {
            MemoryStream memoryStream = new MemoryStream();
            (new SoapFormatter()).Serialize(memoryStream, this);
            return Encoding.UTF8.GetString(memoryStream.GetBuffer());
        }

        [Serializable]
        public class DataBaseSettings : ISerializable
        {
            private string _NAME;

            public string P_LicServ;

            public string P_SERVEUR_Name;

            public string P_SERVEUR_Type;

            public bool P_SERVEUR_AuthWin;

            public string P_SERVEUR_Login;

            private string _P_SERVEUR_Pass;

            public string P_SERVEUR_DB;

            public string P_SERVEUR_SAPUser;

            private string _P_SERVEUR_SAPPass;

            public bool S_SERVEUR_Activated;

            public string S_SERVEUR_Name;

            public string S_SERVEUR_Type;

            public bool S_SERVEUR_AuthWin;

            public string S_SERVEUR_Login;

            private string _S_SERVEUR_Pass;

            public string S_SERVEUR_DB;

            public string name
            {
                get
                {
                    return this._NAME;
                }
                set
                {
                    this._NAME = value;
                }
            }

            public string P_SERVEUR_Pass
            {
                get
                {
                    string str;
                    try
                    {
                        str = Objets.StrDecrypt(this._P_SERVEUR_Pass, Conversions.ToString(clsGeneralSetting.K), false);
                    }
                    catch (Exception exception)
                    {
                        ProjectData.SetProjectError(exception);
                        str = "";
                        ProjectData.ClearProjectError();
                    }
                    return str;
                }
                set
                {
                    this._P_SERVEUR_Pass = Objets.StrCrypt(value, Conversions.ToString(clsGeneralSetting.K));
                }
            }

            public string P_SERVEUR_SAPPass
            {
                get
                {
                    string str;
                    try
                    {
                        str = Objets.StrDecrypt(this._P_SERVEUR_SAPPass, Conversions.ToString(clsGeneralSetting.K), false);
                    }
                    catch (Exception exception)
                    {
                        ProjectData.SetProjectError(exception);
                        str = "";
                        ProjectData.ClearProjectError();
                    }
                    return str;
                }
                set
                {
                    this._P_SERVEUR_SAPPass = Objets.StrCrypt(value, Conversions.ToString(clsGeneralSetting.K));
                }
            }

            public string S_SERVEUR_Pass
            {
                get
                {
                    string str;
                    try
                    {
                        str = Objets.StrDecrypt(this._S_SERVEUR_Pass, Conversions.ToString(clsGeneralSetting.K), false);
                    }
                    catch (Exception exception)
                    {
                        ProjectData.SetProjectError(exception);
                        str = "";
                        ProjectData.ClearProjectError();
                    }
                    return str;
                }
                set
                {
                    this._S_SERVEUR_Pass = Objets.StrCrypt(value, Conversions.ToString(clsGeneralSetting.K));
                }
            }

            internal DataBaseSettings(string pName)
            {
                this._NAME = "";
                this.P_LicServ = "";
                this.P_SERVEUR_Name = "(Local)";
                this.P_SERVEUR_Type = "MSSQL 2008";
                this.P_SERVEUR_AuthWin = true;
                this.S_SERVEUR_Activated = false;
                this.S_SERVEUR_Name = "(Local)";
                this.S_SERVEUR_Type = "MSSQL 2008";
                this.S_SERVEUR_AuthWin = true;
                this._NAME = pName;
            }

            public DataBaseSettings(SerializationInfo info, StreamingContext context)
            {
                this._NAME = "";
                this.P_LicServ = "";
                this.P_SERVEUR_Name = "(Local)";
                this.P_SERVEUR_Type = "MSSQL 2008";
                this.P_SERVEUR_AuthWin = true;
                this.S_SERVEUR_Activated = false;
                this.S_SERVEUR_Name = "(Local)";
                this.S_SERVEUR_Type = "MSSQL 2008";
                this.S_SERVEUR_AuthWin = true;
                try
                {
                    this._NAME = info.GetString("NAME");
                    this.P_SERVEUR_Name = info.GetString("P_SERVEUR_Name");
                    this.P_SERVEUR_Type = info.GetString("P_SERVEUR_Type");
                    this.P_SERVEUR_AuthWin = info.GetBoolean("P_SERVEUR_AuthWin");
                    this.P_SERVEUR_Login = info.GetString("P_SERVEUR_Login");
                    this._P_SERVEUR_Pass = info.GetString("_P_SERVEUR_Pass");
                    this.P_SERVEUR_DB = info.GetString("P_SERVEUR_DB");
                    this.P_SERVEUR_SAPUser = info.GetString("P_SERVEUR_SAPUSER");
                    this._P_SERVEUR_SAPPass = info.GetString("_P_SERVEUR_SAPPass");
                    this.S_SERVEUR_Activated = info.GetBoolean("S_SERVEUR_Activated");
                    this.S_SERVEUR_Name = info.GetString("S_SERVEUR_Name");
                    this.S_SERVEUR_Type = info.GetString("S_SERVEUR_Type");
                    this.S_SERVEUR_AuthWin = info.GetBoolean("S_SERVEUR_AuthWin");
                    this.S_SERVEUR_Login = info.GetString("S_SERVEUR_Login");
                    this._S_SERVEUR_Pass = info.GetString("_S_SERVEUR_Pass");
                    this.S_SERVEUR_DB = info.GetString("S_SERVEUR_DB");
                    this.P_LicServ = info.GetString("P_LicServ");
                }
                catch (Exception exception)
                {
                    ProjectData.SetProjectError(exception);
                    ProjectData.ClearProjectError();
                }
            }

            public static clsGeneralSetting.DataBaseSettings FromXML(string sXML)
            {
                SoapFormatter soapFormatter = new SoapFormatter();
                byte[] numArray = new byte[checked(checked(sXML.Length * 2) + 1)];
                Encoding.UTF8.GetBytes(sXML, 0, sXML.Length, numArray, 0);
                MemoryStream memoryStream = new MemoryStream(numArray);
                soapFormatter.TypeFormat = FormatterTypeStyle.XsdString;
                return (clsGeneralSetting.DataBaseSettings)soapFormatter.Deserialize(memoryStream);
            }

            public Connection GetADOcnx(bool pFromPrincipal = false)
            {
                Connection connectionClass = new Connection()
                {
                    ConnectionString = this.getConnexionString(ref pFromPrincipal),
                    CursorLocation = CursorLocationEnum.adUseClient
                };
                connectionClass.Open("", "", "", -1);
                if (!pFromPrincipal && Operators.CompareString(this.S_SERVEUR_Type, "HANA", false) == 0)
                {
                    string str = string.Concat("SET SCHEMA \"", this.S_SERVEUR_DB, "\"");
                    object value = Missing.Value;
                    connectionClass.Execute(str, out value, -1);
                }
                return connectionClass;
            }

            public OleDbConnection GetADONETcnx(bool pFromPrincipal = false)
            {
                OleDbConnection oleDbConnection = new OleDbConnection()
                {
                    ConnectionString = this.getConnexionString(ref pFromPrincipal)
                };
                oleDbConnection.Open();
                return oleDbConnection;
            }

            private string getConnexionString(ref bool pFromPrincipal)
            {
                string str;
                string[] pSERVEURName;
                if (!this.S_SERVEUR_Activated || pFromPrincipal || !LicenseLib.Options.ToUpper().Contains("READING_CONN"))
                {
                    pFromPrincipal = true;
                    switch (SBOTools.Str2ServerType(this.P_SERVEUR_Type))
                    {
                        case BoDataServerTypes.dst_MSSQL2008:
                            {
                                str = "Provider=SQLNCLI10;";
                                break;
                            }
                        case BoDataServerTypes.dst_MSSQL2012:
                            {
                                str = "Provider=SQLNCLI11;";
                                break;
                            }
                        default:
                            {
                                str = "Provider=SQLNCLI;";
                                break;
                            }
                    }
                    if (!this.P_SERVEUR_AuthWin)
                    {
                        pSERVEURName = new string[] { str, ";Data Source=", this.P_SERVEUR_Name, ";User ID=", this.P_SERVEUR_Login, ";Password=", this.P_SERVEUR_Pass, ";" };
                        str = string.Concat(pSERVEURName);
                    }
                    else
                    {
                        str = string.Concat(str, ";Integrated Security=SSPI;Data Source=", this.P_SERVEUR_Name, ";");
                    }
                    str = string.Concat(str, "Initial Catalog=", this.P_SERVEUR_DB);
                }
                else
                {
                    if (Operators.CompareString(this.S_SERVEUR_Type, "HANA", false) == 0)
                    {
                        pSERVEURName = new string[] { "DRIVER={HDBODBC32};UID=", this.S_SERVEUR_Login, ";PWD=", this.S_SERVEUR_Pass, ";SERVERNODE=", this.S_SERVEUR_Name, ";" };
                        return string.Concat(pSERVEURName);
                    }
                    switch (SBOTools.Str2ServerType(this.S_SERVEUR_Type))
                    {
                        case BoDataServerTypes.dst_MSSQL2008:
                            {
                                str = "Provider=SQLNCLI10;";
                                break;
                            }
                        case BoDataServerTypes.dst_MSSQL2012:
                            {
                                str = "Provider=SQLNCLI11;";
                                break;
                            }
                        default:
                            {
                                str = "Provider=SQLNCLI;";
                                break;
                            }
                    }
                    if (!this.S_SERVEUR_AuthWin)
                    {
                        pSERVEURName = new string[] { str, ";Data Source=", this.S_SERVEUR_Name, ";User ID=", this.S_SERVEUR_Login, ";Password=", this.S_SERVEUR_Pass, ";" };
                        str = string.Concat(pSERVEURName);
                    }
                    else
                    {
                        str = string.Concat(str, ";Integrated Security=SSPI;Data Source=", this.S_SERVEUR_Name, ";");
                    }
                    str = string.Concat(str, "Initial Catalog=", this.S_SERVEUR_DB);
                }
                return str;
            }

            public Company GetDICompany(clsGeneralSetting.Logger pLogger = null)
            {
                string[] str;
                //SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
                Company companyClass = new SAPbobsCOM.Company()
                {
                    UseTrusted = this.P_SERVEUR_AuthWin
                };
                if (!this.P_SERVEUR_AuthWin)
                {
                    companyClass.DbUserName = this.P_SERVEUR_Login;
                    companyClass.DbPassword = this.P_SERVEUR_Pass;
                }
                companyClass.DbServerType = SBOTools.Str2ServerType(this.P_SERVEUR_Type);
                if (Operators.CompareString(this.P_LicServ, "", false) != 0)
                {
                    companyClass.LicenseServer = this.P_LicServ;
                }
                companyClass.language = BoSuppLangs.ln_French;
                companyClass.CompanyDB = this.P_SERVEUR_DB;
                companyClass.UserName = this.P_SERVEUR_SAPUser;
                companyClass.Password = this.P_SERVEUR_SAPPass;
                companyClass.Server = this.P_SERVEUR_Name;
                int num = 0;
                while (true)
                {
                    num = checked(num + 1);
                    if (companyClass.Connect() == 0)
                    {
                        break;
                    }
                    if (num > 20)
                    {
                        if (pLogger != null)
                        {
                            pLogger.AddLog(string.Concat("Echec des tentatives de connexion à ", this.P_SERVEUR_DB, " : ", companyClass.GetLastErrorDescription()), clsGeneralSetting.Logger.En_LogDetail.A_Erreurs_Critiques);
                        }
                        throw new Exception(companyClass.GetLastErrorDescription());
                    }
                    if (pLogger != null)
                    {
                        str = new string[] { "Tentative n° ", num.ToString(), " : Echec de la connexion à ", this.P_SERVEUR_DB, " : ", companyClass.GetLastErrorDescription(), " (LoginSQL=", this.P_SERVEUR_Login, ", ServerType=", companyClass.DbServerType.ToString(), ")" };
                        pLogger.AddLog(string.Concat(str), clsGeneralSetting.Logger.En_LogDetail.C_Avertissements);
                    }
                    Thread.Sleep(30000);
                }
                if (pLogger != null)
                {
                    str = new string[] { "Tentative n° ", num.ToString(), " : Connexion à ", this.P_SERVEUR_DB, " Effectuée " };
                    pLogger.AddLog(string.Concat(str), clsGeneralSetting.Logger.En_LogDetail.E_Infos);
                }
                return companyClass;
            }

            public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                try
                {
                    info.AddValue("NAME", this._NAME);
                    info.AddValue("P_SERVEUR_Name", this.P_SERVEUR_Name);
                    info.AddValue("P_SERVEUR_Type", this.P_SERVEUR_Type);
                    info.AddValue("P_SERVEUR_AuthWin", this.P_SERVEUR_AuthWin);
                    info.AddValue("P_SERVEUR_Login", this.P_SERVEUR_Login);
                    info.AddValue("_P_SERVEUR_Pass", this._P_SERVEUR_Pass);
                    info.AddValue("P_SERVEUR_DB", this.P_SERVEUR_DB);
                    info.AddValue("P_SERVEUR_SAPUSER", this.P_SERVEUR_SAPUser);
                    info.AddValue("_P_SERVEUR_SAPPass", this._P_SERVEUR_SAPPass);
                    info.AddValue("S_SERVEUR_Activated", this.S_SERVEUR_Activated);
                    info.AddValue("S_SERVEUR_Name", this.S_SERVEUR_Name);
                    info.AddValue("S_SERVEUR_Type", this.S_SERVEUR_Type);
                    info.AddValue("S_SERVEUR_AuthWin", this.S_SERVEUR_AuthWin);
                    info.AddValue("S_SERVEUR_Login", this.S_SERVEUR_Login);
                    info.AddValue("_S_SERVEUR_Pass", this._S_SERVEUR_Pass);
                    info.AddValue("S_SERVEUR_DB", this.S_SERVEUR_DB);
                    info.AddValue("P_LicServ", this.P_LicServ);
                }
                catch (Exception exception)
                {
                    ProjectData.SetProjectError(exception);
                    ProjectData.ClearProjectError();
                }
            }

            public override string ToString()
            {
                return this._NAME;
            }

            public string ToXML()
            {
                MemoryStream memoryStream = new MemoryStream();
                (new SoapFormatter()).Serialize(memoryStream, this);
                return Encoding.UTF8.GetString(memoryStream.GetBuffer());
            }
        }

        public enum enLogType
        {
            GlobalRegistry,
            LocalRegistry
        }

        public class Logger
        {
            private static EventLog ELog;

            private bool LogInFile;

            private int LogInFileLevel;

            private string LogDirectory;

            private string FileLogName;

            private bool LogInEventSource;

            private int LogEventSourceLevel;

            private string EventSource;

            private bool LogInADO;

            private int LogInADOLevel;

            private Connection ADOCN;

            private bool LogInMail;

            private int LogInMailLevel;

            private StringBuilder MailLog;

            private bool LogInSBOTOOLS;

            private bool AutoManaged;

            static Logger()
            {
                clsGeneralSetting.Logger.ELog = new EventLog();
            }

            public Logger()
            {
                this.LogInFile = false;
                this.LogInEventSource = false;
                this.LogInADO = false;
                this.LogInMail = false;
                this.MailLog = new StringBuilder();
                this.LogInSBOTOOLS = false;
                this.AutoManaged = false;
            }

            internal Logger(clsGeneralSetting @params, string pfilename, string pEventSource)
            {
                this.LogInFile = false;
                this.LogInEventSource = false;
                this.LogInADO = false;
                this.LogInMail = false;
                this.MailLog = new StringBuilder();
                this.LogInSBOTOOLS = false;
                this.AutoManaged = false;
                this.AutoManaged = true;
                this.LogInEventSource = @params.LogInWinEvents;
                this.LogEventSourceLevel = @params.LogInWinEvents_Level;
                this.LogInFile = @params.LogInFile;
                this.LogInFileLevel = @params.LogInFile_Level;
                this.LogInMail = @params.ErrToMail;
                this.LogInMailLevel = @params.ErrToMail_Level;
                this.LogInSBOTOOLS = @params.LogInSBOTools;
                this.LogInADOLevel = @params.LogInADO_Level;
                this.LogInADO = @params.LogInADO;
                this.LogDirectory = @params.LogInFile_Directory;
                this.reloadEventsVars(pEventSource);
                this.reloadFileVars(pfilename);
                //if (this.LogInADO)
                //{
                //    this.ADOCN = @params[@params.LogInADO_DBName].GetADOcnx(false);
                //}
            }

            public void ActivateADOLog(int plevel, ref Connection pAdocn)
            {
                if (this.AutoManaged)
                {
                    throw new Exception("Modification des paramètres impossible en mode automanager");
                }
                this.LogInADO = true;
                this.LogInADOLevel = plevel;
                this.ADOCN = pAdocn;
            }

            public void ActivateEventSourceLog(string pSource, int plevel)
            {
                if (this.AutoManaged)
                {
                    throw new Exception("Modification des paramètres impossible en mode automanager");
                }
                this.LogInEventSource = true;
                this.LogEventSourceLevel = plevel;
                this.reloadEventsVars(pSource);
            }

            public void ActivateFileLog(string pLogDirectory, int plevel, string pfileName = "")
            {
                if (this.AutoManaged)
                {
                    throw new Exception("Modification des paramètres impossible en mode automanager");
                }
                this.LogInFile = true;
                this.LogDirectory = pLogDirectory;
                this.LogInFileLevel = plevel;
                this.reloadFileVars(pfileName);
            }

            public void ActivateMailLog(int plevel)
            {
                if (this.AutoManaged)
                {
                    throw new Exception("Modification des paramètres impossible en mode automanager");
                }
                this.LogInMail = true;
                this.LogInMailLevel = plevel;
            }

            public void ActivateSBOTOOLSLog()
            {
                if (this.AutoManaged)
                {
                    throw new Exception("Modification des paramètres impossible en mode automanager");
                }
                this.LogInSBOTOOLS = true;
            }

            public void AddLog(string pMessage, clsGeneralSetting.Logger.En_LogDetail pLevel)
            {
                string[] strArrays = new string[] { Strings.Format(DateTime.Now, "yyyy/MM/dd HH:mm:ss"), " - ", pLevel.ToString(), " - ", pMessage };
                string str = string.Concat(strArrays);
                if (this.EventSource != null && (int)pLevel <= this.LogEventSourceLevel)
                {
                    if (Operators.CompareString(clsGeneralSetting.Logger.ELog.Source, "", false) == 0)
                    {
                        clsGeneralSetting.Logger.ELog.Source = this.EventSource;
                    }
                    if (!EventLog.SourceExists(clsGeneralSetting.Logger.ELog.Source))
                    {
                        EventLog.CreateEventSource(clsGeneralSetting.Logger.ELog.Source, string.Concat("Log", clsGeneralSetting.Logger.ELog.Source));
                    }
                    switch (pLevel)
                    {
                        case clsGeneralSetting.Logger.En_LogDetail.A_Erreurs_Critiques:
                        case clsGeneralSetting.Logger.En_LogDetail.B_Erreurs:
                            {
                                clsGeneralSetting.Logger.ELog.WriteEntry(pMessage, EventLogEntryType.Error);
                                break;
                            }
                        case clsGeneralSetting.Logger.En_LogDetail.C_Avertissements:
                            {
                                clsGeneralSetting.Logger.ELog.WriteEntry(pMessage, EventLogEntryType.Warning);
                                break;
                            }
                        default:
                            {
                                clsGeneralSetting.Logger.ELog.WriteEntry(pMessage, EventLogEntryType.Information);
                                break;
                            }
                    }
                }
                string str1 = Conversions.ToString(0);
                while (true)
                {
                    try
                    {
                        str1 = Conversions.ToString(Conversions.ToDouble(str1) + 1);
                        if (this.LogInFile & (int)pLevel <= this.LogInFileLevel)
                        {
                            if (!Directory.Exists(this.LogDirectory))
                            {
                                Directory.CreateDirectory(this.LogDirectory);
                            }
                            Objets.FileWrite(this.FileLogName, string.Concat(str, "\r\n"), true, false, null);
                        }
                        break;
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        Exception exception = exception1;
                        if (Conversions.ToDouble(str1) != 1)
                        {
                            if (this.EventSource != null)
                            {
                                clsGeneralSetting.Logger.ELog.WriteEntry(string.Concat("Inscription dans le fichier de log impossible : ", exception.Message), EventLogEntryType.Warning);
                            }
                            ProjectData.ClearProjectError();
                            break;
                        }
                        else
                        {
                            Thread.Sleep(100);
                            ProjectData.ClearProjectError();
                        }
                    }
                }
                if (this.LogInMail && (int)pLevel <= this.LogInMailLevel)
                {
                    this.MailLog.Append(string.Concat(str, "\r\n"));
                }
                try
                {
                    if (this.LogInSBOTOOLS)
                    {
                        SBOTools.AddLog(pMessage, (int)pLevel);
                    }
                    if (this.LogInADO & (int)pLevel <= this.LogInADOLevel)
                    {
                        strArrays = new string[] { "begin tran\r\ndeclare @NextCode as varchar(8) ;\r\nselect @NextCode=cast(coalesce(Max(cast(Code as int)),0)+1 as varchar(8)) FROM [@MG2S_SBOTLog] updlock ; \r\nselect @NextCode=stuff('00000000',9-len(@NextCode),len(@NextCode),@NextCode) ; \r\ninsert into [@MG2S_SBOTLog] (Code,Name, U_DateTime, U_UserName,U_HostName,U_Details, U_Level) VALUES (@NextCode, @NextCode, '", Objets.StrClean(Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss")), "', '", Objets.StrClean(Environment.UserName), "','", Objets.StrClean(Environment.MachineName), "','", Objets.StrClean(pMessage), "',", pLevel.ToString(), ") ;\r\ncommit" };
                        string str2 = string.Concat(strArrays);
                        Connection aDOCN = this.ADOCN;
                        object value = Missing.Value;
                        aDOCN.Execute(str2, out value, -1);
                    }
                }
                catch (Exception exception3)
                {
                    ProjectData.SetProjectError(exception3);
                    Exception exception2 = exception3;
                    if (this.EventSource != null)
                    {
                        clsGeneralSetting.Logger.ELog.WriteEntry(string.Concat("Inscription des logs dans SBO / ADO  impossible : ", exception2.Message), EventLogEntryType.Warning);
                    }
                    ProjectData.ClearProjectError();
                }
            }

            public void ClearLog()
            {
                this.MailLog = new StringBuilder();
            }

            public string getErrMailBody()
            {
                return this.MailLog.ToString();
            }

            private void reloadEventsVars(string peventSource)
            {
                if (peventSource != null)
                {
                    if (Operators.CompareString(peventSource, "", false) != 0)
                    {
                        if (!this.LogInEventSource)
                        {
                            this.EventSource = null;
                            return;
                        }
                        this.EventSource = peventSource;
                        return;
                    }
                }
                this.EventSource = null;
            }

            private void reloadFileVars(string pfilename)
            {
                if (pfilename == null || Operators.CompareString(pfilename.Trim(), "", false) == 0)
                {
                    this.FileLogName = string.Concat(this.LogDirectory, "\\", clsGeneralSetting.AppName, ".log");
                }
                else
                {
                    this.FileLogName = string.Concat(this.LogDirectory, "\\", pfilename);
                }
            }

            public enum En_LogDetail
            {
                A_Erreurs_Critiques,
                B_Erreurs,
                C_Avertissements,
                D_Infos_Importante,
                E_Infos,
                F_Details,
                G_Details_Max
            }
        }
    }
    
}
